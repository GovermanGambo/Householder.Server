using Newtonsoft.Json;

namespace Householder.Server.Models
{
    public class Resident
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}