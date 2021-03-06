using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Extreme.Starry.Updater.Common.Models;
using Extreme.Starry.Updater.Common.Models.Primitives;

namespace Extreme.Starry.Updater.Client
{
    public sealed class Updater : IDisposable
    {
        private readonly Client _client = new Client();
        private bool _disposedValue;

        public async Task<ServerInfo> GetServerInfo(Uri url) => await LoadBaseOn(await _client.GetObjectFromJson<ServerInfo>(url));

        public List<VersionInfo> AnalyzeVersionInfos(ServerInfo server, string currentVersion)
        {
            ThrowExceptWhenIfNull(server, nameof(server));

            List<VersionInfo> result = new List<VersionInfo>();
            bool isNeedUpdate = string.IsNullOrWhiteSpace(currentVersion);
            foreach (var version in server.Versions)
            {
                if (isNeedUpdate)
                    result.Add(version);
                else if (currentVersion == version.Version)
                    isNeedUpdate = true;
            }

            return result;
        }

        public async Task<IEnumerable<DownloadFileInfo>> AnalyzeDownloadFiles(List<VersionInfo> versions)
        {
            ThrowExceptWhenIfNull(versions, nameof(versions));

            Dictionary<string, DownloadFileInfo> finalList = new Dictionary<string, DownloadFileInfo>();
            foreach (var version in await LoadBaseOn(versions))
            {
                version.FileList = await LoadBaseOn(version.FileList);

                foreach (var file in version.FileList)
                {
                    // What the FUCK?
                    // How do we delete this file when removed from the list?
                    //if (finalList.ContainsKey(file.Path) && file.WillBeRemoved)
                    //{
                    //    finalList.Remove(file.Path);
                    //}
                    if (!string.IsNullOrWhiteSpace(file.WillBeMovedFrom) && finalList.TryGetValue(file.WillBeMovedFrom, out _))
                    {
                        finalList.Remove(file.WillBeMovedFrom);
                        finalList.Add(file.Path, file);
                    }
                    else
                    {
                        finalList[file.Path] = file;
                    }
                }
            }

            return finalList.Values;
        }

        
        public async Task DownloadFiles(IEnumerable<DownloadFileInfo> files)
        {
            // UNDONE: DownloadFiles
        }


        private void ThrowExceptWhenIfNull(object o, string name)
        {
            if (o is null)
                throw new ArgumentNullException(name);
        }

        private async Task<List<T>> LoadBaseOn<T>(List<T> list) where T : BaseOnConfig
        {
            for (int i = 0; i < list.Count; i++)
                list[i] = await LoadBaseOn(list[i]);

            return list;
        }

        private async Task<T> LoadBaseOn<T>(T baseOn) where T : BaseOnConfig
        {
            return baseOn is BaseOnConfig @base && !string.IsNullOrEmpty(@base.BaseOnUrl?.ToString())
                ? ApplyPropertiesFromBase(await _client.GetObjectFromJson<T>(@base.BaseOnUrl), baseOn)
                : baseOn;
        }

        private static T ApplyPropertiesFromBase<T>(T @base, T target)
        {
            foreach (var property in typeof(T).GetProperties())
            {
                var v = property.GetValue(target);
                if (!(v is null))
                    property.SetValue(@base, v);
            }

            return @base;
        }

        private void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _client.Dispose();
                }

                _disposedValue = true;
            }
        }

        // ~Updater()
        // {
        //     // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
