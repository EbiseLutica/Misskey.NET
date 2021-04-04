using System;
using System.Collections.Generic;
using System.IO;

namespace MisskeyDotNet
{
    public class Note
    {
        public string Id { get; set; } = "";
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; } = "";
        public User User { get; set; } = null!;
        public string? Text { get; set; }
        public string? Cw { get; set; }
        public string Visibility { get; set; } = "public";
        public bool LocalOnly { get; set; }
        public string[]? VisibleUserIds { get; set; }
        public bool ViaMobile { get; set; }
        public int RenoteCount { get; set; }
        public int RepliesCount { get; set; }
        public Dictionary<string, int> Reactions { get; set; } = new Dictionary<string, int>();
        // public string Tags { get; set; }
        // public string Emojis { get; set; }
        public string[]? FileIds { get; set; }
        public DriveFIle[]? Files { get; set; }
        public string? ReplyId { get; set; }
        public string? RenoteId { get; set; }
        // channelId { get; set; }
        // channel { get; set; }
        //     id { get; set; }
        //     name { get; set; }
        // }  { get; set; }
        // mentions { get; set; }
        public string? Uri { get; set; }
        public string? Url { get; set; }
        public string? _featuredId_ { get; set; }
        public string? _prId_ { get; set; }
    }
}