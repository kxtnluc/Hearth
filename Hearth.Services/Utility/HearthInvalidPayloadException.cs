using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.Utility
{
    public class HearthInvalidPayloadException : Exception
    {
        private const string DefaultMessage = "The payload did not pass the validation method: ";
        public HearthInvalidPayloadException() : base() { }

        public HearthInvalidPayloadException(string message) : base(message) { }

        public HearthInvalidPayloadException(string message, Exception innerException) : base(message, innerException) { }
    }
}
