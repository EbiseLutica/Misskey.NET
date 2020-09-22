using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MisskeyDotNet
{
    public static class ObjectExtension
    {
        public static Dictionary<string, object?> ConvertToDictionary(this object? o)
        {
            var dict = new Dictionary<string, object?>();
            if (o == null) return dict;
            foreach (PropertyDescriptor d in TypeDescriptor.GetProperties(o))
                dict[d.Name] = d.GetValue(o);
            return dict;
        }

        public static string? ToQueryString(this object? o)
        { 
            if (o == null) return null;
            return string.Join(
                '&',
                o.ConvertToDictionary()
                    .Select(kv => kv.Value == null ? null : $"{HttpUtility.UrlEncode(kv.Key)}={HttpUtility.UrlEncode(kv.Value.ToString())}")
                    .OfType<string>()
            );
        }
    }
}