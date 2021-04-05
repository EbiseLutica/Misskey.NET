using System;

namespace MisskeyDotNet
{
    public class StreamingResponse
    {
        public string Type { get; set; } = "";
        public string Body { get; set; } = "";

        public T BodyToObject<T>()
        {
            return Misskey.Deserialize<T>(Body);
        }
    }
}