using Discord;
using Discord.WebSocket;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Linq;
using System.Threading.Tasks;

using Victoria;
using Victoria.Enums;
using Victoria.Responses.Search;

namespace KnaveBot.Core.Managers
{
  public static class AudioManager
  {
    private static readonly LavaNode LavaNode = ServiceManager.Service.GetRequiredService<LavaNode>();

    public static async Task<Embed> JoinAsync(IGuild nGuild, IVoiceState nVoiceState)
    {
      if (LavaNode.HasPlayer(nGuild))
        return EmbedManager.BuildEmbed("I am already connected to a voice channel!").Build();

      if (nVoiceState.VoiceChannel == null)
        return EmbedManager.BuildEmbed("You must be connected to a voice channel for me to join!").Build();

      try
      {
        await LavaNode.JoinAsync(nVoiceState.VoiceChannel);
        return EmbedManager.BuildEmbed($"Joined - {nVoiceState.VoiceChannel.Name}").Build();
      }
      catch (Exception e)
      {
        EmbedBuilder eb = EmbedManager.BuildEmbed($"Error: {e.Message}");
        eb.Color = Color.Red;
        return eb.Build();

        // Log Error
      }
    }

    public static async Task<Embed> PlayAsync(SocketGuildUser nUser, IGuild nGuild, string nQuery)
    {
      if (nUser.VoiceChannel == null)
        return EmbedManager.BuildEmbed("You must be connected to a voice channel to execute commands").Build();

      if (!LavaNode.HasPlayer(nGuild))
      {
        await JoinAsync(nGuild, nUser.VoiceState);
      }

      try
      {
        var player = LavaNode.GetPlayer(nGuild);

        SearchResponse search;

        SearchType _type;

        if (nQuery.Contains("t="))
          if (int.TryParse(nQuery.Split("t=")[1], out int _))
            nQuery = nQuery.Split("t=")[0];

        if (nQuery.Contains("youtube"))
          if (nQuery.Contains("list"))
            nQuery = $"https://www.youtube.com/playlist?list={nQuery.Split("list=")[1]}";

        search = await LavaNode.SearchAsync(SearchType.Direct, nQuery);

        if (search.Status == SearchStatus.LoadFailed || search.Status == SearchStatus.NoMatches)
          return EmbedManager.BuildEmbed("Issue loading music: " + search.Status).Build();


        foreach (LavaTrack _track in search.Tracks)
        {
          player.Queue.Enqueue(_track);
        }

        if (player.Track != null && player.PlayerState == PlayerState.Playing || player.PlayerState == PlayerState.Paused)
        {
          return EmbedManager.BuildEmbed($"Added: ({search.Tracks.Count()}) songs to the queue").Build();
        }

        await player.PlayAsync(player.Queue.First());

        if(search.Tracks.Count() > 1)
          return EmbedManager.BuildEmbed($"Added: ({search.Tracks.Count()}) songs to the queue").Build();
        
        return EmbedManager.BuildEmbed(search.Tracks.First()).Build();
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }

      return EmbedManager.BuildEmbed("Somethign went wront :x").Build();
    }
  }
}
