using System;

namespace DependencyInjectionServiceLifetimes.Services
{
    public class SingletonService : IDisposable
    {
        public SingletonService() =>
            Console.WriteLine("Constructing a singleton service...");

        public void Dispose() =>
            Console.WriteLine("Disposing of singleton service...");
    }
}
