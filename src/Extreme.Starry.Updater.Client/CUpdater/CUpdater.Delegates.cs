using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater
{
    public delegate void NoParamEventHandler();
    public delegate void SetExceptionCallback(Exception ex);
    public delegate void LocalFileCheckProgressChangedCallback(int checkedFileCount, int totalFileCount);
    public delegate void UpdateProgressChangedCallback(string currFileName, int currFilePercentage, int totalPercentage);


    public delegate void DownloadFinishedEventHandler(CustomComponent cc, bool success);
    public delegate void DownloadProgressChangedEventHandler(CustomComponent cc, int percentage);
}
