using KnaveBot.Core;
using KnaveBot.Database;
using System;
using System.Threading.Tasks;

namespace KnaveBot
{
  class Program
  {
    static void Main(string[] args)
    {
      Bot _bot = new Bot();
      _bot.MainAsync().GetAwaiter().GetResult();
    }
  }
}
