using System.Collections.Generic;

using Extreme.Starry.Updater.Common.Models.Primitives;

#if !NET45
using System.Text.Json.Serialization;
#else
using JsonPropertyName = Newtonsoft.Json.JsonPropertyAttribute;
using JsonIgnore = Newtonsoft.Json.JsonIgnoreAttribute;
#endif


namespace Extreme.Starry.Updater.Common.Models
{
    public sealed class VersionInfo : BaseOnConfig
    {
        [JsonPropertyName("files")]
        public List<DownloadFileInfo> FileList { get; set; }

        [JsonPropertyName("gameId")]
        public string GameID { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }
    }
}
