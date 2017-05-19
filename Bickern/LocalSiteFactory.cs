using System.IO;
using System.Linq;
using System.Net;

namespace Bickern
{
    //TODO: Refactor to ServiceActionResult.
    public class LocalSiteFactory : ILocalSiteFactory
    {
        private IHostFileService hostFileService;
        private IIPService ipService;
        private IProxyService proxyService;

        public LocalSiteFactory(IHostFileService hostFileService, IProxyService proxyService, IIPService ipService)
        {
            this.hostFileService = hostFileService;
            this.proxyService = proxyService;
            this.ipService = ipService;
        }
        /// <summary>
        /// Create a new localsite from path and url. Returns null if it can't
        /// </summary>
        /// <param name="path"></param>
        /// <param name="siteUrl"></param>
        /// <returns></returns>
        public LocalSite CreateLocalSite(DirectoryInfo path, string siteUrl)
        {

            if (path.GetFiles().Select(f =>f.Name).Intersect(Resources.IndexFiles).Any())
                    return new LocalSite(hostFileService, proxyService, ipService, path, $"{siteUrl}.dev");          

            return null;
        }
        /// <summary>
        /// Create site from ip, path and url. Returns null if it fails.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="siteUrl"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
 
        public LocalSite CreateLocalSiteFromIP(DirectoryInfo path, string siteUrl, string ip)
        {
            IPAddress ipAddress;
            if (!IPAddress.TryParse(ip, out ipAddress))
                return null;

            if (path.GetFiles().Select(f => Resources.IndexFiles.Contains(f.Name)).Any())
                return new LocalSite(hostFileService, proxyService, ipService, path, $"{siteUrl}.dev", ipAddress);

            return null;
        }
    }
}