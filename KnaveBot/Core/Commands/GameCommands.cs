using Discord;
using Discord.Commands;
using KnaveBot.Core.Managers;
using System.IO;
using System.Threading.Tasks;
using static KnaveBot.Core.Managers.GameManager.Coinflip;

namespace KnaveBot.Core.Commands
{
  public class GameCommands : ModuleBase<SocketCommandContext>
  {
    [Command("coinflip")]
    public async Task Coinflip()
    {
      CoinflipType type = CoinflipResult();

      Embed _embed = EmbedManager.BuildCoinflipEmbed(type).Build();

      await ReplyAsync(embed: EmbedManager.BuildCoinflipEmbed(type).Build());
    }
  }
}
