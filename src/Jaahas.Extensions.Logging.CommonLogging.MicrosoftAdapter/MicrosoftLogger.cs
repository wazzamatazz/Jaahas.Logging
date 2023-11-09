using System;
using Microsoft.Extensions.Logging;

namespace Jaahas.CommonLogging.MicrosoftAdapter {

    /// <summary>
    /// <c>Common.Logging</c> logger that writes log messages to an <see cref="ILogger"/>.
    /// </summary>
    public class MicrosoftLogger : Common.Logging.Factory.AbstractLogger {

        /// <summary>
        /// The underlying logger to wrote messages to.
        /// </summary>
        private readonly ILogger _logger;


        /// <summary>
        /// Gets a flag that indicates if trace-level messages can be written to the logger.
        /// </summary>
        public override bool IsTraceEnabled {
            get { return _logger.IsEnabled(LogLevel.Trace); }
        }


        /// <summary>
        /// Gets a flag that indicates if debug-level messages can be written to the logger.
        /// </summary>
        public override bool IsDebugEnabled {
            get { return _logger.IsEnabled(LogLevel.Debug); }
        }


        /// <summary>
        /// Gets a flag that indicates if info-level messages can be written to the logger.
        /// </summary>
        public override bool IsInfoEnabled {
            get { return _logger.IsEnabled(LogLevel.Information); }
        }


        /// <summary>
        /// Gets a flag that indicates if warning-level messages can be written to the logger.
        /// </summary>
        public override bool IsWarnEnabled {
            get { return _logger.IsEnabled(LogLevel.Warning); }
        }


        /// <summary>
        /// Gets a flag that indicates if error-level messages can be written to the logger.
        /// </summary>
        public override bool IsErrorEnabled {
            get { return _logger.IsEnabled(LogLevel.Error); }
        }


        /// <summary>
        /// Gets a flag that indicates if fatal-level messages can be written to the logger.
        /// </summary>
        public override bool IsFatalEnabled {
            get { return _logger.IsEnabled(LogLevel.Critical); }
        }


        /// <summary>
        /// Creates a new <see cref="MicrosoftLogger"/> object.
        /// </summary>
        /// <param name="logger">
        ///   The underlying Microsoft logger to write messages to.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="logger"/> is <see langword="null"/>.
        /// </exception>
        public MicrosoftLogger(ILogger logger) {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        /// <summary>
        /// Writes a log message.
        /// </summary>
        /// <param name="level">
        ///   The log level for the message.
        /// </param>
        /// <param name="message">
        ///   The message.
        /// </param>
        /// <param name="exception">
        ///   The exception associated with the message.
        /// </param>
        protected override void WriteInternal(Common.Logging.LogLevel level, object message, Exception exception) {
            switch (level) {
                case Common.Logging.LogLevel.Trace:
                    _logger.LogTrace(exception, Convert.ToString(message));
                    break;
                case Common.Logging.LogLevel.Debug:
                    _logger.LogDebug(exception, Convert.ToString(message));
                    break;
                case Common.Logging.LogLevel.Info:
                    _logger.LogInformation(exception, Convert.ToString(message));
                    break;
                case Common.Logging.LogLevel.Error:
                    _logger.LogError(exception, Convert.ToString(message));
                    break;
                case Common.Logging.LogLevel.Warn:
                    _logger.LogWarning(exception, Convert.ToString(message));
                    break;
                case Common.Logging.LogLevel.Fatal:
                    _logger.LogCritical(exception, Convert.ToString(message));
                    break;
                default:
                    _logger.LogDebug(exception, Convert.ToString(message));
                    break;
            }
        }

    }
}
