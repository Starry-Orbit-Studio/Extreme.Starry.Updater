using System.Diagnostics;
using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Extreme.Starry.Updater.Client.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            var updater = new Updater();
            Common.Models.ServerInfo serverInfo = await updater.GetServerInfo(new Uri("http://localhost:3000/"));

            var versions = updater.AnalyzeVersionInfos(serverInfo, string.Empty);
            
            var files = await updater.AnalyzeDownloadFiles(versions);

            foreach (var file in files)
            {
                Console.WriteLine(file.Path);
                Console.WriteLine(file.WillBeMovedFrom);
                Console.WriteLine(file.WillBeRemoved);
            }
            //Debugger.Break();
        }
    }
}