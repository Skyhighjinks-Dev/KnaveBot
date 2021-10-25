using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using Victoria;

namespace KnaveBot.Core.Managers
{
  public static class EmbedManager
  {
    public static EmbedBuilder BuildEmbed(string nMessage)
    {
      EmbedBuilder eb = new EmbedBuilder()
      {
        Description = nMessage,
        Color = Color.Blue
      };

      return eb;
    }

    public static EmbedBuilder BuildEmbed(LavaTrack nTrack)
    {
      EmbedBuilder eb = new EmbedBuilder()
      {
        Title = "Video is now playing",
        Description = nTrack.Title,
        Color = Color.Blue,
        Url = nTrack.Url,
        ImageUrl = nTrack.FetchArtworkAsync().Result,
        Footer = new EmbedFooterBuilder() { Text = $"Duration: {nTrack.Duration.ToString()}" }
      };

      return eb;
    }

    public static EmbedBuilder BuildEmbed(LavaTrack nCurrTrack, int nTrackCount)
    {
      EmbedBuilder eb = BuildEmbed(nCurrTrack);

      eb.AddField(new EmbedFieldBuilder()
      {
        Name = "Added tracks",
        Value = $"({nTrackCount}) tracks have been added to the queue"
      });

      return eb;
    }

    public static EmbedBuilder BuildEmbed(LavaPlayer nPlayer, int nPage)
    {
      EmbedBuilder eb = new EmbedBuilder()
      {
        Title = "Current Queue",
        Footer = new EmbedFooterBuilder()
        { 
          Text = $"Page: {nPage}/{(int)Math.Ceiling((decimal)(nPlayer.Queue.Count / 10))}"
        }
      };

      List<LavaTrack> tracks = nPlayer.Queue.ToList();

      string content = "";

      int _pageItteration = (nPage - 1) * 10;

      for (int x = 0; x < 10; x++)
      {
        content += $"[[{_pageItteration + (x + 1)}]: {tracks[_pageItteration + x].Title}]({tracks[_pageItteration + x].Url}){(x == 10 ? "" : "\n")}";
      }

      eb.AddField(new EmbedFieldBuilder()
      {
        Name = "Queued Tracks",
        Value = content
      });

      return eb;
    }

    public static EmbedBuilder BuildCoinflipEmbed(bool nIsHeads)
    {
      // TODO
      // Add image for heads and tails

      EmbedBuilder eb = new EmbedBuilder()
      {
        Title = "Coinflip"
      };

      eb.AddField(new EmbedFieldBuilder()
      {
        Name = "Result",
        Value = nIsHeads ? "Heads" : "Tails",
        IsInline = true
      });
      
      return eb;
    }
  }
}
