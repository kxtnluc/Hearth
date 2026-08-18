using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.Utility
{
    public class HearthRecordAlreadyExistsException : Exception
    {
        private const string DefaultMessage = "The payload already exists in Hearth. Could not create.";
        public HearthRecordAlreadyExistsException() : base() { }

        public HearthRecordAlreadyExistsException(string message) : base(message) { }

        public HearthRecordAlreadyExistsException(string message, Exception innerException) : base(message, innerException) { }
    }
}
