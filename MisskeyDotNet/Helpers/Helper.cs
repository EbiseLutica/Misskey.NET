using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MisskeyDotNet
{
    public static class Helper
    {
        public static void OpenUrl(string url)
        {
            // from https://brockallen.com/2016/09/24/process-start-for-urls-on-net-core/
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                url = url.Replace("&", "^&");
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("xdg-open", url);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", url);
            }
            else
            {
                throw new NotSupportedException("このプラットフォームはサポートされていません。");
            }
        }

        public static void Debug(string str)
        {
            #if DEBUG
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {str}");
            #endif
        }
    }
}