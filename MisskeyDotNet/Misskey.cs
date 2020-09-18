using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;
using System.Net;

namespace MisskeyDotNet
{
    public class Misskey
    {
        public string Host { get; }
        public string? Token { get; }

        public static HttpClient Http { get; } = new HttpClient();

        public Misskey(string host)
        {
            Host = host;
        }

        private Misskey(string host, string? token)
        {
            Host = host;
            Token = token;
        }

        public async ValueTask<T> ApiAsync<T>(string endPoint, object? parameters = null)
        {
            var p = parameters ?? new { };

            var dict = new Dictionary<string, object?>();
            foreach (PropertyDescriptor d in TypeDescriptor.GetProperties(p))
                dict[d.Name] = d.GetValue(p);

            if (Token != null)
                dict["i"] = Token;

            var json = JsonSerializer.Serialize(dict);
            var res = await Http.PostAsync(GetApiUrl(endPoint), new StringContent(json));
            if (res.IsSuccessStatusCode)
            {
                return await DeserializeAsync<T>(await res.Content.ReadAsStreamAsync());
            }
            if (res.StatusCode == HttpStatusCode.BadRequest || res.StatusCode == HttpStatusCode.InternalServerError)
            {
                var err = await DeserializeAsync<Error>(await res.Content.ReadAsStreamAsync());
                throw new MisskeyApiException(err);
            }
            else if (res.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new MisskeyApiRequireTokenException();
            }
            else
            {
                throw new HttpException(res.StatusCode);
            }
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

        public static Misskey Deserialize(string serialized)
        {
            var o = serialized.Split('\n').Select(s => s.Split('=', 2)).Select(a => (key: a[0], value: a[1]));
            var host = "";
            string? token = null;

            foreach (var (key, value) in o)
            {
                switch (key)
                {
                    case "Host":
                        host = value;
                        break;
                    case "Token":
                        token = value;
                        break;
                }
            }

            return new Misskey(host, token);
        }

        private ValueTask<T> DeserializeAsync<T>(Stream s)
        { 
            return JsonSerializer.DeserializeAsync<T>(s, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });
        }

        private string GetApiUrl(string endPoint)
        {
            return "https://" + Host + "/api/" + endPoint;
        }
    }
}
