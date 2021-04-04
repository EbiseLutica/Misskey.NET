using System;

namespace MisskeyDotNet
{
    [Serializable]
    public class MisskeyApiException : Exception
    {
        public Error Error { get; }
        public MisskeyApiException(Error error)
            : base($"Misskey API Error: {error.Message}{(error.Info == null ? "" : $"{error.Info.Param}: {error.Info.Reason}")}")
        {
            Error = error;
        }
    }
}