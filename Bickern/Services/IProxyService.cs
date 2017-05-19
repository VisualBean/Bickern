namespace Bickern
{
    public interface IProxyService
    {
        void AddV4Proxy(string ip, int port);
        void RemoveV4Proxy(string ip);
    }
}