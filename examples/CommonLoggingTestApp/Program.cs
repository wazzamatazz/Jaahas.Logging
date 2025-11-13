using System;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CommonLoggingTestApp {
    class Program {

        static void Main() {
            var builder = Host.CreateApplicationBuilder();

            builder.Logging.ClearProviders().AddCommonLogging();

            using (var app = builder.Build()) {
                var logger = app.Services.GetRequiredService<ILoggerFactory>().CreateLogger(typeof(Program));

                var logDelegates = new Action[] {
                    () => logger.LogTrace("Trace message!"),
                    () => logger.LogDebug("Debug message!"),
                    () => logger.LogInformation("Information message!"),
                    () => logger.LogWarning("Warning message!"),
                    () => logger.LogError("Error message!"),
                    () => logger.LogCritical("Critical message!"),
                };

                foreach (var item in logDelegates) {
                    item.Invoke();

                    using (logger.BeginScope("with_scope")) {
                        item.Invoke();
                    }

                    using (logger.BeginScope("with_scope"))
                    using (logger.BeginScope(new Dictionary<string, string>() {
                        ["details"] = "with_delegated_scope"
                    })) {
                        item.Invoke();
                    }
                }
            }
        }

    }
}
