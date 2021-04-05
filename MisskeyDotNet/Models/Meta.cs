using System;
using System.Collections.Generic;

namespace MisskeyDotNet
{
    public class Meta
    {
		public string? MaintainerName { get; set; }
		public string? MaintainerEmail { get; set; }
		public string Version { get; set; } = "";
		public string? Name { get; set; }
		public string Uri { get; set; } = "";
		public string? Description { get; set; }
		public string[] Langs { get; set; } = Array.Empty<string>();
		public string? TosUrl { get; set; }
		public string? RepositoryUrl { get; set; }
		public string? FeedbackUrl { get; set; }
		public bool Secure { get; set; }
		public bool DisableRegistration { get; set; }
		public bool DisableLocalTimeline { get; set; }
		public bool DisableGlobalTimeline { get; set; }
		public int DriveCapacityPerLocalUserMb { get; set; }
		public int DriveCapacityPerRemoteUserMb { get; set; }
		public bool EnableHcaptcha { get; set; }
		public string? HcaptchaSiteKey { get; set; }
		public bool EnableRecaptcha { get; set; }
		public string? RecaptchaSiteKey { get; set; }
		public string? SwPublickey { get; set; }
		public string? MascotImageUrl { get; set; }
		public string? BannerUrl { get; set; }
		public string? ErrorImageUrl { get; set; }
		public string? IconUrl { get; set; }
		public string? BackgroundImageUrl { get; set; }
		public string? LogoImageUrl { get; set; }
		public int MaxNoteTextLength { get; set; }
		// public Emoji[] Emojis { get; set; }
		public bool EnableEmail { get; set; }
		public bool EnableTwitterIntegration { get; set; }
		public bool EnableGithubIntegration { get; set; }
		public bool EnableDiscordIntegration { get; set; }
		public bool EnableServiceWorker { get; set; }
        public string[]? PinnedPages { get; set; }
        public string? PinnedClipId { get; set; }
        public bool? CacheRemoteFiles { get; set; }
        public bool? ProxyRemoteFiles { get; set; }
        public bool? RequireSetup { get; set; }
        public string? ProxyAccountName { get; set; }
        public Dictionary<string, bool>? Features { get; set; }
        public bool? UseStarForReactionFallback { get; set; }
        public string[]? PinnedUsers { get; set; }
        public string[]? HiddenTags { get; set; }
        public string[]? BlockedHosts { get; set; }
        public string? HcaptchaSecretKey { get; set; }
        public string? RecaptchaSecretKey { get; set; }
        public string? ProxyAccountId { get; set; }
        public string? TwitterConsumerKey { get; set; }
        public string? TwitterConsumerSecret { get; set; }
        public string? GithubClientId { get; set; }
        public string? GithubClientSecret { get; set; }
        public string? DiscordClientId { get; set; }
        public string? DiscordClientSecret { get; set; }
        public string? SummalyProxy { get; set; }
        public string? Email { get; set; }
        public string? SmtpSecure { get; set; }
        public string? SmtpHost { get; set; }
        public int? SmtpPort { get; set; }
        public string? SmtpUser { get; set; }
        public string? SmtpPass { get; set; }
        public string? SwPrivateKey { get; set; }
        public bool? UseObjectStorage { get; set; }
        public string? ObjectStorageBaseUrl { get; set; }
        public string? ObjectStorageBucket { get; set; }
        public string? ObjectStoragePrefix { get; set; }
        public string? ObjectStorageEndpoint { get; set; }
        public string? ObjectStorageRegion { get; set; }
        public int? ObjectStoragePort { get; set; }
        public string? ObjectStorageAccessKey { get; set; }
        public string? ObjectStorageSecretKey { get; set; }
        public bool? ObjectStorageUseSSL { get; set; }
        public bool? ObjectStorageUseProxy { get; set; }
        public bool? ObjectStorageSetPublicRead { get; set; }
        public bool? ObjectStorageS3ForcePathStyle { get; set; }
    }
}