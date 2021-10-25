using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnaveBot.Core.Managers
{
  public static class GameManager
  {
    public static bool Coinflip()
    {
      return new Random().Next(0, 1000) % 2 == 0;
    }
  }
}
