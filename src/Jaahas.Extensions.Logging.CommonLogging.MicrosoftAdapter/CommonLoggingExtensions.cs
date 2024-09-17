using System;

using Jaahas.CommonLogging.MicrosoftAdapter;

using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection {

    /// <summary>
    /// Extensions for registering and using <c>Common.Logging</c> with <see cref="IServiceCollection"/> 
    /// and <see cref="IServiceProvider"/>.
    /// </summary>
    public static class CommonLoggingExtensions {

        /// <summary>
        /// Registers a singleton <see cref="Common.Logging.ILoggerFactoryAdapter"/> service that 
        /// uses <see cref="MicrosoftLoggerFactoryAdapter"/> as its implementation type.
        /// </summary>
        /// <param name="services">
        ///   The <see cref="IServiceCollection"/>.
        /// </param>
        /// <returns>
        ///   The <see cref="IServiceCollection"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="services"/> is <see langword="null"/>.
        /// </exception>
        public static IServiceCollection AddMicrosoftCommonLoggingAdapter(this IServiceCollection services) {
            if (services == null) {
                throw new ArgumentNullException(nameof(services));
            }

            services.TryAddSingleton<MicrosoftLoggerFactoryAdapter>();
            services.TryAddSingleton<Common.Logging.ILogManager, Common.Logging.LogManager>();

            return services;
        }


        /// <summary>
        /// Configures the static <see cref="Common.Logging.LogManager.Adapter"/> property to use 
        /// the registered <see cref="MicrosoftLoggerFactoryAdapter"/>.
        /// </summary>
        /// <param name="provider">
        ///   The <see cref="IServiceProvider"/>.
        /// </param>
        /// <returns>
        ///   The <see cref="IServiceProvider"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="provider"/> is <see langword="null"/>.
        /// </exception>
        public static IServiceProvider UseMicrosoftCommonLoggingAdapter(this IServiceProvider provider) {
            if (provider == null) {
                throw new ArgumentNullException(nameof(provider));
            }

            var adapter = provider.GetRequiredService<MicrosoftLoggerFactoryAdapter>();
            Common.Logging.LogManager.Adapter = adapter;
            return provider;
        }

    }
}
