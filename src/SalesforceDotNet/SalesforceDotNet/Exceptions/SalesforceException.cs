using System;

namespace Semptra.SalesforceDotNet.Exceptions
{
    public class SalesforceException : Exception
    {
        public SalesforceException() : base() { }

        public SalesforceException(string message) : base(message) { }

        public SalesforceException(string message, Exception innerException) : base(message, innerException) { }
    }
}
