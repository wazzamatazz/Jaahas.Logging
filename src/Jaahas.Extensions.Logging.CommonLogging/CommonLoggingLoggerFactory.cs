using System;
using Microsoft.Extensions.Logging;

namespace Jaahas.Extensions.Logging.CommonLogging {
    public class CommonLoggingLoggerFactory : ILoggerFactory {

        private bool _isDisposed;

        private readonly CommonLoggingLoggerProvider _provider;

        private static readonly Lazy<CommonLoggingLoggerFactory> _default = new Lazy<CommonLoggingLoggerFactory>(() => new CommonLoggingLoggerFactory(), System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);

        public static CommonLoggingLoggerFactory Default { get { return _default.Value; } }


        public CommonLoggingLoggerFactory() : this(null) { }


        public CommonLoggingLoggerFactory(CommonLoggingLoggerProvider provider) {
            _provider = provider ?? new CommonLoggingLoggerProvider(new Common.Logging.LogManager());
        }


        public ILogger CreateLogger(string categoryName) {
            if (_isDisposed) {
                throw new ObjectDisposedException(GetType().FullName);
            }

            return _provider.CreateLogger(categoryName);
        }

        public void AddProvider(ILoggerProvider provider) {
            if (_isDisposed) {
                throw new ObjectDisposedException(GetType().FullName);
            }

            // Do nothing.
        }


        private void Dispose(bool disposing) {
            if (disposing) {
                _provider.Dispose();
            }
        }


        public void Dispose() {
            if (_isDisposed) {
                return;
            }

            Dispose(true);
            _isDisposed = true;
            GC.SuppressFinalize(this);
        }


        ~CommonLoggingLoggerFactory() {
            Dispose(false);
        }

    }
}
