using Common.Logging;
using Common.Logging.Factory;

using Microsoft.Extensions.Logging;

namespace Jaahas.CommonLogging.MicrosoftAdapter {

    /// <summary>
    /// <c>Common.Logging</c> logger factory adapter that redirects logging to <c>Microsoft.Extensions.Logging</c>.
    /// </summary>
    public class MicrosoftLoggerFactoryAdapter : AbstractCachingLoggerFactoryAdapter {

        /// <summary>
        /// The Microsoft logger factory to use.
        /// </summary>
        private readonly ILoggerFactory _loggerFactory;


        /// <summary>
        /// Creates a new <see cref="MicrosoftLoggerFactoryAdapter"/> object.
        /// </summary>
        /// <param name="loggerFactory">
        ///   The underlying logger factory to use. Specify <see langword="null"/> to use the 
        ///   <see cref="Microsoft.Extensions.Logging.Abstractions.NullLoggerFactory"/> factory.
        /// </param>
        public MicrosoftLoggerFactoryAdapter(ILoggerFactory loggerFactory) {
            _loggerFactory = loggerFactory ?? Microsoft.Extensions.Logging.Abstractions.NullLoggerFactory.Instance;
        }


        /// <summary>
        /// Creates a new logger with the specified name.
        /// </summary>
        /// <param name="name">
        ///   The logger name.
        /// </param>
        /// <returns>
        ///   The logger.
        /// </returns>
        protected override ILog CreateLogger(string name) {
            return new MicrosoftLogger(_loggerFactory.CreateLogger(name));
        }
    }
}
