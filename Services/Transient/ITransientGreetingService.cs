using System;

namespace DependencyInjectionServiceLifetimes.Services.Transient
{
    public interface ITransientGreetingService
    {
        void SayTransientHello(string name);
    }
}
