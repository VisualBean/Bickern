using System.Collections.Generic;

namespace Bickern
{
    public interface IIPService
    {
        string GetNextAvailableIPAddress();
        (string ipAddress, bool isInUse) GetIPAddress(string ip);
    }
}