using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable IDE0130
#pragma warning disable IDE1006
#pragma warning disable IDE2002

namespace Updater
{
    /// <summary>
    /// Easy porting To ESUpdater
    /// </summary>
    public static class CUpdater
    {
        //        private static string _offlineConfigPath;
        //        private static VersionState _dTAVersionState = VersionState.UNKNOWN;
        //        private static UpdateFileInfo[] _updateInfoList;
        //        private static CancellationTokenSource _cancellation = new CancellationTokenSource();
        //        private static bool _terminateUpdate;
        //        private static int _currentSourceId;
        //        private static bool _init = false;
        //        public static string VERSION_FILE { get => _offlineConfigPath; set => _offlineConfigPath = value; }

        //        private static void SaveConfig()
        //        {
        //            OfflineConfig.Sources.Source = UPDATEMIRRORS.Select(x => (OfflineSource)x).ToList();
        //            OfflineConfig.SaveOfflineConfig(_offlineConfigPath);
        //        }

        //        public static VersionState DTAVersionState
        //        {
        //            get => _dTAVersionState; private set
        //            {
        //                _dTAVersionState = value;
        //                OnVersionStateChanged?.Invoke();
        //            }
        //        }

        public static int UpdateSizeInKb { get; private set; }
        public static string CURRENT_LAUNCHER_NAME;

        [Obsolete("Equal CURRENT_LAUNCHER_NAME")]
        public static string NEW_LAUNCHER_NAME => CURRENT_LAUNCHER_NAME;
        //public static string GameVersion => OfflineConfig?.Version ?? "N/A";
        //public static string ServerGameVersion => ServerDetailsInfo?.Current ?? "N/A";

        public static List<UpdateMirror> UPDATEMIRRORS { get; } = new List<UpdateMirror>();

        //        public static void Initialize(string game)
        //        {
        //            if (string.IsNullOrWhiteSpace(_offlineConfigPath))
        //            {
        //                ESUpdater.Log($"{nameof(VERSION_FILE)} was not set.");
        //                return;
        //            }
        //            if (string.IsNullOrWhiteSpace(CURRENT_LAUNCHER_NAME))
        //            {
        //                ESUpdater.Log($"{nameof(CURRENT_LAUNCHER_NAME)} was not set.");
        //                return;
        //            }
        //            if (!File.Exists(_offlineConfigPath))
        //            {
        //                ESUpdater.Log($"File {Path.GetFileName(_offlineConfigPath)} was not found.");
        //                return;
        //            }
        //            OfflineConfig = ESUpdater.LoadOfflineConfig(_offlineConfigPath);
        //            UPDATEMIRRORS.AddRange(OfflineConfig.Sources.Source.Select(x => new UpdateMirror(x)));
        //            _init = true;
        //        }


        //        public static void ClearVersionInfo()
        //        {
        //            if (!_init)
        //                return;
        //            if (!(OfflineConfig is null))
        //                OfflineConfig.Version = string.Empty;
        //            ServerDetailsInfo = null;

        //            LocalFileInfos.Clear();
        //            DTAVersionState = VersionState.UNKNOWN;
        //        }


        //        public static async void CheckForUpdates()
        //        {
        //            if (!_init)
        //                return;
        //            try
        //            {
        //                ESUpdater.Log("Invoking");
        //                HasVersionBeenChecked = true;
        //                if (DTAVersionState != VersionState.UPDATECHECKINPROGRESS && DTAVersionState != VersionState.UPDATEINPROGRESS)
        //                {
        //                    if (UPDATEMIRRORS.Count < 1)
        //                    {
        //                        ESUpdater.Log("There are no update source can be used.");
        //                        return;
        //                    }

        //                    DTAVersionState = VersionState.UPDATECHECKINPROGRESS;

        //                    if (ServerDetailsInfo is null)
        //                    {
        //                        for (int i = 0; i < UPDATEMIRRORS.Count; i++)
        //                        {
        //                            try
        //                            {
        //                                ESUpdater.Log($"Trying to connect and download {UPDATEMIRRORS[0].Url}");
        //                                ServerDetailsInfo = await ((OfflineSource)UPDATEMIRRORS[0]).GetServerDetailsInfo();
        //                                ESUpdater.Log($"Connect Successful.");
        //                                break;
        //                            }
        //                            catch (Exception e)
        //                            {
        //                                ESUpdater.Log($"Error connecting to update source. {e}");
        //                            }
        //                        }

        //                        if (ServerDetailsInfo is null)
        //                            ESUpdater.Log($"Nothing update source to used.");
        //                    }

        //                    if (OfflineConfig.IsShouldUpdate(ServerDetailsInfo) || LocalFileInfos.Count > 0)
        //                    {
        //                        ESUpdater.Log($"Current Version is {OfflineConfig.Version}, Online Version is {ServerDetailsInfo.Current}");
        //                        var versions = OfflineConfig.GetUpdateInfoList(ServerDetailsInfo);
        //                        _updateInfoList = await versions.GetUpdateFileList().ConfigureAwait(false);
        //                        ESUpdater.Log($"Update File Count is {_updateInfoList.Length}");
        //                        var size = _updateInfoList.Select(x => x.Size).Sum();
        //                        ESUpdater.Log($"Update File Size is {size}");
        //                        UpdateSizeInKb = (int)(size / 1024);
        //                        OfflineConfig.SyncOfflineConfigFile(_updateInfoList);
        //                        SaveConfig();
        //                        DTAVersionState = VersionState.OUTDATED;
        //                        OnFileIdentifiersUpdated();
        //                        OnLocalFileVersionsChecked?.Invoke();
        //                    }
        //                    else
        //                    {
        //                        DTAVersionState = VersionState.UPTODATE;
        //                        OnFileIdentifiersUpdated();
        //                        // TODO: Check Modules Update Info
        //                    }
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                DTAVersionState = VersionState.UNKNOWN;
        //                ESUpdater.Log($"An error occured while performing version check: {e}");
        //            }
        //        }

        //        public static bool IsFileNonexistantOrOriginal(string filepath)
        //        {
        //            if (!_init)
        //                return true;
        //            UpdateFileInfo localFile = OfflineConfig.Files.File.Find(x => x.Path.Equals(filepath, StringComparison.OrdinalIgnoreCase));
        //            switch (localFile)
        //            {
        //                case null:
        //                    return true;
        //                default:
        //                    return localFile.Hash.Equals(localFile.GetFileHash(), StringComparison.OrdinalIgnoreCase);
        //            }
        //        }


        //        private static void OnFileIdentifiersUpdated()
        //        {
        //            ESUpdater.Log("Invoked");
        //            FileIdentifiersUpdated?.Invoke();
        //        }

        //        public static string TryGetUniqueId(string filePath)
        //        {
        //            try
        //            {
        //                return GetUniqueIdForFile(filePath);
        //            }
        //            catch
        //            {
        //                return string.Empty;
        //            }
        //        }
        //        public static bool TerminateUpdate
        //        {
        //            get => _terminateUpdate; set
        //            {
        //                if (_terminateUpdate = value)

        //                    _cancellation.Cancel();
        //                else
        //                    _cancellation = new CancellationTokenSource();
        //            }
        //        }

        //        public static string GetUniqueIdForFile(string filePath) => ESUpdater.GetFileHash(filePath);

        //        public static string GetUpdateServerUrl() => UPDATEMIRRORS[_currentSourceId].Url;


        //        [Obsolete("It's Not Supported in ESUpdater", true)]
        //        public static DTAFileInfo parseFileInfo(string filePath, string versionId) => throw new NotSupportedException();


        //        public static async void StartAsyncUpdate()
        //        {
        //            if (!_init)
        //                return;
        //            ESUpdater.Log("Start Update");
        //            try
        //            {
        //                if (string.IsNullOrEmpty(ServerGameVersion) || ServerGameVersion == "N/A" || DTAVersionState != VersionState.OUTDATED)
        //                {
        //                    throw new Exception("Update server integrity error.");
        //                }

        //                DTAVersionState = VersionState.UPDATEINPROGRESS;
        //                if (_updateInfoList is null)
        //                    throw new Exception("");

        //                try
        //                {
        //                    await _updateInfoList.DownloadAll(
        //                        1024,
        //                        (i1, i2, i3, i4, s1, i5, i6) =>
        //                            UpdateProgressChanged?.Invoke(s1, (int)(((decimal)i5 / i6) * 100), (int)((i3 / i4) * 100)),
        //                        _cancellation.Token).ConfigureAwait(false);
        //                    ESUpdater.Log("Download Successful.");

        //                    if (File.Exists(Path.Combine(ESUpdater.UpdateFolder, CURRENT_LAUNCHER_NAME)))
        //                    {
        //                        ESUpdater.Log("Update New Launcher");
        //                        if (File.Exists(CURRENT_LAUNCHER_NAME))
        //                            File.Delete(CURRENT_LAUNCHER_NAME);
        //                        File.Move(Path.Combine(ESUpdater.UpdateFolder, CURRENT_LAUNCHER_NAME), CURRENT_LAUNCHER_NAME);
        //                    }

        //                    OfflineConfig.Version = ServerDetailsInfo.Current;
        //                    OfflineConfig.SyncOfflineConfigFile(_updateInfoList);

        //                    if (Directory.Exists(ESUpdater.UpdateFolder))
        //                    {
        //                        ESUpdater.Log("Executing launcher.");
        //                        Process proc = new Process();
        //                        proc.StartInfo.FileName = CURRENT_LAUNCHER_NAME;
        //                        SaveConfig();
        //                        proc.Start();
        //                        Restart?.Invoke(null, EventArgs.Empty);
        //                    }
        //                    else
        //                    {
        //                        ESUpdater.Log("Update completed succesfully.");
        //                        UpdateSizeInKb = 0;
        //                        CheckLocalFileVersions();
        //                        DTAVersionState = VersionState.UPTODATE;
        //                        OnUpdateCompleted?.Invoke();

        //                        if (AreCustomComponentsOutdated())
        //                        {
        //                            DoCustomComponentsOutdatedEvent();
        //                        }
        //                    }

        //                }
        //                catch (OperationCanceledException e)
        //                {
        //                    ESUpdater.Log($"Terminating update because of user request. {e}");
        //                    DTAVersionState = VersionState.OUTDATED;
        //                    TerminateUpdate = false;
        //                    return;
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                ESUpdater.Log($"Update Faild: {e}");
        //                DTAVersionState = VersionState.UNKNOWN;
        //                OnUpdateFailed?.Invoke(e);
        //            }
        //        }

        //        public static void CheckLocalFileVersions()
        //        {
        //            if (!_init)
        //                return;
        //            try
        //            {
        //                ESUpdater.Log("Invoke.");
        //                LocalFileInfos.Clear();
        //                var length = OfflineConfig.Files.File.Count;
        //                for (int i = 0; i < length; i++)
        //                {
        //                    UpdateFileInfo file = OfflineConfig.Files.File[i];
        //                    LocalFileCheckProgressChanged?.Invoke(i + 1, length);

        //                    if (!File.Exists(file.Path) && !File.Exists(Path.Combine(ESUpdater.UpdateFolder, file.Path)))
        //                    {
        //                        ESUpdater.Log($"Warning: Offline file \"{file.Path}\" was not found");
        //                        LocalFileInfos.Add(file);
        //                    }
        //                    else if (!ESUpdater.GetFileHash(file).Equals(file.Hash, StringComparison.OrdinalIgnoreCase))
        //                    {
        //                        ESUpdater.Log($"Warning: Offline file \"{file.Path}\" hash was not equals that offline hash cache.");
        //                        LocalFileInfos.Add(file);
        //                    }
        //                }
        //                OnLocalFileVersionsChecked?.Invoke();
        //                LocalFileVersionsChecked = true;
        //            }
        //            catch (Exception e)
        //            {
        //                ESUpdater.Log($"An Except has been throw {e}");
        //                throw;
        //            }
        //        }
        //        public static event NoParamEventHandler OnVersionStateChanged;
        //        public static event UpdateProgressChangedCallback UpdateProgressChanged;
        //        public static event NoParamEventHandler OnUpdateCompleted;
        //        public static event SetExceptionCallback OnUpdateFailed;
        //        public static event NoParamEventHandler FileIdentifiersUpdated;
        //        public static event LocalFileCheckProgressChangedCallback LocalFileCheckProgressChanged;


        //        public static List<DTAFileInfo> LocalFileInfos { get; } = new List<DTAFileInfo>();
        //        public static bool LocalFileVersionsChecked = false;

        //        public static bool CreateShortcutOnInstall = false;
        //        public static volatile bool HasVersionBeenChecked;
        //        public static event NoParamEventHandler OnLocalFileVersionsChecked;
        //        public static event EventHandler Restart;
        //        // TODO

        //        public static CustomComponent[] CustomComponents { get; private set; }
        //        public static ServerDetailsInfo ServerDetailsInfo { get; private set; }
        //        public static OfflineConfig OfflineConfig { get; private set; }

        //        public static event NoParamEventHandler BeforeSelfUpdate;
        //        public static event NoParamEventHandler OnCustomComponentsOutdated;

        //        [Obsolete]
        //        public const string LAUNCHER_UPDATER = "clientupdt.dat";

        //        private static void OnBeforeSelfUpdate() => BeforeSelfUpdate?.Invoke();
        //        private static void DoCustomComponentsOutdatedEvent() => OnCustomComponentsOutdated?.Invoke();

        //        private static bool AreCustomComponentsOutdated()
        //        {
        //            ESUpdater.Log("Checking if custom components are outdated.");
        //            CustomComponent[] customComponents = CustomComponents;
        //            foreach (CustomComponent customComponent in customComponents)
        //            {
        //                if (File.Exists(customComponent.LocalPath) && customComponent.RemoteIdentifier != customComponent.LocalIdentifier)
        //                {
        //                    return true;
        //                }
        //            }
        //            return false;
        //        }

    }

}
