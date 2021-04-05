using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MisskeyDotNet
{
    public static class PermissionExtension
    {
        public static string[] ToStringArray(this Permission[] arr)
        {
            if (arr.Contains(Permission.All))
                return permissionMap.Values.ToArray();
            return arr.Select(p => permissionMap[p]).ToArray();
        }

        private static readonly ReadOnlyDictionary<Permission, string> permissionMap = new ReadOnlyDictionary<Permission, string>(
            new[] {
                (Permission.ReadAccount, "read:account"),
                (Permission.WriteAccount, "write:account"),
                (Permission.ReadBlocks, "read:blocks"),
                (Permission.WriteBlocks, "write:blocks"),
                (Permission.ReadDrive, "read:drive"),
                (Permission.WriteDrive, "write:drive"),
                (Permission.ReadFavorites, "read:favorites"),
                (Permission.WriteFavorites, "write:favorites"),
                (Permission.ReadFollowing, "read:following"),
                (Permission.WriteFollowing, "write:following"),
                (Permission.ReadMessaging, "read:messaging"),
                (Permission.WriteMessaging, "write:messaging"),
                (Permission.ReadMutes, "read:mutes"),
                (Permission.WriteMutes, "write:mutes"),
                (Permission.WriteNotes, "write:notes"),
                (Permission.ReadNotifications, "read:notifications"),
                (Permission.WriteNotifications, "write:notifications"),
                (Permission.ReadReactions, "read:reactions"),
                (Permission.WriteReactions, "write:reactions"),
                (Permission.WriteVotes, "write:votes"),
                (Permission.ReadPages, "read:pages"),
                (Permission.WritePages, "write:pages"),
                (Permission.WritePageLikes, "write:page-likes"),
                (Permission.ReadPageLikes, "read:page-likes"),
                (Permission.ReadUserGroups, "read:user-groups"),
                (Permission.WriteUserGroups, "write:user-groups"),
                (Permission.ReadChannels, "read:channels"),
                (Permission.WriteChannels, "write:channels"),
            }.ToDictionary(k => k.Item1, v => v.Item2)
        );
    }
}