using Discord.Commands;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace KnaveBot.Core.Managers
{
  public class CommandManager
  {
    /// <summary>Retrieves command service</summary>
    private static CommandService CommandService = ServiceManager.GetService<CommandService>();

    /// <summary>
    /// Loads all of the commands
    /// </summary>
    /// <returns></returns>
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
