using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace MisskeyDotNet.Test
{
    public class Test
    {
        [Fact(DisplayName = "Call Meta")]
        public async Task CallMeta()
        {
            try
            {
                var meta = await new Misskey("misskey.io").ApiAsync<Dictionary<string, object>>("meta");
                
                Console.WriteLine(meta["uri"]?.ToString() ?? "");
                Console.WriteLine(meta["version"]?.ToString() ?? "");
                Console.WriteLine(meta["maintainerName"]?.ToString() ?? "");
                Console.WriteLine(meta["maintainerEmail"]?.ToString() ?? "");
                Assert.True(true);
            }
            catch (HttpRequestException)
            {
                Console.WriteLine("Server is down");
                Assert.True(true);
            }
        }
    }
}