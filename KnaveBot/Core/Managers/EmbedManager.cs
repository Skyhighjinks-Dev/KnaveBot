using Discord;

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
  }
}
