using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bickern
{
    public class HostFileService : IHostFileService
    {
        private static string hostFileLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "drivers/etc/hosts");

        private static List<string> GetHostFile()
        {
            return File.ReadAllLines(hostFileLocation).ToList();
        }
        /// <summary>
        /// Add hostfile entry for ip and url
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="url"></param>
        public void AddHostFileEntry(string ip, string url)
        {
            var hostFile = GetHostFile();
            hostFile.RemoveAll(l => l.Contains(ip));
            hostFile.Add($"{ip} {url}");

            WriteHostFile(hostFile);
        }
        /// <summary>
        /// Remove hostfile entry
        /// </summary>
        /// <param name="entry"></param>
        public void RemoveHostFileEntry(string entry)
        {
            var hostFile = GetHostFile();
            hostFile.RemoveAll(l => l.Contains(entry));

            WriteHostFile(hostFile);
        }

        private static void WriteHostFile(IEnumerable<string> lines)
        {
            File.WriteAllLines(hostFileLocation, lines);
        }
    }
}