using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.Utility
{
    public class HearthFieldReturnedNullException : Exception
    {
        private const string DefaultMessage = "A specific field or fields on a found Hearth record returned null";
        public HearthFieldReturnedNullException() : base() { }

        public HearthFieldReturnedNullException(string message) : base(message) { }

        public HearthFieldReturnedNullException(string message, Exception innerException) : base(message, innerException) { }
    }
}
