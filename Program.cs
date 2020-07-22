using System;
using System.Diagnostics;
using DependencyInjectionServiceLifetimes.Services.Scoped;
using DependencyInjectionServiceLifetimes.Services.Singleton;
using DependencyInjectionServiceLifetimes.Services.Transient;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjectionServiceLifetimes
{
    public class Program
    {
        static void Main(string[] args)
        {
            var builder = new ServiceCollection();
            builder.AddScoped<IScopedGreetingService, ScopedGreetingService>();
            builder.AddTransient<ITransientGreetingService, TransientGreetingService>();
            builder.AddSingleton<ISingletonGreetingService, SingletonGreetingService>();

            // Build our service container
            var serviceProvider = builder.BuildServiceProvider();

            // Create a disposable instance of our service container and assert our scoped services are the same object references
            using var firstScopedContainer = serviceProvider.CreateScope();
            var scopedServiceOne = firstScopedContainer.ServiceProvider.GetRequiredService<IScopedGreetingService>();
            var scopedServiceTwo = firstScopedContainer.ServiceProvider.GetRequiredService<IScopedGreetingService>();

            // Asser that our scoped services are the same object reference, existing within the same service container scope lifetime
            Debug.Assert(scopedServiceOne == scopedServiceTwo);

            // Create our transient services are difference object references within the same service scope
            var transientServiceOne = firstScopedContainer.ServiceProvider.GetRequiredService<ITransientGreetingService>();
            var transientServiceTwo = firstScopedContainer.ServiceProvider.GetRequiredService<ITransientGreetingService>();

            // Assert that our transient services are not the same object reference, newly created for each request from the container
            Debug.Assert(transientServiceOne != transientServiceTwo);

            // Create our singleton services are the same object reference within the same service scope
            var singletonServiceOne = firstScopedContainer.ServiceProvider.GetRequiredService<ISingletonGreetingService>();
            var singletonServiceTwo = firstScopedContainer.ServiceProvider.GetRequiredService<ISingletonGreetingService>();

            // Asser that our singleton services are the same object reference, existing for the lifetime of the application
            Debug.Assert(singletonServiceOne == singletonServiceTwo);

            // Create another service container instance and grab a few more of our lifetime services for comparison
            using var secondScopedContainer = serviceProvider.CreateScope();

            // Create another scoped service instance and compare it's object reference to the previous scoped instances
            var anotherScopedService = secondScopedContainer.ServiceProvider.GetRequiredService<IScopedGreetingService>();
            Debug.Assert(anotherScopedService != scopedServiceOne && anotherScopedService != scopedServiceTwo);

            // Create another transient service instance and compare it's object reference to the previous transient instances
            var anotherTransientService = secondScopedContainer.ServiceProvider.GetRequiredService<ITransientGreetingService>();
            Debug.Assert(anotherTransientService != transientServiceOne && anotherTransientService != transientServiceTwo);

            // Create another singletone service instance and compare it's object reference to the previous singleton instances
            var anotherSingletonService = secondScopedContainer.ServiceProvider.GetRequiredService<ISingletonGreetingService>();
            Debug.Assert(anotherSingletonService == singletonServiceOne && anotherSingletonService == singletonServiceTwo);
        }
    }
}
