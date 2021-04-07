using System;
using Newtonsoft.Json.Linq;

namespace MisskeyDotNet
{
    public class ChannelStreamingMessage : StreamingMessage
    {
        public string Id { get; } = "";

        public ChannelStreamingMessage(string type, string id, string body): base(type, body)
        {
            Id = id;
        }

        public static ChannelStreamingMessage FromBodyOf(StreamingMessage mes)
        {
            var body = mes.body;
            if (body is not JObject jobj) throw new ArgumentException("Body is not an object type.", nameof(mes));

            return new ChannelStreamingMessage(
                jobj.Value<string>("type"),
                jobj.Value<string>("id"),
                jobj["body"]?.ToString() ?? ""
            );
        }
    }
}