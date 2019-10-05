using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Jaahas.Logging.Extensions.CommonLogging {
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
