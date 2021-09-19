#pragma warning disable IDE0130

using Extreme.Starry.Updater.Common.Models;

namespace Updater
{
    public class DTAFileInfo
    {
        public string Name { get; set; }

        public string Identifier { get; set; }

        public int Version { get; set; }

        public int Size { get; set; }

        public static implicit operator DownloadFileInfo(DTAFileInfo fileInfo) => new DownloadFileInfo
        {
            Path = fileInfo.Name,
            Hash = fileInfo.Identifier
        };
        public static implicit operator DTAFileInfo(DownloadFileInfo offline) => new DTAFileInfo
        {
            Name = offline.Path,
            Identifier = offline.Hash
        };
    }
}
