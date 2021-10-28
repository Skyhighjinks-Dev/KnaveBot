using Discord;
using Discord.Commands;
using Discord.WebSocket;
using KnaveBot.Core.Enum.Discord;
using KnaveBot.Database.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnaveBot.Core.Managers
{
  public static class AdminManager
  {
    /// <summary>
    /// Handles kicking a user
    /// </summary>
    /// <param name="nContext">Content - Contains guild and other aspects</param>
    /// <param name="nUser">The user to kick</param>
    /// <param name="nReason">Reason to kick</param>
    /// <returns>EmbedBuilder</returns>
    public static async Task<EmbedBuilder> Kick(SocketCommandContext nContext, SocketGuildUser nUser, string nReason)
    { 
      SocketUser sender = nContext.User;

      int actionID = await Database.DatabaseManager.Instance.InsertAdminActionAsync(AdminAction.KICK, nContext.Guild, nUser, sender, nReason);

      //await nUser.KickAsync(nReason, new RequestOptions() { AuditLogReason = nReason, RetryMode = RetryMode.AlwaysRetry});

      await Database.DatabaseManager.Instance.UpdateAdminActionAsync(actionID, true);

      await nContext.Message.DeleteAsync();

      return EmbedManager.BuildEmbed(AdminAction.KICK, nUser, sender, nReason);
    }

    /// <summary>
    /// Handles viewing a user
    /// </summary>
    /// <param name="nUser">User to view</param>
    /// <param name="nPage">Page to view</param>
    /// <returns>EmbedBuilder</returns>
    public static async Task<EmbedBuilder> View(SocketGuildUser nUser, int? nPage)
    { 
      if(nPage < 1 || nPage == null)
        nPage = 1;

      List<ActivityData> data = await Database.DatabaseManager.Instance.GetActivityData(nUser);

      List<ActivityData> _toShow = new List<ActivityData>();

      try
      { 
        for(int x = 0; x < (10 > data.Count() ? data.Count() : 10); x++)
          _toShow.Add(data[((nPage.Value - 1) * 10) + x]);
      }
      catch(Exception ex)
      { }

      if(nPage > (data.Count() / 10))
        nPage = (int)Math.Ceiling((decimal)(data.Count() / 10));

      return EmbedManager.BuildEmbed(_toShow, nUser, nPage.Value, (int)Math.Ceiling((decimal)(data.Count() / 10)));
    }
  }
}
