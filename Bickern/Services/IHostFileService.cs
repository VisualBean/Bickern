namespace Bickern
{
    public interface IHostFileService
    {
        void AddHostFileEntry(string ip, string url);
        void RemoveHostFileEntry(string entry);
    }
}