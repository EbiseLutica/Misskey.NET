using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SuperSocket.ClientEngine;
using WebSocket4Net;

namespace MisskeyDotNet
{
    public class StreamingContext : IDisposable
    {
        private StreamingContext(string url)
        {
            sock = new WebSocket(url);
            sock.MessageReceived += SockMessageReceived;
            sock.Error += SockError;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        internal static async ValueTask<StreamingContext> InitializeAsync(Misskey mi)
        {
            var url = $"wss://{mi.Host}/streaming";
            if (mi.Token is not null)
            {
                url += "?i=" + mi.Token;
            }

            var ctx = new StreamingContext(url);

            await ctx.sock.OpenAsync();

            return ctx;
        }

        public void Send(string type, object body)
        {
            var obj = JsonConvert.SerializeObject(new { type, body });
            Helper.Debug("Sent " + obj);
            sock.Send(obj);
        }

        protected virtual void Dispose(bool disposing)
        {
            sock.CloseAsync();
            Helper.Debug($"Closed socket.");
        }

        protected virtual void OnReceived(StreamingMessage res)
        {
            Received?.Invoke(res);
        }

        private void SockMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            // メッセージは必ずJSONであることを前提としている
            // 将来的にMisskeyがJSON以外を平気で送るようになると死ぬ
            var obj = JObject.Parse(e.Message);
            var type = obj["type"]?.ToObject<string>();
            var body = obj["body"]?.ToString(Formatting.None);
            Helper.Debug($"Received message type: " + type);
            if (type is not null && body is not null)
            {
                OnReceived(new StreamingMessage(type, body));
            }
        }

        private async void SockError(object sender, ErrorEventArgs e)
        {
            // 5秒後に再接続
            Helper.Debug($"Socket Error. {e.Exception.GetType().Name}: {e.Exception.Message}");
            Helper.Debug("Reconnect after 5 seconds...");
            await Task.Delay(5000);
            await sock.OpenAsync();
            Helper.Debug("Success!");
        }

        public event StreamingReceivedEventHandler? Received;

        private readonly WebSocket sock;
    }

    public delegate void StreamingReceivedEventHandler(StreamingMessage res);
}