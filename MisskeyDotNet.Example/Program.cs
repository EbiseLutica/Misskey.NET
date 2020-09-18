using System;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MisskeyDotNet.Example
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var io = new Misskey("misskey.io");
            var note = await io.ApiAsync<Note>("notes/show", new
            {
                noteId = "7zzafqsm9a",
            });
            Console.WriteLine(note.Cw);
            Console.WriteLine(note.Text);
        }
    }

    class Note
    {
        public string Id { get; set; } = "";
        public string? Text { get; set; }
        public string? Cw { get; set; }
    }
}
