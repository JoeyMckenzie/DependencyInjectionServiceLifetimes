using System;
using System.Diagnostics;
using DependencyInjectionServiceLifetimes.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjectionServiceLifetimes
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Instantiate a service container and add each of our service lifetime types
            var builder = new ServiceCollection();
            builder.AddScoped<ScopedService>();
            builder.AddTransient<TransientService>();
            builder.AddSingleton<SingletonService>();

            // Build our service container within the scope of our current program
            using var serviceProvider = builder.BuildServiceProvider();

            // Create a disposable instance of our service container and grab a couple of scoped references
            Console.WriteLine("Building the first service container...\n");
            using var firstScopedContainer = serviceProvider.CreateScope();
            var scopedServiceOne = firstScopedContainer.ServiceProvider.GetRequiredService<ScopedService>();
            var scopedServiceTwo = firstScopedContainer.ServiceProvider.GetRequiredService<ScopedService>();

            // Validate that our scoped services are the same object reference, existing within the same service container scope lifetime
            Debug.Assert(scopedServiceOne == scopedServiceTwo);

            // Create our transient services are difference object references within the same service scope
            var transientServiceOne = firstScopedContainer.ServiceProvider.GetRequiredService<TransientService>();
            var transientServiceTwo = firstScopedContainer.ServiceProvider.GetRequiredService<TransientService>();

            // Validate that our transient services are not the same object reference, newly created for each request from the container
            Debug.Assert(transientServiceOne != transientServiceTwo);

            // Create our singleton services are the same object reference within the same service scope
            var singletonServiceOne = firstScopedContainer.ServiceProvider.GetRequiredService<SingletonService>();
            var singletonServiceTwo = firstScopedContainer.ServiceProvider.GetRequiredService<SingletonService>();

            // Validate that our singleton services are the same object reference, existing for the lifetime of the application
            Debug.Assert(singletonServiceOne == singletonServiceTwo);

            // Dispose of our current service container and create a new one
            firstScopedContainer.Dispose();

            // Create another scoped service container instance and grab a few more of our lifetime services for comparison
            Console.WriteLine("\nBuilding our second service container...");
            using var secondScopedContainer = serviceProvider.CreateScope();

            // Create another scoped service instance and compare it's object reference to the previous scoped instances
            Console.WriteLine("\nGrabbing a reference to another scoped service...");
            var anotherScopedService = secondScopedContainer.ServiceProvider.GetRequiredService<ScopedService>();
            Debug.Assert(anotherScopedService != scopedServiceOne && anotherScopedService != scopedServiceTwo);

            //// Create another transient service instance and compare it's object reference to the previous transient instances
            Console.WriteLine("\nGrabbing a reference to another transient service...");
            var anotherTransientService = secondScopedContainer.ServiceProvider.GetRequiredService<TransientService>();
            Debug.Assert(anotherTransientService != transientServiceOne && anotherTransientService != transientServiceTwo);

            // Create another singleton service instance and compare it's object reference to the previous singleton instances
            Console.WriteLine("\nGrabbing a reference to another singleton service...");
            var anotherSingletonService = secondScopedContainer.ServiceProvider.GetRequiredService<SingletonService>();
            Debug.Assert(anotherSingletonService == singletonServiceOne && anotherSingletonService == singletonServiceTwo);
        }
    }
}
