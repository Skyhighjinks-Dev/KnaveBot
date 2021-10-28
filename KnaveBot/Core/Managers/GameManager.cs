using System;
using System.IO;

namespace KnaveBot.Core.Managers
{
  public static class GameManager
  {
    public static class Coinflip
    {
      /// <summary>
      /// Coinflip results
      /// </summary>
      public enum CoinflipType
      {
        HEADS = 0,
        TAILS = 1
      }

      /// <summary>
      /// Gets a coinflip result
      /// </summary>
      /// <returns>CoinflipType</returns>
      public static CoinflipType CoinflipResult()
      {
        return new Random().Next(0, 1000) % 2 == 0 ? CoinflipType.HEADS : CoinflipType.TAILS;
      }

      /// <summary>
      /// Gets the URL for the image
      /// </summary>
      /// <param name="nType">CoinFlipResult</param>
      /// <returns>URL</returns>
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
