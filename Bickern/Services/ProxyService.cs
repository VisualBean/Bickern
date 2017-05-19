using System.Diagnostics;

namespace Bickern
{
    public class ProxyService : IProxyService
    {
        public void AddV4Proxy(string ip, int port)
        {
            RunCommand($"netsh interface portproxy add v4tov4 listenport=80 listenaddress={ip} connectport={port} connectaddress=127.0.0.1");
        }

        public void RemoveV4Proxy(string ip)
        {
            RunCommand($"netsh interface portproxy delete v4tov4 listenport=80 listenaddress={ip}");
        }

        private static void RunCommand(string command)
        {
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/C {command}",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };
            proc.Start();
        }
    }
}