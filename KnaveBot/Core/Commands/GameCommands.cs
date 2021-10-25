using Discord.Commands;
using KnaveBot.Core.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnaveBot.Core.Commands
{
  public class GameCommands : ModuleBase<SocketCommandContext>
  {
    [Command("coinflip")]
    public async Task Coinflip() => await ReplyAsync(embed: EmbedManager.BuildCoinflipEmbed(GameManager.Coinflip()).Build());
  }
}
