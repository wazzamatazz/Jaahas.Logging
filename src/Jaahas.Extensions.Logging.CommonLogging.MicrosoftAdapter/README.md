# About

Jaahas.Extensions.Logging.CommonLogging.MicrosoftAdapter is a [Common.Logging](https://github.com/net-commons/common-logging) `ILoggerFactoryAdapter` that allows Common.Logging to write messages to `Microsoft.Extensions.Logging.ILogger` instances.

This allows e.g. legacy ASP.NET applications that use Common.Logging to hook into the modern .NET logging infrastructure without requiring a rewrite of logging code.


# Getting Started

Register the Microsoft.Extensions.Logging adapter for Common.Logging when configuring your dependency injection container and then configure the static `Common.Logging.LogManager` class to use the adapter:

```csharp
var builder = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) => {
        services.AddMicrosoftCommonLoggingAdapter();
    });

var app = builder.Build();

app.Services.UseMicrosoftCommonLoggingAdapter();
```

You can also request the `Common.Logging.ILogManager` service from the dependency injection container to get a log manager instance instead of calling static members on `Common.Logging.LogManager`:

```csharp
var logManager = app.Services.GetRequiredService<Common.Logging.ILogManager>();
```

Log manager instances retrieved in this way will use the Microsoft.Extensions.Logging adapter as long as the `UseMicrosoftCommonLoggingAdapter` extension is called after the service provider has been built.
