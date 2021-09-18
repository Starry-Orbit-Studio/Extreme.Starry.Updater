using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Extreme.Starry.Updater.Common.Models.Primitives;

#if !NET45
using System.Text.Json.Serialization;
#else
using JsonPropertyName = Newtonsoft.Json.JsonPropertyAttribute;
using JsonIgnore = Newtonsoft.Json.JsonIgnoreAttribute;
#endif


namespace Extreme.Starry.Updater.Common.Models
{
    [XmlRoot(ElementName = "file")]
    public sealed class DownloadFileInfo : BaseOnConfig
    {
        [JsonPropertyName("hash")]
        [XmlAttribute(AttributeName = "hash")]
        public string Hash { get; set; }

        [JsonPropertyName("path")]
        [XmlAttribute(AttributeName = "path")]
        public string Path { get; set; }

        [JsonPropertyName("size")]
        [XmlAttribute(AttributeName = "size")]

        public long Size { get; set; }
        [JsonPropertyName("url")]
        [XmlAttribute(AttributeName = "url")]
        public Uri Url { get; set; }

        [JsonPropertyName("remove")]
        [XmlAttribute(AttributeName = "remove")]
#if !NET45
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
        public bool WillBeRemoved { get; set; }

        [JsonPropertyName("movefrom")]
        [XmlAttribute(AttributeName = "movefrom")]
#if !NET45
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
        public string WillBeMovedFrom { get; set; }
    }
}
