using System;

namespace DependencyInjectionServiceLifetimes.Services
{
    public class ScopedService : IDisposable
    {
        public ScopedService() =>
            Console.WriteLine("Constructing a scoped service...");

        public void Dispose() =>
            Console.WriteLine("Disposing of scoped service...");
    }
}
