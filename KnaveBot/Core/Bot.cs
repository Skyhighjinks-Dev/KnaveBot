using Discord.Commands;
using Discord.WebSocket;
using KnaveBot.Core.Managers;
using KnaveBot.Database;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Victoria;

namespace KnaveBot.Core
{
  public class Bot
  {
    public static DatabaseManager DBManager { get; private set; }

    private DiscordSocketClient _client;
    private CommandService _commandService;

    private string DiscordToken { get; set; }
    private string LavaNodeHost { get; set; }
    private string LavaNodePass { get; set; }


    public Bot()
    {
      DBManager = new DatabaseManager();

      Task.Run(async () => await CollectConfig()).Wait();

      this._client = new DiscordSocketClient(new DiscordSocketConfig() { LogLevel = Discord.LogSeverity.Debug });
      this._commandService = new CommandService(new CommandServiceConfig()
      {
        LogLevel = Discord.LogSeverity.Debug,
        CaseSensitiveCommands = false,
        DefaultRunMode = RunMode.Async,
        IgnoreExtraArgs = true
      });

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

    private async Task CollectConfig()
    {
      this.DiscordToken = await DBManager.GetConfig(nameof(this.DiscordToken)).ConfigureAwait(false);
      this.LavaNodeHost = await DBManager.GetConfig(nameof(this.LavaNodeHost)).ConfigureAwait(false);
      this.LavaNodePass = await DBManager.GetConfig(nameof(this.LavaNodePass)).ConfigureAwait(false);
    }
  }
}
