using Microsoft.Extensions.Logging;

namespace Jaahas.Extensions.Logging.CommonLogging {
    public class CommonLoggingLoggerProvider : ILoggerProvider {

        private readonly Common.Logging.ILogManager _logManager;

        private readonly IExternalScopeProvider _scopeProvider;


        public CommonLoggingLoggerProvider(Common.Logging.ILogManager logManager, IExternalScopeProvider scopeProvider = null) {
            _logManager = logManager ?? new Common.Logging.LogManager();
            _scopeProvider = scopeProvider;
        }


        public ILogger CreateLogger(string categoryName) {
            return new CommonLoggingLogger(_logManager.GetLogger(categoryName), _scopeProvider);
        }

        public void Dispose() {
            // Do nothing.
        }
    }
}
