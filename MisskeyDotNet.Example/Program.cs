using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MisskeyDotNet.Example
{
    class Program
    {
        static async Task Main()
        {
            Misskey io;
            if (File.Exists("credential"))
            {
                io = Misskey.Import(await File.ReadAllTextAsync("credential"));
            }
            else
            {
                var miAuth = new MiAuth("misskey.io", "Misskey.NET", null, null, Permission.All);
                if (!miAuth.TryOpenBrowser())
                {
                    Console.WriteLine("次のURLをお使いのウェブブラウザーで開き、認証を完了させてください。");
                    Console.WriteLine(miAuth.Url);
                }
                Console.WriteLine("認可が完了したら、ENTER キーを押してください。");
                Console.ReadLine();

                io = await miAuth.CheckAsync();
                await File.WriteAllTextAsync("credential", io.Export());
            }

            // await FetchReactions(io);
            // await SummonError(io);
            // await GetMeta(io);
            // await FetchInstances();
            await TestWebSocket(io);
        }

        private static async ValueTask FetchReactions(Misskey mi)
        {
            var note = await mi.ApiAsync<Note>("notes/show", new
            {
                noteId = "7zzafqsm9a",
            });

            Console.WriteLine("Note ID: " + note.Id);
            Console.WriteLine("Note Created At: " + note.CreatedAt);
            Console.WriteLine("CW: " + note.Cw ?? "null");
            Console.WriteLine("Body: " + note.Text ?? "null");
            Console.WriteLine("Reactions: ");
            int c = 0;
            foreach (var kv in note.Reactions)
            {
                Console.Write(" {0}: {1}", kv.Key, kv.Value);
                c++;
                if (c == 5)
                {
                    c = 0;
                    Console.WriteLine();
                }
            }
        }
        private static async ValueTask SummonError(Misskey mi)
        {
            try
            {
                var note = await mi.ApiAsync<Note>("notes/show", new
                {
                    noteId = "m",
                });
            }
            catch (MisskeyApiException e)
            {
                Console.WriteLine(e.Error.Message);
            }
        }
        private static async ValueTask GetMeta(Misskey mi)
        {
            try
            {
                var meta = await mi.ApiAsync<Meta>("meta");
                Console.WriteLine($"インスタンス名: {meta.Name}");
                Console.WriteLine($"バージョン: {meta.Version}");
                Console.WriteLine($"説明: {meta.Description}");
                Console.WriteLine($"管理者: {meta.MaintainerName}");
                Console.WriteLine($"管理者メール: {meta.MaintainerEmail}");
                Console.WriteLine($"LTL: {(meta.DisableLocalTimeline ? "いいえ" : "はい")}");
                Console.WriteLine($"GTL: {(meta.DisableGlobalTimeline ? "いいえ" : "はい")}");
                Console.WriteLine($"登録可能: {(meta.DisableRegistration ? "いいえ" : "はい")}");
                Console.WriteLine($"メール: {(meta.EnableEmail ? "はい" : "いいえ")}");
                Console.WriteLine($"Twitter認証: {(meta.EnableTwitterIntegration ? "はい" : "いいえ")}");
                Console.WriteLine($"Discord認証: {(meta.EnableDiscordIntegration ? "はい" : "いいえ")}");
                Console.WriteLine($"GitHub認証: {(meta.EnableGithubIntegration ? "はい" : "いいえ")}");
            }
            catch (MisskeyApiException e)
            {
                Console.WriteLine(e.Error.Message);
            }
        }
        private static async ValueTask FetchInstances()
        {
            var res = await Misskey.JoinMisskeyInstancesApiAsync();
            Console.WriteLine($"最終更新: {res.Date}");
            Console.WriteLine($"インスタンス数: {res.Stats.InstancesCount}");
            Console.WriteLine($"インスタンス一覧:");
            res.InstancesInfos.Select(meta => " " + meta.Url).ToList().ForEach(Console.WriteLine);
        }
        private static async ValueTask TestWebSocket(Misskey mi)
        {
            using var st = await mi.OpenStreamingAsync();

            st.Received += (res) =>
            {
                Console.WriteLine(res.Body);
            };

            st.Send("connect", new
            {
                channel = "homeTimeline",
                id = "1",
            });
            
            await Task.Delay(60000);
        }
    }
}
