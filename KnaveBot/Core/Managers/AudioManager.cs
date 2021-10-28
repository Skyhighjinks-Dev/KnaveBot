using Discord;
using Discord.WebSocket;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Linq;
using System.Threading.Tasks;

using Victoria;
using Victoria.Enums;
using Victoria.EventArgs;
using Victoria.Responses.Search;

namespace KnaveBot.Core.Managers
{
  public static class AudioManager
  {
    /// <summary>LavaNode which handles audio</summary>
    private static readonly LavaNode LavaNode = ServiceManager.Service.GetRequiredService<LavaNode>();


    /// <summary>
    /// Joins bot to voice channel
    /// </summary>
    /// <param name="nGuild">Guild to join</param>
    /// <param name="nVoiceState">Voice channel to join</param>
    /// <returns>Embed to send</returns>
    public static async Task<Embed> JoinAsync(IGuild nGuild, IVoiceState nVoiceState)
    {
      // Checks to see if we're already connected
      if (LavaNode.HasPlayer(nGuild))
        return EmbedManager.BuildEmbed("I am already connected to a voice channel!").Build();

      // Checks to see if user is in a voice channel
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

    /// <summary>
    /// Plays audio
    /// </summary>
    /// <param name="nUser">User who requested the song</param>
    /// <param name="nGuild">Guild to join</param>
    /// <param name="nQuery">Query to search for</param>
    /// <returns>Embed to send</returns>
    public static async Task<Embed> PlayAsync(SocketGuildUser nUser, IGuild nGuild, string nQuery)
    {
      // Makes sure they're in a voice channel
      if (nUser.VoiceChannel == null)
        return EmbedManager.BuildEmbed("You must be connected to a voice channel to execute commands").Build();

      // If we aren't already connected to a voice channel, then we join the user
      if (!LavaNode.HasPlayer(nGuild))
      {
        await JoinAsync(nGuild, nUser.VoiceState);
      }

      try
      {
        // Get the player
        var player = LavaNode.GetPlayer(nGuild);

        SearchResponse search;

        // Adjust the query depending on specific issues found during testing (e.g Timestamp screwing up the bots search) 
        if (nQuery.Contains("t="))
          if (int.TryParse(nQuery.Split("t=")[1], out int _))
            nQuery = nQuery.Split("t=")[0];

        // Formatting query to allow playlists to be added
        if (nQuery.Contains("youtube"))
          if (nQuery.Contains("list"))
            nQuery = $"https://www.youtube.com/playlist?list={nQuery.Split("list=")[1]}";

        // Searches and validates the response
        search = await LavaNode.SearchAsync(SearchType.Direct, nQuery);

        if (search.Status == SearchStatus.LoadFailed || search.Status == SearchStatus.NoMatches)
          return EmbedManager.BuildEmbed("Issue loading music: " + search.Status).Build();

        // Adds all of the tracks to the queue
        foreach (LavaTrack _track in search.Tracks)
        {
          player.Queue.Enqueue(_track);
        }

        if (player.Track != null && player.PlayerState == PlayerState.Playing || player.PlayerState == PlayerState.Paused)
        {
          return EmbedManager.BuildEmbed($"Added: ({search.Tracks.Count()}) songs to the queue").Build();
        }

        // Plays the first song
        await player.PlayAsync(player.Queue.First());

        if(search.Tracks.Count() > 1)
          return EmbedManager.BuildEmbed(player.Track, search.Tracks.Count()).Build();
        
        return EmbedManager.BuildEmbed(player.Track).Build();
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return EmbedManager.BuildEmbed("Somethign went wront :x").Build();
      }
    }


    /// <summary>
    /// Skips current song
    /// </summary>
    /// <param name="nUser">Sender</param>
    /// <param name="nGuild">Sender's guild</param>
    /// <returns>Embed to send</returns>
    public static async Task<Embed> SkipAsync(SocketGuildUser nUser, IGuild nGuild)
    {
      if (nUser.VoiceChannel == null)
        return EmbedManager.BuildEmbed("You must be connected to a voice channel to use this command").Build();

      if (!LavaNode.HasPlayer(nGuild))
        return EmbedManager.BuildEmbed("I must be connected to a voice channel to execute this command").Build();

      LavaPlayer player = LavaNode.GetPlayer(nGuild);

      try
      {
        if (player.Track == null)
          return EmbedManager.BuildEmbed("No track is currently playing").Build();

        LavaTrack _track = player.Track;

        if (player.PlayerState == PlayerState.Playing || player.PlayerState == PlayerState.Paused)
        {
          if (player.Queue.Count() < 1)
          {
            await player.StopAsync();
            return EmbedManager.BuildEmbed("No songs left in the queue").Build();
          }

          var _newTrack = await player.SkipAsync(null);

          while(_newTrack.Current.Url == _newTrack.Skipped.Url)
            _newTrack = await player.SkipAsync(null);

          return EmbedManager.BuildEmbed(_newTrack.Current).Build();
        }

        return EmbedManager.BuildEmbed("Something went wrong, weirdly :x").Build();
      }
      catch (Exception e)
      {
        return EmbedManager.BuildEmbed($"Error: {e.Message}").Build();
      }
    }


    /// <summary>
    /// Leaves the Voice Channel
    /// </summary>
    /// <param name="nGuild">Guild to disconnect from</param>
    /// <returns>Embed to return</returns>
    public static async Task<Embed> LeaveAsync(IGuild nGuild)
    {
      try
      {
        var player = LavaNode.GetPlayer(nGuild);
        
        if (player.PlayerState == PlayerState.Playing)
          await player.StopAsync();

        await LavaNode.LeaveAsync(player.VoiceChannel);
        return EmbedManager.BuildEmbed("Leaving channel").Build();
      }
      catch (Exception e)
      {
        return EmbedManager.BuildEmbed($"Error: {e.Message}").Build();
      }
    }


    /// <summary>
    /// Happens on the event of a track ending
    /// </summary>
    /// <param name="nArgs">Argument information</param>
    /// <returns>Embed</returns>
    public static async Task<Embed> TrackEnded(TrackEndedEventArgs nArgs)
    {
      if (!nArgs.Player.Queue.TryDequeue(out LavaTrack nTrack))
        return EmbedManager.BuildEmbed("Issue trying to play next song").Build();

      if (!(nTrack is LavaTrack track))
        return EmbedManager.BuildEmbed($"Something even worse happened lolol").Build();

      await nArgs.Player.PlayAsync(track);
      return EmbedManager.BuildEmbed(track).Build();
    }


    /// <summary>
    /// Pauses the track
    /// </summary>
    /// <param name="nGuild">Guild to pause track in</param>
    /// <returns>Embed to send</returns>
    public static async Task<Embed> PauseTrack(IGuild nGuild)
    {
      try
      {
        if (LavaNode.GetPlayer(nGuild).PlayerState == PlayerState.Playing)
        {
          await LavaNode.GetPlayer(nGuild).PauseAsync();
          return EmbedManager.BuildEmbed("Player paused").Build();
        }

        return EmbedManager.BuildEmbed("Player is already paused").Build();
      }
      catch (Exception e)
      {
        return EmbedManager.BuildEmbed($"Error: {e.Message}").Build();
      }
    }


    /// <summary>
    /// Resumes the track
    /// </summary>
    /// <param name="nGuild">Guild to resume playing in</param>
    /// <returns>Embed to send</returns>
    public static async Task<Embed> ResumeTrack(IGuild nGuild)
    {
      try
      {
        LavaPlayer player = LavaNode.GetPlayer(nGuild);

        if (player.PlayerState == PlayerState.Stopped ||
            player.PlayerState == PlayerState.Paused)
        {
          await player.ResumeAsync();
          return EmbedManager.BuildEmbed(player.Track).Build();
        }
        else
          return EmbedManager.BuildEmbed($"Can not resume").Build();
      }
      catch (Exception e)
      {
        return EmbedManager.BuildEmbed($"Error: {e.Message}").Build();
      }
    }

    /// <summary>
    /// Sends queue information
    /// </summary>
    /// <param name="nGuild">Guild to send queue information for</param>
    /// <param name="nPage">Page of the queue</param>
    /// <returns>Embed to send</returns>
    public static async Task<Embed> Queue(IGuild nGuild, int nPage)
    {
      LavaPlayer player = LavaNode.GetPlayer(nGuild);

      if (nPage < 1)
        nPage = 1;

      if (nPage > Math.Ceiling((decimal)(player.Queue.Count() / 10)))
        nPage = (int)Math.Ceiling((decimal)(player.Queue.Count() / 10));

      return EmbedManager.BuildEmbed(player, nPage).Build();
    }
  }
}
