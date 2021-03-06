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
using JsonIgnore = Newtonsoft.Json.JsonIgnoreAttribute;
#endif

namespace Extreme.Starry.Updater.Common.Models
{

    [XmlRoot(ElementName = "server")]
    public sealed class ServerInfo : BaseOnConfig
    {
        [JsonPropertyName("serverName"), XmlAttribute(AttributeName = "name")]
        public string ServerName { get; set; }

        [JsonIgnore, XmlIgnore]
        public Uri ServerUrl { get; set; }

        [XmlAttribute(AttributeName = "url"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string ServerUrlString
        {
            get => ServerUrl?.ToString();
            set => ServerUrl = value == null ? null : new Uri(value);
        }

        [JsonPropertyName("serverVersion"), XmlIgnore]
        public string ServerVersion { get; set; }

        [JsonPropertyName("current"), XmlIgnore]
        public string CurrentVersion { get; set; }

        [JsonPropertyName("gameId"), XmlIgnore]
        public string GameID { get; set; }

        [JsonPropertyName("versions"), XmlIgnore]
        public List<VersionInfo> Versions { get; set; }
    }
}
