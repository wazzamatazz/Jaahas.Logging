using Microsoft.Extensions.Logging;

namespace Jaahas.Extensions.Logging.CommonLogging {

    /// <summary>
    /// <see cref="ILoggerProvider"/> implementation that creates <see cref="CommonLoggingLogger"/> 
    /// instances.
    /// </summary>
    public class CommonLoggingLoggerProvider : ILoggerProvider {

        /// <summary>
        /// The Common.Logging log manager.
        /// </summary>
        private readonly Common.Logging.ILogManager _logManager;

        /// <summary>
        /// The external scope provider for the logger provider.
        /// </summary>
        private readonly IExternalScopeProvider _scopeProvider;


        /// <summary>
        /// Creates a new <see cref="CommonLoggingLoggerProvider"/> instance.
        /// </summary>
        /// <param name="logManager">
        ///   The Common.Logging log manager to use. Specify <see langword="null"/> to use a new 
        ///   <see cref="Common.Logging.LogManager"/> instance.
        /// </param>
        /// <param name="scopeProvider">
        ///   The <see cref="IExternalScopeProvider"/> to use.
        /// </param>
        public CommonLoggingLoggerProvider(Common.Logging.ILogManager logManager, IExternalScopeProvider scopeProvider = null) {
            _logManager = logManager ?? new Common.Logging.LogManager();
            _scopeProvider = scopeProvider;
        }


        /// <inheritdoc/>
        public ILogger CreateLogger(string categoryName) {
            return new CommonLoggingLogger(_logManager.GetLogger(categoryName), _scopeProvider);
        }


        /// <inheritdoc/>
        public void Dispose() {
            // Do nothing.
        }

    }
}
