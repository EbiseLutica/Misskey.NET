using System;

namespace MisskeyDotNet
{
    public class Emoji
    {
        public string Id { get; set; } = "";

        public string[] Aliases { get; set; } = Array.Empty<string>();

        public string Name { get; set; } = "";

        public string? Category { get; set; }

        public string? Host { get; set; }

        public string Url { get; set; } = "";
    }
}