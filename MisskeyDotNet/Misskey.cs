using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MisskeyDotNet
{
    public sealed class Misskey
    {
        public string Host { get; }
        public string? Token { get; }

        public static HttpClient Http { get; } = new HttpClient();

        public Misskey(string host)
        {
            Host = host;
        }

        public Misskey(string host, string? token)
        {
            Host = host;
            Token = token;
        }

        public ValueTask<Dictionary<string, object>> ApiAsync(string endPoint, object? parameters = null)
        {
            return ApiAsync<Dictionary<string, object>>(endPoint, parameters);
        }

        public async ValueTask<T> ApiAsync<T>(string endPoint, object? parameters = null)
        {
            var dict = parameters.ConvertToDictionary();

            if (Token != null)
                dict["i"] = Token;

            var json = JsonConvert.SerializeObject(dict);
            var res = await Http.PostAsync(GetApiUrl(endPoint), new StringContent(json));
            var content = await res.Content.ReadAsStringAsync();;
            if (res.IsSuccessStatusCode)
            {
                return Deserialize<T>(content);
            }
            switch (res.StatusCode)
            {
                case HttpStatusCode.InternalServerError:
                case HttpStatusCode.BadRequest:
                    throw new MisskeyApiException(
                        res.StatusCode == HttpStatusCode.InternalServerError
                        ? Deserialize<Error>(content)
                        : Deserialize<ErrorWrapper>(content).Error
                    );
                case HttpStatusCode.Unauthorized:
                    throw new MisskeyApiRequireTokenException();
                default:
                    throw new HttpException(res.StatusCode);
            }
        }

        public string Export()
        {
            var s = "Host=" + Host;
            if (Token is string)
            {
                s += "\nToken=" + Token;
            }
            return s;
        }

        public static Misskey Import(string serialized)
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

        private T Deserialize<T>(string s)
        { 
            return JsonConvert.DeserializeObject<T>(s, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
            })!;
        }

        private string GetApiUrl(string endPoint)
        {
            return "https://" + Host + "/api/" + endPoint;
        }

        private readonly DefaultContractResolver contractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        };
    }
}
