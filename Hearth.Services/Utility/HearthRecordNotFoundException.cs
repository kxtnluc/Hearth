using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.Utility
{
    public class HearthRecordNotFoundException : Exception
    {
        private const string DefaultMessage = "The requested record was not found in Hearth";
        public HearthRecordNotFoundException() : base() { }

        public HearthRecordNotFoundException(string message) : base(message) { }

        public HearthRecordNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
