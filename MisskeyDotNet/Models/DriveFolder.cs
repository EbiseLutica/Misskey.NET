namespace MisskeyDotNet
{
    public class DriveFolder
    {
        public string Id { get; set; } = "";
        public string CreatedAt { get; set; } = "";
        public string Name { get; set; } = "";
        public string? ParentId { get; set; }
        public DriveFolder? Parent { get; set; }
        public int? FoldersCount { get; set; }
        public int? FilesCount { get; set; }
    }
}