using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Microsoft.Extensions.Logging;

namespace Jaahas.Logging.Extensions.CommonLogging {
    public class CommonLoggingLogger : ILogger {

        private readonly ILog _log;


        public CommonLoggingLogger(ILog log) {
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }


        private void LogMessage(
            Microsoft.Extensions.Logging.LogLevel logLevel,
            string message,
            Exception exception
        ) {
            switch (logLevel) {
                case Microsoft.Extensions.Logging.LogLevel.Trace:
                    _log.Trace(message, exception);
                    break;
                case Microsoft.Extensions.Logging.LogLevel.Debug:
                    _log.Debug(message, exception);
                    break;
                case Microsoft.Extensions.Logging.LogLevel.Information:
                    _log.Info(message, exception);
                    break;
                case Microsoft.Extensions.Logging.LogLevel.Warning:
                    _log.Warn(message, exception);
                    break;
                case Microsoft.Extensions.Logging.LogLevel.Error:
                    _log.Error(message, exception);
                    break;
                case Microsoft.Extensions.Logging.LogLevel.Critical:
                    _log.Fatal(message, exception);
                    break;
                case Microsoft.Extensions.Logging.LogLevel.None:
                    return;
                default:
                    _log.Debug(message, exception);
                    break;
            }
        }


        public void Log<TState>(
            Microsoft.Extensions.Logging.LogLevel logLevel, 
            EventId eventId, 
            TState state, 
            Exception exception, 
            Func<TState, Exception, string> formatter
        ) {
            if (!IsEnabled(logLevel)) {
                return;
            }

            if (formatter == null) {
                throw new ArgumentNullException(nameof(formatter));
            }

            var message = formatter(state, exception);
            LogMessage(logLevel, message, exception);
        }

        public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel) {
            switch (logLevel) {
                case Microsoft.Extensions.Logging.LogLevel.Trace:
                    return _log.IsTraceEnabled;
                case Microsoft.Extensions.Logging.LogLevel.Debug:
                    return _log.IsDebugEnabled;
                case Microsoft.Extensions.Logging.LogLevel.Information:
                    return _log.IsInfoEnabled;
                case Microsoft.Extensions.Logging.LogLevel.Warning:
                    return _log.IsWarnEnabled;
                case Microsoft.Extensions.Logging.LogLevel.Error:
                    return _log.IsErrorEnabled;
                case Microsoft.Extensions.Logging.LogLevel.Critical:
                    return _log.IsFatalEnabled;
                case Microsoft.Extensions.Logging.LogLevel.None:
                    return !(_log.IsTraceEnabled || _log.IsDebugEnabled || _log.IsInfoEnabled || _log.IsWarnEnabled || _log.IsErrorEnabled || _log.IsFatalEnabled);
                default:
                    return false;
            }
        }

        public IDisposable BeginScope<TState>(TState state) {
            return new CommonLoggingLoggerScope<TState>(state);
        }
    }
}
