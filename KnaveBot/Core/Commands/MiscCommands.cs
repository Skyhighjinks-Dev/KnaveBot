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
      await ReplyAsync(embed: EmbedManager.BuildEmbed("Pong?").Build());
    }


    //[Command("help")]
    //public async Task Help([Remainder] string nCmd)
    //{ 

    //}


    //[Command("help")]
    //public async Task Help()
    //{ 

    //}

    //[Command("invite")]
    //public async Task Invite() => MiscManager.Invite();

    //[Command("meme")]
    //public async Task Meme() => MiscManager.Meme();

    //[Command("crypto")]
    //public async Task Crypto()
    //{ 
    
    //}

    //[Command("crypto")]
    //public async Task Crypto(string nCrypto) => MiscManager.Crypto();
  }
}
