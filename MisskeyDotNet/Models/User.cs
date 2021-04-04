namespace MisskeyDotNet
{
    public class User
    {
        public string Id { get; set; } = "";
        public string? Name { get; set; }
        public string Username { get; set; } = "";
        public string? Host { get; set; }
        public string? AvatarUrl { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsModerator { get; set; }
        public bool IsBot { get; set; }
        public bool IsCat { get; set; }
        public bool IsLocked { get; set; }
        public bool? IsSilenced { get; set; }
        public bool? IsSuspended { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public string? Birthday { get; set; }
        // public Field[]? Fields { get; set; }
        public int? FollowersCount { get; set; }
        public int? FollowingCount { get; set; }
        public int? NotesCount { get; set; }
        public bool? InjectFeaturedNote { get; set; }
        public bool? ReceiveAnnouncementEmail { get; set; }
        public bool? AlwaysMarkNsfw { get; set; }
        public bool? CarefulBot { get; set; }
        public bool? AutoAcceptFollowed { get; set; }
        public bool? NoCrawle { get; set; }
        public bool? IsExplorable { get; set; }
        public bool? IsFollowing { get; set; }
        public bool? IsFollowed { get; set; }
        public bool? HasPendingFollowRequestFromYou { get; set; }
        public bool? HasPendingFollowRequestToYou { get; set; }
        public bool? IsBlocking { get; set; }
        public bool? IsBlocked { get; set; }
        public bool? IsMuted { get; set; }
        public bool? IsRenoteMuted { get; set; }

    }
}


