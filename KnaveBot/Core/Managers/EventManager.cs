using Discord;
using Discord.Commands;
using Discord.WebSocket;

using KnaveBot.Database;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Threading.Tasks;

using Victoria;

namespace KnaveBot.Core.Managers
{
  public static class EventManager
  {
    /// <summary>Client</summary>
    private static DiscordSocketClient Client = ServiceManager.GetService<DiscordSocketClient>();
    /// <summary>Command service</summary>
    private static CommandService CommandService = ServiceManager.GetService<CommandService>();

    /// <summary>LavaNode</summary>
    private static LavaNode LavaNode = ServiceManager.Service.GetRequiredService<LavaNode>();

    /// <summary>Discord prefix</summary>
    private static string DiscordPrefix { get; set; }

    /// <summary>
    /// Loads commands
    /// </summary>
    /// <returns></returns>
    public static async Task LoadCommands()
    {
      DiscordPrefix = await DatabaseManager.Instance.GetConfig(nameof(DiscordPrefix)).ConfigureAwait(false);
      Client.Ready += OnClientReady;
      Client.MessageReceived += OnClientMessageReceived;

      return;
    }

    /// <summary>
    /// On Client Ready event
    /// </summary>
    /// <returns></returns>
    private static async Task OnClientReady()
    {
      try
      {
        await LavaNode.ConnectAsync();
      }
      catch (Exception e)
      {
        throw e;
        // Log Exception
      }

      Console.WriteLine($"{Client.CurrentUser.Username} has logged in.");

      await Client.SetStatusAsync(Discord.UserStatus.Online);
      await Client.SetGameAsync($"Prefix: {DiscordPrefix}");
    }


    /// <summary>
    /// Message handler
    /// </summary>
    /// <param name="nMessage">Message info</param>
    /// <returns></returns>
    private static async Task OnClientMessageReceived(SocketMessage nMessage)
    {
      var message = (SocketUserMessage)nMessage;
      var context = new SocketCommandContext(Client, message);

      if (message.Author.IsBot || message.Channel is IDMChannel) return;

      int argPos = 0;

      if (!(message.HasStringPrefix(DiscordPrefix, ref argPos) || message.HasMentionPrefix(Client.CurrentUser, ref argPos))) return;

      var result = await CommandService.ExecuteAsync(context, argPos, ServiceManager.Service);

      if (!result.IsSuccess)
      { 
        // Log Error
      }
    }
  }
}
