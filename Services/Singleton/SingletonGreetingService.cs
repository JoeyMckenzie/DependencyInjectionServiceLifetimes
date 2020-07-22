using System;

namespace DependencyInjectionServiceLifetimes.Services.Singleton
{
    public class SingletonGreetingService : ISingletonGreetingService
    {
        public void SaySingletonHello(string name) =>
            Console.WriteLine($"Hello from singleton {name}!");
    }
}
