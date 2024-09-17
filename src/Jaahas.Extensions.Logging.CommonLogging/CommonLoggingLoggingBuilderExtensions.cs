using System;

using Jaahas.Extensions.Logging.CommonLogging;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.Logging {

    /// <summary>
    /// Extensions for registering <c>Common.Logging</c> with the <see cref="ILoggingBuilder"/>.
    /// </summary>
    public static class CommonLoggingLoggingBuilderExtensions {

        /// <summary>
        /// Adds a <c>Common.Logging</c> logger provider.
        /// </summary>
        /// <param name="builder">
        ///   The <see cref="ILoggingBuilder"/>.
        /// </param>
        /// <returns>
        ///   The <see cref="ILoggingBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        public static ILoggingBuilder AddCommonLogging(this ILoggingBuilder builder) {
            if (builder == null) {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Services.TryAddSingleton<Common.Logging.ILogManager, Common.Logging.LogManager>();
            builder.AddCommonLoggingLoggerProvider();

            return builder;
        }


        /// <summary>
        /// Adds a <c>Common.Logging</c> logger provider.
        /// </summary>
        /// <param name="builder">
        ///   The <see cref="ILoggingBuilder"/>.
        /// </param>
        /// <param name="logManager">
        ///   The <see cref="Common.Logging.ILogManager"/> to use when creating loggers.
        /// </param>
        /// <returns>
        ///   The <see cref="ILoggingBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="builder"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="logManager"/> is <see langword="null"/>.
        /// </exception>
        public static ILoggingBuilder AddCommonLogging(this ILoggingBuilder builder, Common.Logging.ILogManager logManager) {
            if (builder == null) {
                throw new ArgumentNullException(nameof(builder));
            }
            if (logManager == null) {
                throw new ArgumentNullException(nameof(logManager));
            }

            builder.Services.TryAddSingleton(logManager);
            builder.AddCommonLoggingLoggerProvider();

            return builder;
        }


        /// <summary>
        /// Registers <see cref="CommonLoggingLoggerProvider"/> with the <see cref="ILoggingBuilder"/>.
        /// </summary>
        /// <param name="builder">
        ///   The <see cref="ILoggingBuilder"/>.
        /// </param>
        private static void AddCommonLoggingLoggerProvider(this ILoggingBuilder builder) {
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider>(provider => ActivatorUtilities.CreateInstance<CommonLoggingLoggerProvider>(provider, provider.GetService<IExternalScopeProvider>() ?? new LoggerExternalScopeProvider())));
        }

    }
}
