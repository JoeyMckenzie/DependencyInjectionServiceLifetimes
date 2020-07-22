using System;

namespace DependencyInjectionServiceLifetimes.Services.Singleton
{
    public interface ISingletonGreetingService
    {
        void SaySingletonHello(string name);
    }
}
