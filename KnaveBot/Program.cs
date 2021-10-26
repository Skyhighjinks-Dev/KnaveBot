using KnaveBot.Core;
using static KnaveBot.Core.Attributes.DatabaseAttributes;
using KnaveBot.Database.Objects;
using System;
using System.Linq;
using System.Reflection;
using KnaveBot.Core.Attributes;
using KnaveBot.Core.Enum.Discord;

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
