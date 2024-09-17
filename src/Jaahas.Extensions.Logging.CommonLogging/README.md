# About

Jaahas.Extensions.Logging.CommonLogging is a `Microsoft.Extensions.Logging.ILoggerProvider` implementation that writes log messages to [Common.Logging](https://github.com/net-commons/common-logging).

This allows libraries and components that write log messages using modern .NET logging infrastructure to be consumed by legacy applications that use Common.Logging without requiring a rewrite of logging code.


# Getting Started

Add the Common.Logging provider to your logger configuration when building your dependency injection container:

```csharp
services.AddLogging(logging => {
    // Remove the default logging providers and add Common.Logging instead
    logging.ClearProviders().AddCommonLogging();
});
```

Refer to the [Common.Logging documentation](https://github.com/net-commons/common-logging) for information on how to configure Common.Logging to log to your destination(s) of choice.


