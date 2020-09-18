using Newtonsoft.Json;

namespace Householder.Server.Models
{
    public class Resident
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}