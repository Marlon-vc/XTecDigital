using System;
using System.IO;

namespace XTecDigital.Helpers
{
    public static class FileHandler
    {
        public static string StoragePath { get; set; }

        public static string GetGroupFolder(int groupId)
        {
            return Path.Combine(StoragePath, $"Grupo-{groupId}");
        }
    }
}