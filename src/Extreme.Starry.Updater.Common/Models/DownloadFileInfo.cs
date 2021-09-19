using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Extreme.Starry.Updater.Common.Models.Primitives;
using System.ComponentModel;

#if !NET45
using System.Text.Json.Serialization;
#else
using JsonPropertyName = Newtonsoft.Json.JsonPropertyAttribute;
#endif


namespace Extreme.Starry.Updater.Common.Models
{
    [XmlRoot(ElementName = "file")]
    public sealed class DownloadFileInfo : BaseOnConfig
    {
        [JsonPropertyName("hash"), XmlAttribute(AttributeName = "hash")]
        public string Hash { get; set; }

        [JsonPropertyName("path"), XmlAttribute(AttributeName = "path")]
        public string Path { get; set; }

        [JsonPropertyName("size"), XmlAttribute(AttributeName = "size")]

        public long Size { get; set; }

        [JsonPropertyName("url"), XmlIgnore]
        public Uri Url { get; set; }

        [XmlAttribute(AttributeName = "url"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string UrlString
        {
            get => Url?.ToString();
            set => Url = value == null ? null : new Uri(value);
        }

        [JsonPropertyName("remove"), XmlAttribute(AttributeName = "remove")]
#if !NET45
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
        public bool WillBeRemoved { get; set; }

        [JsonPropertyName("movefrom"), XmlAttribute(AttributeName = "movefrom")]
#if !NET45
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
        public string WillBeMovedFrom { get; set; }
    }
}
