using System;
using Microsoft.Extensions.Logging;

namespace CommonLoggingTestApp {
    class Program {

        static void Main() {
            var factory = Jaahas.Extensions.Logging.CommonLogging.CommonLoggingLoggerFactory.Default;
            var logger = factory.CreateLogger(typeof(Program));

            logger.LogTrace("Trace message!");
            logger.LogDebug("Debug message!");
            logger.LogInformation("Information message!");
            logger.LogWarning("Warning message!");
            logger.LogError("Error message!", new ApplicationException("ERROR"));
            logger.LogCritical("Critical message!", new ApplicationException("CRITICAL ERROR"));
        }

    }
}
