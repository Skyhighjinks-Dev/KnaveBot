﻿using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace KnaveBot.Core.Commands
{
  public class AdminCommands : ModuleBase<SocketCommandContext>
  {
    //[Command("kick")]
    //[RequireUserPermission(Discord.GuildPermission.KickMembers)]
    //public async Task KickCommand()
    //{
    //  // Damn Boi you need help kicking someone?!?! who hired this guy
    //}

    //[Command("kick")]
    //[RequireUserPermission(Discord.GuildPermission.KickMembers)]
    //public async Task KickCommand(SocketGuildUser nTarget) => await AdminManager.Kick(nTarget, null);

    //[Command("kick")]
    //[RequireUserPermission(Discord.GuildPermission.KickMembers)]
    //public async Task KickCommand(SocketGuildUser nTarget, [Remainder] string nReason) => await AdminManager.Kick(nTarget, nReason);

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