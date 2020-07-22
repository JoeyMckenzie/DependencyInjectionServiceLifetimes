using System;

namespace DependencyInjectionServiceLifetimes.Services.Scoped
{
    public interface IScopedGreetingService
    {
        void SayScopedHello(string name);
    }
}
