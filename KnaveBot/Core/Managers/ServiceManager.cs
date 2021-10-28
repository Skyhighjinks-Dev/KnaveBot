using Microsoft.Extensions.DependencyInjection;

using System;

namespace KnaveBot.Core.Managers
{
  public static class ServiceManager
  {
    /// <summary>
    /// Service providor
    /// </summary>
    public static IServiceProvider Service { get; private set; }

    /// <summary>
    /// Sets the Service Provider
    /// </summary>
    /// <param name="nCollection">Service Collection</param>
    public static void SetProvider(ServiceCollection nCollection) => Service = nCollection.BuildServiceProvider();

    /// <summary>
    /// Retrieves a specific service
    /// </summary>
    /// <typeparam name="T">Type of service to retrieve</typeparam>
    /// <returns>T type</returns>
    public static T GetService<T>() where T : new() => Service.GetRequiredService<T>();
  }
}
