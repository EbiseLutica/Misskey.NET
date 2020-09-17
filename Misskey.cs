using System;
using System.Net.Http;

namespace MisskeyDotNet
{
    public class Misskey
    {
        public string Host { get; }
        public string? Token { get; }

        public Misskey(string host)
        {
            Host = host;
        }

        public string Serialize()
        {
            var s = "Host=" + Host;
            if (Token is string)
            {
                s += "\nToken=" + Token;
            }
            return s;
        }
    }
}
