using Discord;
using Discord.Commands;
using Discord.WebSocket;
using KnaveBot.Core.Enum.Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnaveBot.Core.Managers
{
  public static class AdminManager
  {
    public static async Task<EmbedBuilder> Kick(SocketCommandContext nContext, SocketGuildUser nUser, string nReason)
    { 
      SocketUser sender = nContext.User;

      int actionID = await Database.DatabaseManager.Instance.InsertAdminActionAsync(AdminAction.KICK, nContext.Guild, nUser, sender, nReason);

      //await nUser.KickAsync(nReason, new RequestOptions() { AuditLogReason = nReason, RetryMode = RetryMode.AlwaysRetry});

      await Database.DatabaseManager.Instance.UpdateAdminActionAsync(actionID, true);

      await nContext.Message.DeleteAsync();

      return EmbedManager.BuildEmbed(AdminAction.KICK, nUser, sender, nReason);
    }

    public static async Task<EmbedBuilder> View(SocketGuildUser nUser, int? nPage)
    { 
      if(nPage < 1 || nPage == null)
        nPage = 1;

      var data = await Database.DatabaseManager.Instance.GetActivityData(nUser);

      return EmbedManager.BuildEmbed();
    }
  }
}
