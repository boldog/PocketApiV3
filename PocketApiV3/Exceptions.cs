using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketApiV3
{
    public class PocketException : Exception
    {
        public PocketException()
            : base()
        {
        }

        public PocketException(string message)
            : base(message) { }


        public PocketException(string message, Exception innerException)
            : base(message, innerException) { }

        public PocketException(string message, int pocketErrorCode, Exception innerException)
            : base(message, innerException)
        {
            PocketErrorCode = pocketErrorCode;
        }

        public int? PocketErrorCode { get; }
    }

    public class PocketClientException : Exception
    {
        public int? PocketErrorCode { get; set; }

        public string PocketError { get; set; }

        public PocketClientException()
            : base() { }

        public PocketClientException(string message)
            : base(message) { }

        public PocketClientException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    public class PocketJsonException : Exception
    {
        public PocketJsonException()
            : base() { }

        public PocketJsonException(string message)
            : base(message) { }

        public PocketJsonException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
