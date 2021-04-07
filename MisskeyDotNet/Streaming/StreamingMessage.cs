using System;
using Newtonsoft.Json.Linq;

namespace MisskeyDotNet
{
    public class StreamingMessage
    {
        public string Type { get; } = "";

        public StreamingMessage(string type, string body)
        {
            Type = type;
            this.body = JToken.Parse(body);
        }

        public T GetBodyAs<T>()
        {
            return body.ToObject<T>()!;
        }

        public string GetBodyAsJson()
        {
            return body.ToString(Newtonsoft.Json.Formatting.None);
        }

        public static StreamingMessage FromBodyOf(StreamingMessage mes)
        {
            var body = mes.body;
            if (body is not JObject jobj) throw new ArgumentException("Body is not an object type.", nameof(mes));

            return new StreamingMessage(
                jobj.Value<string>("type"), 
                jobj["body"]?.ToString() ?? ""
            );
        }

        protected internal readonly JToken body;
    }
}