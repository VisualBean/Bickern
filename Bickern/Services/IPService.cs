using System;
using System.Collections.Generic;
using System.Linq;

namespace Bickern
{
    public class IPService : IIPService
    {
        private static object _lock = new object();

        private static IDictionary<string, bool> _ipAddresses = new Dictionary<string, bool>()
        {
            {"127.32.4.1", false },
            {"127.32.4.2", false },
            {"127.32.4.3", false },
            {"127.32.4.4", false },
            {"127.32.4.5", false },
            {"127.32.4.6", false },
            {"127.32.4.7", false },
            {"127.32.4.8", false },
            {"127.32.4.9", false },
            {"127.32.4.10", false },
            {"127.32.4.11", false },
            {"127.32.4.12", false },
            {"127.32.4.13", false },
            {"127.32.4.14", false },
            {"127.32.4.15", false },
            {"127.32.4.16", false },
            {"127.32.4.17", false },
            {"127.32.4.18", false },
            {"127.32.4.19", false },
            {"127.32.4.20", false },
            {"127.32.4.21", false },
            {"127.32.4.22", false },
            {"127.32.4.23", false },
            {"127.32.4.24", false },
            {"127.32.4.25", false },
            {"127.32.4.26", false },
            {"127.32.4.27", false },
            {"127.32.4.28", false },
            {"127.32.4.29", false },
            {"127.32.4.30", false },
            {"127.32.4.31", false },
            {"127.32.4.32", false },
            {"127.32.4.33", false },
            {"127.32.4.34", false },
            {"127.32.4.35", false },
            {"127.32.4.36", false },
            {"127.32.4.37", false },
            {"127.32.4.38", false },
            {"127.32.4.39", false },
            {"127.32.4.40", false },
            {"127.32.4.41", false },
            {"127.32.4.42", false },
            {"127.32.4.43", false },
            {"127.32.4.44", false },
            {"127.32.4.45", false },
            {"127.32.4.46", false },
            {"127.32.4.47", false },
            {"127.32.4.48", false },
            {"127.32.4.49", false },
            {"127.32.4.50", false }
        };

        /// <summary>
        /// Gets and blocks the first free IP in the list.
        /// </summary>
        /// <returns></returns>
        public string GetNextAvailableIPAddress()
        {
            var address = _ipAddresses.FirstOrDefault(c => c.Value == false).Key;
            if (address == null)
                throw new NullReferenceException("No more addresses available");

            lock (_lock)
            {
                _ipAddresses[address] = true;
            }
            return address;
        }
        /// <summary>
        /// Get specific ip.
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public (string ipAddress, bool isInUse) GetIPAddress(string ip)
        {
            var address = _ipAddresses.FirstOrDefault(i => i.Key == ip);

            bool isInUse = address.Value;
            string ipAddress = address.Key;

            lock (_lock)
            {
                _ipAddresses[ipAddress] = true;
            }

            return (ipAddress, isInUse);
        }
    }
}