using System.IO;

namespace Bickern
{
    public interface ILocalSiteFactory
    {
        LocalSite CreateLocalSite(DirectoryInfo path, string siteUrl);
        LocalSite CreateLocalSiteFromIP(DirectoryInfo path, string siteUrl, string ip);
    }
}