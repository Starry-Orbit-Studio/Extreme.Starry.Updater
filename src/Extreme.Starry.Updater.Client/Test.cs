using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extreme.Starry.Updater.Client
{
    internal class Test
    {
        public static async Task Main()
        {
            var updater = new Updater();
            Common.Models.ServerInfo serverInfo = await updater.GetServerInfo(new Uri("http://localhost:3000/"));
            var versions = updater.AnalyzeVersionInfos(serverInfo, string.Empty);
            var files = await updater.AnalyzeDownloadFiles(versions);
            Debugger.Break();
        }
    }
}
