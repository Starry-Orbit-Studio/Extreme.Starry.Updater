using System;
using System.Xml.Serialization;
using System.Security.Policy;
using System.ComponentModel;

#if !NET45
using System.Text.Json.Serialization;
#else
using JsonPropertyName = Newtonsoft.Json.JsonPropertyAttribute;
#endif


namespace Extreme.Starry.Updater.Common.Models.Primitives
{
    public abstract class BaseOnConfig
    {
        [JsonPropertyName("$baseOn")]
#if !NET45
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
        public Uri BaseOnUrl { get; set; }


        [XmlAttribute(AttributeName = "_baseOn"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string BaseOnUrlString
        {
            get => BaseOnUrl?.ToString();
            set => BaseOnUrl = value == null ? null : new Uri(value);
        }
    }
}
