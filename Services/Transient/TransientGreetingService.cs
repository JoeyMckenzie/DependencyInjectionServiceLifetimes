using System;

namespace DependencyInjectionServiceLifetimes.Services.Transient
{
    public class TransientGreetingService : ITransientGreetingService
    {
        public void SayTransientHello(string name) =>
            Console.WriteLine($"Hello from transient {name}!");
    }
}
