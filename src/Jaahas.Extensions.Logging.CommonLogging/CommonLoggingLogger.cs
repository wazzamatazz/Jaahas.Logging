using System;
using System.Collections.Generic;

using Common.Logging;
using Microsoft.Extensions.Logging;

namespace Jaahas.Extensions.Logging.CommonLogging {

    /// <summary>
    /// <see cref="ILogger"/> implementation that writes to an underlying <c>Common.Logging</c> 
    /// <see cref="ILog"/>.
    /// </summary>
    public class CommonLoggingLogger : ILogger {

        /// <summary>
        /// The <see cref="ILog"/> to write log messages to.
        /// </summary>
        private readonly ILog _log;

        /// <summary>
        /// Stores scope data.
        /// </summary>
        private readonly IExternalScopeProvider _scopeProvider;


        /// <summary>
        /// Creates a new <see cref="CommonLoggingLogger"/> object.
        /// </summary>
        /// <param name="log">
        ///   The underlying log to write messages to.
        /// </param>
        /// <param name="scopeProvider">
        ///   The <see cref="IExternalScopeProvider"/> service to store scope data in. Can be <see langword="null"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="log"/> is <see langword="null"/>.
        /// </exception>
        public CommonLoggingLogger(ILog log, IExternalScopeProvider scopeProvider = null) {
            _log = log ?? throw new ArgumentNullException(nameof(log));
            _scopeProvider = scopeProvider;
        }


        /// <summary>
        /// Writes a log messages to the underlying logger.
        /// </summary>
        /// <param name="logLevel">
        ///   The log level for the message.
        /// </param>
        /// <param name="message">
        ///   The message.
        /// </param>
        /// <param name="exception">
        ///   The exception associated with the message.
        /// </param>
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


        /// <summary>
        /// Writes a message to the logger.
        /// </summary>
        /// <typeparam name="TState">
        ///   The type of the state parameter associated with the message.
        /// </typeparam>
        /// <param name="logLevel">
        ///   The log level for the message.
        /// </param>
        /// <param name="eventId">
        ///   The event ID associated with the message.
        /// </param>
        /// <param name="state">
        ///   The state value associated with the message.
        /// </param>
        /// <param name="exception">
        ///   The exception associated with the message.
        /// </param>
        /// <param name="formatter">
        ///   A delegate that will format the message based on the <paramref name="state"/> and 
        ///   <paramref name="exception"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="formatter"/> is <see langword="null"/>.
        /// </exception>
        public void Log<TState>(
            Microsoft.Extensions.Logging.LogLevel logLevel, 
            EventId eventId, 
            TState state, 
            Exception exception, 
            Func<TState, Exception, string> formatter
        ) {
            if (formatter == null) {
                throw new ArgumentNullException(nameof(formatter));
            }

            if (!IsEnabled(logLevel)) {
                return;
            }

            var sb = new System.Text.StringBuilder();

            if (_scopeProvider != null) {
                _scopeProvider.ForEachScope((scopeInstance, builder) => {
                    if (scopeInstance is IDictionary<string, object> dict) {
                        sb.Append(System.Text.Json.JsonSerializer.Serialize(dict));
                    }
                    else {
                        sb.Append(System.Text.Json.JsonSerializer.Serialize(new Dictionary<string, object>() {
                            ["None"] = scopeInstance
                        }));
                    }
                    sb.Append(" ");
                }, sb);
            }

            sb.Append(formatter(state, exception));

            LogMessage(logLevel, sb.ToString(), exception);
        }


        /// <summary>
        /// Tests if the logger will accept messages of the specified level.
        /// </summary>
        /// <param name="logLevel">
        ///   The log level.
        /// </param>
        /// <returns>
        ///   <see langword="true"/> if the logger will accept messages at the specified log level, 
        ///   or <see langword="false"/> otherwise.
        /// </returns>
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


        /// <summary>
        /// Begins a scope using the specified state value.
        /// </summary>
        /// <typeparam name="TState">
        ///   The state value type.
        /// </typeparam>
        /// <param name="state">
        ///   The state.
        /// </param>
        /// <returns>
        ///   A disposable scope object.
        /// </returns>
        public IDisposable BeginScope<TState>(TState state) {
            return _scopeProvider?.Push(state) ?? NullScope.Instance;
        }


        /// <summary>
        /// Empty logger scope.
        /// </summary>
        private class NullScope : IDisposable {

            /// <summary>
            /// Singleton instance.
            /// </summary>
            internal static NullScope Instance { get; } = new NullScope();

            /// <summary>
            /// Does nothing!
            /// </summary>
            public void Dispose() {
                // Do nothing.
            }

        }
    }
}
