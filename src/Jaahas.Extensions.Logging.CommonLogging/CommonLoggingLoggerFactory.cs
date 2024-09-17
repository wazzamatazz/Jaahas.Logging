using System;

using Microsoft.Extensions.Logging;

namespace Jaahas.Extensions.Logging.CommonLogging {

    /// <summary>
    /// <see cref="ILoggerFactory"/> implementation that uses <c>Common.Logging</c> for logging.
    /// </summary>
    public class CommonLoggingLoggerFactory : ILoggerFactory {

        /// <summary>
        /// Specifies whether the object has been disposed.
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// The <see cref="CommonLoggingLoggerProvider"/> instance to use.
        /// </summary>
        private readonly CommonLoggingLoggerProvider _provider;

        /// <summary>
        /// The default <see cref="CommonLoggingLoggerFactory"/> instance.
        /// </summary>
        private static readonly Lazy<CommonLoggingLoggerFactory> s_default = new Lazy<CommonLoggingLoggerFactory>(() => new CommonLoggingLoggerFactory(), System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// The default <see cref="CommonLoggingLoggerFactory"/> instance.
        /// </summary>
        public static CommonLoggingLoggerFactory Default { get { return s_default.Value; } }


        /// <summary>
        /// Creates a new <see cref="CommonLoggingLoggerFactory"/> instance using a default 
        /// <see cref="CommonLoggingLoggerProvider"/> instance.
        /// </summary>
        public CommonLoggingLoggerFactory() : this(null) { }


        /// <summary>
        /// Creates a new <see cref="CommonLoggingLoggerFactory"/> instance using the specified 
        /// <see cref="CommonLoggingLoggerProvider"/> instance.
        /// </summary>
        /// <param name="provider">
        ///   The <see cref="CommonLoggingLoggerProvider"/> instance to use. Specify <see langword="null"/> 
        ///   to create a new provider.
        /// </param>
        public CommonLoggingLoggerFactory(CommonLoggingLoggerProvider provider) {
            _provider = provider ?? new CommonLoggingLoggerProvider(new Common.Logging.LogManager(), new LoggerExternalScopeProvider());
        }


        /// <inheritdoc/>
        public ILogger CreateLogger(string categoryName) {
            if (_disposed) {
                throw new ObjectDisposedException(GetType().FullName);
            }

            return _provider.CreateLogger(categoryName);
        }


        /// <inheritdoc/>
        public void AddProvider(ILoggerProvider provider) {
            // Do nothing.
        }


        /// <inheritdoc/>
        public void Dispose() {
            if (_disposed) {
                return;
            }

            _provider.Dispose();

            _disposed = true;
            GC.SuppressFinalize(this);
        }

    }
}
