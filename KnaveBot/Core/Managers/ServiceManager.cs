using Microsoft.Extensions.DependencyInjection;

using System;

namespace KnaveBot.Core.Managers
{
  public static class ServiceManager
  {
    public static IServiceProvider Service { get; private set; }

    public static void SetProvider(ServiceCollection nCollection) => Service = nCollection.BuildServiceProvider();

    public static T GetService<T>() where T : new() => Service.GetRequiredService<T>();
  }
}
