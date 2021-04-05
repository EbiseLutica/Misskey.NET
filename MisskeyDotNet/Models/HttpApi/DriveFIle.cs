using System;
using System.Collections.Generic;

namespace MisskeyDotNet
{
    public class DriveFIle
    {
        public string Id { get; set; } = "";

        public DateTime CreatedAt { get; set; }

        public string Name { get; set; } = "";

        public string Type { get; set; } = "";

        public string Md5 { get; set; } = "";

        public int Size { get; set; }


        public bool IsSensitive { get; set; }

        public string? Blurhash { get; set; }

        public Dictionary<string, object>? Properties { get; set; }

        public string? Url { get; set; }

        public string? ThumbnailUrl { get; set; }

        public string? Comment { get; set; }

        public string? FolderId { get; set; }

        public DriveFolder? Folder { get; set; }

        public string? UserId { get; set; }

        public User? User { get; set; }
    }
}