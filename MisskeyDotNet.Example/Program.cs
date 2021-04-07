using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MisskeyDotNet.Example
{
    partial class Program
    {
        static async Task Main()
        {
            var commands = typeof(Program).GetMethods(BindingFlags.Static | BindingFlags.NonPublic)
                .Select(m => (method: m, attr: m.GetCustomAttributes().OfType<DemoAttribute>().FirstOrDefault()))
                .Where(ma => ma.attr != null)
                .Select(ma => (ma.method, name: ma.attr.Name))
                .ToList();
            
            Console.WriteLine("Misskey.NET Demo Program");
            Console.WriteLine("(C)2021 Xeltica");
            Console.WriteLine();

            while (true)
            {
                var i = 0;
                foreach (var (_, name) in commands)
                {
                    Console.WriteLine($"{i}: {name}");
                    i++;
                }

                Console.Write("Choose number to run > ");

                if (!int.TryParse(Console.ReadLine(), out var num) || num < 0 || num >= commands.Count)
                {
                    Console.Error.WriteLine("Invalid");
                }
                else
                {
                    await (ValueTask)(commands[num].method.Invoke(null, null))!;
                    await Task.Delay(1000);
                }
            }
        }

        private static async ValueTask<Misskey> ConnectAsync()
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
            return io;
        }
    }
}
