using System;

namespace DependencyInjectionServiceLifetimes.Services
{
    public class TransientService : IDisposable
    {
        public TransientService() =>
            Console.WriteLine("Constructing a transient service...");

        public void Dispose() =>
            Console.WriteLine("Disposing of transient service...");
    }
}
