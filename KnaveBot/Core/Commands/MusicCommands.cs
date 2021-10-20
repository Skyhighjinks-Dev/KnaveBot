using Discord;
using Discord.Commands;
using Discord.WebSocket;

using KnaveBot.Core.Managers;

using System.Threading.Tasks;

namespace KnaveBot.Core.Commands
{
  public class MusicCommands : ModuleBase<SocketCommandContext>
  {
    [Command("Join")]
    public async Task JoinCommand() => await Context.Channel.SendMessageAsync(embed: await AudioManager.JoinAsync(Context.Guild, Context.User as IVoiceState));

    [Command("play")]
    public async Task PlayCommand([Remainder] string nQuery) => await Context.Channel.SendMessageAsync(embed: await AudioManager.PlayAsync(Context.User as SocketGuildUser, Context.Guild, nQuery));
  }
}
