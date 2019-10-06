using Microsoft.Extensions.Logging;

namespace Jaahas.Extensions.Logging.CommonLogging {
    public class CommonLoggingLoggerProvider : ILoggerProvider {

        private readonly Common.Logging.ILogManager _logManager;


        public CommonLoggingLoggerProvider(Common.Logging.ILogManager logManager) {
            _logManager = logManager ?? new Common.Logging.LogManager();
        }


        public ILogger CreateLogger(string categoryName) {
            return new CommonLoggingLogger(_logManager.GetLogger(categoryName));
        }

        public void Dispose() {
            // Do nothing.
        }
    }
}
