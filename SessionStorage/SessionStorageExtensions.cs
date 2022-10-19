using System;
using Forge.Wasm.BrowserStorages.Serialization.SessionStorage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Forge.Wasm.BrowserStorages.NewtonSoft.Json.SessionStorage
{

    /// <summary>Extension methods for IServiceCollection</summary>
    public static class SessionStorageExtensions
    {

        /// <summary>
        /// Registers the Forge LocalStorage services as scoped.
        /// </summary>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddForgeSessionStorageWithNewtonsoftSerializer(this IServiceCollection services)
            => services.AddForgeSessionStorageWithNewtonsoftSerializer(null);

        /// <summary>
        /// Registers the Forge LocalStorage services as scoped.
        /// </summary>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddForgeSessionStorageWithNewtonsoftSerializer(this IServiceCollection services, Action<SerializerOptions> configure)
        {
            return services
                .Replace(new ServiceDescriptor(typeof(ISerializationProvider), typeof(JsonSerializer), ServiceLifetime.Scoped))
                .Configure<SerializerOptions>(configureOptions =>
                {
                    configure?.Invoke(configureOptions);
                });
        }

        /// <summary>
        /// Registers the Forge LocalStorage services as singletons. This should only be used in Blazor WebAssembly applications.
        /// Using this in Blazor Server applications will cause unexpected and potentially dangerous behaviour. 
        /// </summary>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddForgeSessionStorageAsSingletonWithNewtonsoftSerializer(this IServiceCollection services)
            => services.AddForgeSessionStorageAsSingletonWithNewtonsoftSerializer(null);

        /// <summary>
        /// Registers the Forge LocalStorage services as singletons. This should only be used in Blazor WebAssembly applications.
        /// Using this in Blazor Server applications will cause unexpected and potentially dangerous behaviour. 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configure"></param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddForgeSessionStorageAsSingletonWithNewtonsoftSerializer(this IServiceCollection services, Action<SerializerOptions> configure)
        {
            return services
                .Replace(new ServiceDescriptor(typeof(ISerializationProvider), typeof(JsonSerializer), ServiceLifetime.Singleton))
                .Configure<SerializerOptions>(configureOptions =>
                {
                    configure?.Invoke(configureOptions);
                });
        }

    }

}
