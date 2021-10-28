using Discord;
using Discord.Commands;
using Discord.WebSocket;

using KnaveBot.Core.Managers;

using System.Threading.Tasks;

namespace KnaveBot.Core.Commands
{
  public class MusicCommands : ModuleBase<SocketCommandContext>
  {
    /// <summary>
    /// Joins bot to voice channel
    /// </summary>
    /// <returns></returns>
    [Command("Join")]
    public async Task JoinCommand() => await Context.Channel.SendMessageAsync(embed: await AudioManager.JoinAsync(Context.Guild, Context.User as IVoiceState));

    /// <summary>
    /// Plays an audio track
    /// </summary>
    /// <param name="nQuery">Query to search for (Link or query)</param>
    /// <returns></returns>
    [Command("play")]
    public async Task PlayCommand([Remainder] string nQuery) => await Context.Channel.SendMessageAsync(embed: await AudioManager.PlayAsync(Context.User as SocketGuildUser, Context.Guild, nQuery));

    /// <summary>
    /// Skips current song
    /// </summary>
    /// <returns></returns>
    [Command("skip")]
    public async Task SkipCommand() => await Context.Channel.SendMessageAsync(embed: await AudioManager.SkipAsync(Context.User as SocketGuildUser, Context.Guild));

    /// <summary>
    /// Leaves discord channel
    /// </summary>
    /// <returns></returns>
    [Command("leave")]
    public async Task LeaveCommand() => await Context.Channel.SendMessageAsync(embed: await AudioManager.LeaveAsync(Context.Guild));

    /// <summary>
    /// Pauses the current track
    /// </summary>
    /// <returns></returns>
    [Command("pause")]
    public async Task PauseCommand() => await Context.Channel.SendMessageAsync(embed: await AudioManager.PauseTrack(Context.Guild));

    /// <summary>
    /// Resumes the current track from a paused state
    /// </summary>
    /// <returns></returns>
    [Command("resume")]
    public async Task ResumeCommand() => await Context.Channel.SendMessageAsync(embed: await AudioManager.ResumeTrack(Context.Guild));

    /// <summary>
    /// Shows the current queue
    /// </summary>
    /// <param name="nText">Page number (Revise this somepoint)</param>
    /// <returns></returns>
    [Command("Queue")]
    public async Task Queue([Remainder]string nText)
    {
      if (string.IsNullOrEmpty(nText))
      {
        await Context.Channel.SendMessageAsync(embed: await AudioManager.Queue(Context.Guild, 1));
        return;
      }

      string[] words = nText.Split(' ');

      for (int x = 0; x < words.Length; x++)
      {
        if (int.TryParse(words[x], out int y))
        {
          await Context.Channel.SendMessageAsync(embed: await AudioManager.Queue(Context.Guild, y));
          return;
        }
      }

      await Context.Channel.SendMessageAsync(embed: await AudioManager.Queue(Context.Guild, 1));
    }

    /// <summary>
    /// Shows the current queue
    /// </summary>
    /// <returns></returns>
    [Command("Queue")]
    public async Task Queue() => await Queue(null);
  }
}
