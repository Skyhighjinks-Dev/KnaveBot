using Discord;

namespace KnaveBot.Core.Managers
{
  public static class HelpManager
  {
    public static class AdminCommands
    { 
      /// <summary>
      /// Displays how to kick a user
      /// </summary>
      /// <returns>Embed Builder</returns>
      public static EmbedBuilder KickHelp()
      { 
        EmbedBuilder eb = EmbedManager.BuildEmbed();

        eb.Title = "Admin Help: (***Kick Command***)";
        eb.Author = new EmbedAuthorBuilder() { Name = "Skyhighjinks#3395" };
        eb.Description = "This will help detail how to properly use this command.";

        eb.AddField("kick @MentionedUser", "This will simply kick the user, without any reason.", true);
        eb.AddField("kick @MentionedUser {reason}", "This will simply kick the user and log a reason against their profile stored by the bot.", true);
        eb.AddField("Required Permissions: ", "KickMembers");

        return eb;
      }
    }
  }
}
