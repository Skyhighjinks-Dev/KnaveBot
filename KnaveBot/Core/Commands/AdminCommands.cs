using Discord.Commands;
using Discord.WebSocket;
using KnaveBot.Core.Managers;
using System;
using System.Threading.Tasks;

namespace KnaveBot.Core.Commands
{
  public class AdminCommands : ModuleBase<SocketCommandContext>
  {
    /// <summary>
    /// Gets kick help
    /// </summary>
    /// <returns></returns>
    [Command("kick")]
    [RequireUserPermission(Discord.GuildPermission.KickMembers)]
    public async Task KickCommand()
    {
      // Damn Boi you need help kicking someone?!?! who hired this guy
      await ReplyAsync(embed: HelpManager.AdminCommands.KickHelp().Build());
    }


    /// <summary>
    /// Kicks user mentioned in message
    /// </summary>
    /// <param name="nTarget">User to kick from guild</param>
    /// <param name="nReason">(Optional) reason for the kick</param>
    /// <returns></returns>
    [Command("kick")]
    [RequireUserPermission(Discord.GuildPermission.KickMembers)]
    public async Task KickCommand(SocketGuildUser nTarget, [Remainder] string nReason = null) => await ReplyAsync(embed: (await AdminManager.Kick(Context, nTarget, nReason)).Build());


    /// <summary>
    /// Sends user "view" help
    /// </summary>
    /// <returns></returns>
    [Command("view")]
    [RequireUserPermission(Discord.GuildPermission.Administrator)]
    public async Task View()
    { 
      
    }

    /// <summary>
    /// Views a users history with staff
    /// (punishments provided by staff or provided by this person)
    /// </summary>
    /// <param name="nTarget">Person to view</param>
    /// <param name="nPage">Page to check (if multiple pages)</param>
    /// <returns></returns>
    [Command("view")]
    [RequireUserPermission(Discord.GuildPermission.Administrator)]
    [Priority(1)]
    public async Task View(SocketGuildUser nTarget, int? nPage = null) => await ReplyAsync(embed: (await AdminManager.View(nTarget, nPage)).Build());

    //[Command("ban")]
    //[RequireUserPermission(Discord.GuildPermission.BanMembers)]
    //public async Task BanCommand()
    //{
    //  // Damn boi you need help banning someone?
    //}

    //[Command("ban")]
    //[RequireUserPermission(Discord.GuildPermission.BanMembers)]
    //public async Task BanCommand(SocketGuildUser nTarget) => await AdminManager.Ban(nTarget, null);

    //[Command("ban")]
    //[RequireUserPermission(Discord.GuildPermission.BanMembers)]
    //public async Task BanCommand(SocketGuildUser nTarget, [Remainder] string nReason) => await AdminManager.Ban(nTarget, nReason);

    //[Command("unban")]
    //[RequireUserPermission(Discord.GuildPermission.BanMembers)]
    //public async Task UnbanCommand()
    //{ 
    //  // Fair enough ig
    //}

    //[Command("unban")]
    //[RequireUserPermission(Discord.GuildPermission.BanMembers)]
    //public async Task UnbanCommand(ulong nUserID) => await AdminManager.Unban(nUserID);

    //[Command("unban")]
    //[RequireUserPermission(Discord.GuildPermission.BanMembers)]
    //public async Task UnbanCommand(string nBanID) => await AdminManager.Unban(nBanID);

    //[Command("tempban")]
    //[RequireUserPermission(Discord.GuildPermission.BanMembers)]
    //public async Task TempbanCommand()
    //{ 
    //  // Fairs
    //}

    //[Command("tempban")]
    //[RequireUserPermission(Discord.GuildPermission.BanMembers)]
    //public async Task TempbanCommand(SocketGuildUser nUser, [Remainder] string nReason) => await AdminManager.Tempban(nUser, nReaosn);

    //[Command("tempban")]
    //[RequireUserPermission(Discord.GuildPermission.BanMembers)]
    //public async Task TempbanCommand(SocketGuildUser nUser) => await AdminManager.Tempban(nUser);

    //[Command("banlist")]
    //public async Task BanListCommand()
    //{ 
    //  // Fairs
    //}

    //[Command("banlist")]
    //[RequireUserPermission(Discord.GuildPermission.BanMembers)]
    //public async Task BanListCommand() => await AdminManager.BanList(null, null);


    //[Command("banlist")]
    //[RequireUserPermission(Discord.GuildPermission.BanMembers)]
    //public async Task BanListCommand(int nPage) => await AdminManager.BanList(nPage, null);

    //[Command("banlist")]
    //[RequireUserPermission(Discord.GuildPermission.BanMembers)]
    //public async Task BanListCommand(string nUserName) => await AdminManager.BanList(null, nUserName);

    //[Command("poll")]
    //[RequireUserPermission(Discord.GuildPermission.Administrator)]
    //public async Task Poll()
    //{ 

    //}

    //[Command("poll")]
    //[RequireUserPermission(Discord.GuildPermission.Administrator)]
    //public async Task Poll([Remainder] string nOptions) => await AdminManager.Poll(nOptions.Split(';'));
  }
}
