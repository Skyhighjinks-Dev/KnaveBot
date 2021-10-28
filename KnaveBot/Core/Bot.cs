using Discord.Commands;
using Discord.WebSocket;

using KnaveBot.Core.Managers;
using KnaveBot.Database;

using Microsoft.Extensions.DependencyInjection;

using System.Threading.Tasks;

using Victoria;

namespace KnaveBot.Core
{
  public class Bot
  {
    /// <summary>
    /// Database Manager
    /// </summary>
    public static DatabaseManager DBManager { get; private set; }

    /// <summary>
    /// Client and command service
    /// </summary>
    private DiscordSocketClient _client;
    private CommandService _commandService;

    /// <summary>
    /// Information for discord and LavaNode
    /// </summary>
    private string DiscordToken { get; set; }
    private string LavaNodeHost { get; set; }
    private string LavaNodePass { get; set; }


    public Bot()
    {
      DBManager = new DatabaseManager();

      // Gets config options
      Task.Run(async () => await CollectConfig()).Wait();

      // Sets up Discord connection
      this._client = new DiscordSocketClient(new DiscordSocketConfig() { LogLevel = Discord.LogSeverity.Debug });
      this._commandService = new CommandService(new CommandServiceConfig()
      {
        LogLevel = Discord.LogSeverity.Debug,
        CaseSensitiveCommands = false,
        DefaultRunMode = RunMode.Async,
        IgnoreExtraArgs = true
      });

      // Sets up collection
      var collection = new ServiceCollection();
      collection.AddSingleton(_client)
                .AddSingleton(_commandService)
                .AddSingleton<LavaNode>()
                .AddLavaNode(x =>
                {
                  x.SelfDeaf = true;
                  x.Port = 2333;
                  x.Authorization = this.LavaNodePass;
                  x.Hostname = this.LavaNodeHost;
                });

      ServiceManager.SetProvider(collection);
    }

    /// <summary>
    /// Main Async
    /// </summary>
    /// <returns></returns>
    public async Task MainAsync()
    {
      if (string.IsNullOrEmpty(this.DiscordToken))
      {
        // log error
        return;
      }

      await CommandManager.LoadCommandAsync();
      await EventManager.LoadCommands();
      await _client.LoginAsync(Discord.TokenType.Bot, this.DiscordToken);
      await _client.StartAsync();

      await Task.Delay(-1);
    }

    /// <summary>
    /// Assigns config values
    /// </summary>
    /// <returns></returns>
    private async Task CollectConfig()
    {
      this.DiscordToken = await DBManager.GetConfig(nameof(this.DiscordToken)).ConfigureAwait(false);
      this.LavaNodeHost = await DBManager.GetConfig(nameof(this.LavaNodeHost)).ConfigureAwait(false);
      this.LavaNodePass = await DBManager.GetConfig(nameof(this.LavaNodePass)).ConfigureAwait(false);
    }
  }
}
