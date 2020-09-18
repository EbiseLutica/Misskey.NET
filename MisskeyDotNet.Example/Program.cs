using System;
using System.Threading.Tasks;

namespace MisskeyDotNet.Example
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var io = new Misskey("social.xeltica.work");
            var note = await io.ApiAsync<Note>("notes/show", new
            {
                noteId = "44444444",
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
