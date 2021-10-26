using System;
using System.IO;

namespace KnaveBot.Core.Managers
{
  public static class GameManager
  {
    public static class Coinflip
    {
      public enum CoinflipType
      {
        HEADS = 0,
        TAILS = 1
      }

      public static CoinflipType CoinflipResult()
      {
        return new Random().Next(0, 1000) % 2 == 0 ? CoinflipType.HEADS : CoinflipType.TAILS;
      }

      public static string GetCoinURL(CoinflipType nType)
      {
        string coinUrl = "";

        switch (nType)
        {
          case CoinflipType.HEADS:
            coinUrl = "https://studio.code.org/v3/assets/MJxtgNaXjdkjrgav_zV9UAhc8gnHZuJ-KwhCS2yj3Yk/Head.png";
            break;

          case CoinflipType.TAILS:
            coinUrl = "https://i.redd.it/1ulp5hryfhr61.jpg";
            break;

          default:
            break;
        }

        return coinUrl;
      }
    }
  }
}
