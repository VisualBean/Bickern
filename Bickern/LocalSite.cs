using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace Bickern
{
    public class LocalSite : IDisposable
    {
        private IHostFileService hostFileService;
        private IIPService ipService;
        private IProxyService proxyService;
        private SimpleHTTPServer server;

        public LocalSite(IHostFileService hostFileService, IProxyService proxyService, IIPService ipService, DirectoryInfo path, string siteUrl)
        {

            this.hostFileService = hostFileService;
            this.proxyService = proxyService;
            this.ipService = ipService;

            this.Path = path.FullName;
            this.Url = siteUrl;

            initialize();
        }

        public LocalSite(IHostFileService hostFileService, IProxyService proxyService, IIPService ipService, DirectoryInfo path, string siteUrl, IPAddress ip)
        {

            this.hostFileService = hostFileService;
            this.proxyService = proxyService;
            this.ipService = ipService;

            this.Path = path.FullName;
            this.Url = siteUrl;
            this.IP = ip;

            initialize(ip.ToString());
        }

        public IPAddress IP { get; set; }
        public string Path { get; private set; }
        public string Url { get; private set; }
        public void Dispose()
        {
            server.Stop();
            proxyService.RemoveV4Proxy(IP.ToString());
            hostFileService.RemoveHostFileEntry(this.Url);
        }

        public void Stop()
        {
            Dispose();
        }

        private void CreateProxyAndAddHostFileEntry(string ip, int port, string url)
        {
            proxyService.RemoveV4Proxy(ip);
            proxyService.AddV4Proxy(ip, port);
            hostFileService.AddHostFileEntry(ip, Url);
        }

        private void initialize()
        {
            if (Path == null)
                throw new ArgumentNullException();
            if (Url == null)
                throw new ArgumentNullException();

            server = new SimpleHTTPServer(Path);
            this.IP = IPAddress.Parse(ipService.GetNextAvailableIPAddress());

            CreateProxyAndAddHostFileEntry(IP.ToString(), server.Port, Url);
        }

        private void initialize(string ip)
        {
            if (Path == null)
                throw new ArgumentNullException();
            if (Url == null)
                throw new ArgumentNullException();

            server = new SimpleHTTPServer(Path);
            this.IP = IPAddress.Parse(ipService.GetIPAddress(ip).ipAddress);

            CreateProxyAndAddHostFileEntry(IP.ToString(), server.Port, Url);
        }
    }
}