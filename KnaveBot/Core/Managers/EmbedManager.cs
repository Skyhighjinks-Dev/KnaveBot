﻿using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
  }
}