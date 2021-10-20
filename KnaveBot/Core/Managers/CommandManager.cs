using Discord.Commands;

using System;
using System.Reflection;
using System.Threading.Tasks;

namespace KnaveBot.Core.Managers
{
  public static class CommandManager
  {
    private static CommandService CommandService = ServiceManager.GetService<CommandService>();

    public static async Task LoadCommandAsync()
    {
      await CommandService.AddModulesAsync(Assembly.GetEntryAssembly(), ServiceManager.Service);

      foreach (CommandInfo info in CommandService.Commands)
      {
        Console.WriteLine($"Command: {info.Name} has been loaded");
      }
    }
  }
}
