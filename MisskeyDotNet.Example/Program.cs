using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MisskeyDotNet.Example
{
    class Program
    {
        static async Task Main(string[] args)
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

            var note = await io.ApiAsync<Note>("notes/show", new 
            {
                noteId = "7zzafqsm9a",
            });

            Console.WriteLine("Note ID: " + note.Id);
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
    }

    class Note
    {
        public string Id { get; set; } = "";
        public string? Text { get; set; }
        public string? Cw { get; set; }

        public Dictionary<string, int> Reactions { get; set; } = new Dictionary<string, int>();
    }
}
