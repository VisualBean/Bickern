using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bickern.Services
{
    public class SiteArchive : ISiteArchive
    {
        private static readonly string sitesConfigPath = Path.Combine(Directory.GetCurrentDirectory(), "sites.json");

        public void ArchiveSites(IEnumerable<LocalSite> sites)
        {
            var text = JsonConvert.SerializeObject(sites);
            File.WriteAllText(sitesConfigPath, text);
        }

        public IEnumerable<(string path, string url, string ip)> GetArchivedSites()
        {
            var text = File.ReadAllText(sitesConfigPath);
            var sites = JsonConvert.DeserializeObject<IEnumerable<(string path, string url, string ip)>>(text);
            return sites;
         
        }
    }
}
