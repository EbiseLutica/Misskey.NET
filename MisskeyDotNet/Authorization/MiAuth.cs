using System;
using System.Threading.Tasks;

namespace MisskeyDotNet
{
    public sealed class MiAuth
    {
        public string Host { get; }

        public string? Name { get; }

        public string? IconUrl { get; }

        public string? CallbackUrl { get; }

        public string Uuid { get; }

        public Permission[] Permissions { get; } = new Permission[0];

        public string Url { get; }

        public MiAuth(string host, string? name, string? iconUrl = null, string? callbackUrl = null, params Permission[] permissions)
        {
            Host = host;
            Name = name;
            IconUrl = iconUrl;
            CallbackUrl = callbackUrl;
            Permissions = permissions;
            Uuid = Guid.NewGuid().ToString();
            stringifiedPermissions = string.Join(',', Permissions.ToStringArray());

            Url = "https://" + Host + "/miauth/" + Uuid;

            var query = new
            {
                name = Name,
                icon = IconUrl,
                callback = CallbackUrl,
                permission = stringifiedPermissions,
            }.ToQueryString();
            if (query != null)
                Url += "?" + query;
        }

        public bool TryOpenBrowser()
        {
            try
            {
                OpenBrowser();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void OpenBrowser()
        {
            Helper.OpenUrl(Url);
        }

        public async ValueTask<Misskey> CheckAsync()
        {
            var res = await new Misskey(Host).ApiAsync("miauth/" + Uuid + "/check");

            if (res["token"] is string token)
                return new Misskey(Host, token);

            throw new InvalidOperationException("Failed to check MiAuth session");
        }

        private string stringifiedPermissions;
    }
}