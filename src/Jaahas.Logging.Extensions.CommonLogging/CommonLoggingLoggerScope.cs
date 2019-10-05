using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jaahas.Logging.Extensions.CommonLogging {
    internal class CommonLoggingLoggerScope<TState> : IDisposable {

        private readonly TState _state;


        public CommonLoggingLoggerScope(TState state) {
            _state = state;
        }


        public void Dispose() {
            // Do nothing.
        }
    }
}
