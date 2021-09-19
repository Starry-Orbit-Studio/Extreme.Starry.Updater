using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable IDE0130
#pragma warning disable IDE1006
#pragma warning disable IDE0079
#pragma warning disable IDE0044
#pragma warning disable IDE0051

namespace Updater
{
    public class CustomComponent
    {
        private static List<CustomComponent> _instances = new List<CustomComponent>();
        private readonly CancellationTokenSource _cancellation = new CancellationTokenSource();
        private Task _downloadTask;

        /// <summary>
        /// It's Friendly Name about this Module.
        /// </summary>
        public string GUIName { get; }

        /// <summary>
        /// Configure File ID
        /// </summary>
        public string ININame { get; }

        /// <summary>
        /// Is being downloaded.
        /// </summary>
        public bool IsBeingDownloaded => _downloadTask?.Status is TaskStatus.Running;

        /// <summary>
        /// Local Configure File ID
        /// </summary>
        public string LocalIdentifier { get; }

        public string LocalPath { get; }

        /// <summary>
        /// Remote Configure File ID
        /// </summary>
        public string RemoteIdentifier { get; }

        /// <summary>
        /// File Size about this Module.
        /// </summary>
        /// <remarks>
        /// return <seealso cref="0"/> when get faild.
        /// </remarks>
        public long RemoteSize
        {
            get;internal set;
        }

        public event DownloadFinishedEventHandler DownloadFinished;

        public event DownloadProgressChangedEventHandler DownloadProgressChanged;

        internal CustomComponent()
        {
            _instances.Add(this);
        }

        public bool DoNotUpdateToSmaller { get; }
        public string DownloadPath { get; }

        private void DownloadProgressCallback(long progress, long? length)
        {
            if (length.HasValue)
                DownloadProgressChanged?.Invoke(this, (int)Math.Ceiling(((decimal)progress / length.Value)));
            else
                DownloadProgressChanged?.Invoke(this, 50);
        }

        public static bool IsDownloadInProgress() => _instances.Any(x => x.IsBeingDownloaded);

        /// <summary>
        /// Start Download Module
        /// </summary>
        public void DownloadComponent()
        {
        }

        public static int getComponentId(string componentName)
        {
            throw new NotImplementedException();

            //for (int i = 0; i < CUpdater.CustomComponents.Length; i++)
            //{
            //    if (componentName == CUpdater.CustomComponents[i].ININame)
            //    {
            //        return i;
            //    }
            //}
            //return -1;
        }

        public void StopDownload()
        {
            if (_cancellation.Token.CanBeCanceled && !_cancellation.IsCancellationRequested)
                _cancellation.Cancel();
        }
    }
}
