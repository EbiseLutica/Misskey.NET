using System;
using System.Runtime.Serialization;

namespace MisskeyDotNet
{
    [Serializable]
    internal class MisskeyApiRequireTokenException : Exception
    {
        public MisskeyApiRequireTokenException()
        {
        }

        public MisskeyApiRequireTokenException(string message) : base(message)
        {
        }

        public MisskeyApiRequireTokenException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MisskeyApiRequireTokenException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}