using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MisskeyDotNet.Example
{
    partial class Program
    {
        [Demo("Fetch Reactions")]
        private static async ValueTask TestFetchReactions()
        {
            var mi = await ConnectAsync();

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

        [Demo("API Error")]
        private static async ValueTask TestApiError()
        {
            var mi = await ConnectAsync();

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

        [Demo("Fetch Meta")]
        private static async ValueTask TestFetchMeta()
        {
            var mi = await ConnectAsync();

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

        [Demo("Call JoinMisskey")]
        private static async ValueTask TestJoinMisskey()
        {
            var res = await Misskey.JoinMisskeyInstancesApiAsync();
            Console.WriteLine($"最終更新: {res.Date}");
            Console.WriteLine($"インスタンス数: {res.Stats.InstancesCount}");
            Console.WriteLine($"インスタンス一覧:");
            res.InstancesInfos.Select(meta => " " + meta.Url).ToList().ForEach(Console.WriteLine);
        }

        [Demo("Test WebSocket")]
        private static async ValueTask TestWebSocket()
        {
            var mi = await ConnectAsync();

            using var st = await mi.OpenStreamingAsync();

            void PrintNote(Note note)
            {
                var user = note.User;
                var acct = user.Username;
                if (user.Host != null) acct += "@" + user.Host;

                Console.Write($"{user.Name ?? user.Username} @{acct} {note.CreatedAt:ddd HH:mm}: ");
                if (note.Cw != null) Console.Write(note.Cw + " [もっと見る] ");
                Console.Write(note.Text);
                if (note.Files?.Length > 0)
                {
                    Console.Write($"（{note.Files.Length}件のファイル）");
                }
            }

            st.Received += (res) =>
            {
                if (res.Type == "channel")
                {
                    var channel = ChannelStreamingMessage.FromBodyOf(res);
                    Helper.Debug($"Received Channel(id:{channel.Id}) Message Type=\"{channel.Type}\"");

                    switch (channel.Type)
                    {
                        case "note": 
                        {
                            var note = channel.GetBodyAs<Note>();
                            PrintNote(note);

                            if (note.Renote is Note renote)
                            {
                                Console.Write(" RN:");　
                                PrintNote(renote);
                            }

                            if (note.Reply is Note reply)
                            {
                                Console.Write(" RE:");　
                                PrintNote(reply);
                            }

                            Console.WriteLine();
                        } break;
                    }
                }
                else
                {
                    Helper.Debug($"Received Message Type=\"{res.Type}\"");
                }
            };

            st.Send("connect", new
            {
                channel = "globalTimeline",
                id = "1",
            });
            
            await Task.Delay(60000);
        }

        [Demo("Create Note")]
        private static async ValueTask TestNotesCreate()
        {
            var mi = await ConnectAsync();

            await mi.ApiAsync("notes/create", new
            {
                text = "Test from Misskey.NET",
                visibility = "home",
                localOnly = true,
            });
        }
    }
}
