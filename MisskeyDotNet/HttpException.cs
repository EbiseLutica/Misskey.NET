using System;
using System.Net;
using System.Runtime.Serialization;

namespace MisskeyDotNet
{
    [Serializable]
    internal class HttpException : Exception
    {
        public HttpStatusCode Code { get; }
        public HttpException(HttpStatusCode code) : base($"HTTP Error: {(int)code} {code}")
        {
            Code = code;
        }
    }
}