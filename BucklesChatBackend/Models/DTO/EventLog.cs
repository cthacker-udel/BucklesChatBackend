using Newtonsoft.Json;

namespace BucklesChatBackend.Models.DTO
{
    [JsonObject]
    public class EventLog
    {

        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("message")]
        public string? Message { get; set; }

    }
}
