using Newtonsoft.Json;

namespace GUI
{
    public class ResponseFromServer
    {
        [JsonProperty("img")]
        public string img { get; set; }
        [JsonProperty("url")]
        public string url { get; set; }
        [JsonProperty("desc")]
        public string desc { get; set; }
        [JsonProperty("title")]
        public string title { get; set; }
    }
}