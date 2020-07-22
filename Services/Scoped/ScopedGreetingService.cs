using System;

namespace DependencyInjectionServiceLifetimes.Services.Scoped
{
    public class ScopedGreetingService : IScopedGreetingService
    {
        public void SayScopedHello(string name) =>
            Console.WriteLine($"Hello from scoped {name}!");
    }
}
