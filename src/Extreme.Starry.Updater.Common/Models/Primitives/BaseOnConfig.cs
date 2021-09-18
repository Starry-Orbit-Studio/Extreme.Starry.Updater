using System;
using System.Xml.Serialization;

#if !NET45
using System.Text.Json.Serialization;
#else
using JsonPropertyName = Newtonsoft.Json.JsonPropertyAttribute;
using JsonIgnore = Newtonsoft.Json.JsonIgnoreAttribute;
#endif


namespace Extreme.Starry.Updater.Common.Models.Primitives
{
    public abstract class BaseOnConfig
    {
        [JsonPropertyName("$baseOn")]
        [XmlAttribute(AttributeName = "_baseOn")]
#if !NET45
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
        public Uri BaseOnUrl { get; set; }
    }
}
