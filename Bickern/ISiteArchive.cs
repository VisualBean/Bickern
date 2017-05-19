using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bickern
{
    public interface ISiteArchive
    {
        IEnumerable<(string path, string url, string ip)> GetArchivedSites();
        void ArchiveSites(IEnumerable<LocalSite> sites);

    }
}
