#pragma warning disable IDE0130


using System;

using Extreme.Starry.Updater.Common.Models;

namespace Updater
{
    public class UpdateMirror
    {
        public string Url { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public static implicit operator UpdateMirror(ServerInfo offline)
        {
            return new UpdateMirror()
            {
                Url = offline.ServerUrlString,
                Name = offline.ServerName,
                Location = offline.ServerUrl.Host
            };
        }

        public static implicit operator ServerInfo(UpdateMirror mirror)
        {
            return new ServerInfo()
            {
                ServerUrl = new Uri(mirror.Url),
                ServerName = mirror.Name
            };
        }
    }
}
