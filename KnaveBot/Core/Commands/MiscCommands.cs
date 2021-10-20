using Discord.Commands;

using KnaveBot.Core.Managers;

using System.Threading.Tasks;

namespace KnaveBot.Core.Commands
{
  public class MiscCommands : ModuleBase<SocketCommandContext>
  {
    [Command("ping")]
    public async Task Ping()
    {
      await ReplyAsync(embed: EmbedManager.BuildEmbed("Pong").Build());
    }
  }
}
