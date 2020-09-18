namespace MisskeyDotNet
{
    public class Error
    {
        public string Message { get; set; } = "";
        public string Code { get; set; } = "";
        public string Id { get; set; } = "";
        public string Kind { get; set; } = "";
        public ErrorInfo? Info { get; set; }
    }
}