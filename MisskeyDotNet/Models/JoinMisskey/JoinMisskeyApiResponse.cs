using System;

namespace MisskeyDotNet
{
    public class JoinMisskeyApiResponse
    {
        public DateTime Date { get; set; }
        public JoinMisskeyStatus Stats { get; set; } = new JoinMisskeyStatus();
        public JoinMisskeyInstance[] InstancesInfos { get; set; } = Array.Empty<JoinMisskeyInstance>();
    }
}