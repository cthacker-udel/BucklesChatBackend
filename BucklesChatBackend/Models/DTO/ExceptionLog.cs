using Newtonsoft.Json;

namespace BucklesChatBackend.Models.DTO
{
    [JsonObject]
    public class ExceptionLog
    {
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("message")]
        public string? Message { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("stackTrace")]
        public string? StackTrace { get; set; }

        [JsonProperty("timestamp")]
        public long? Timestamp { get; set; }

    }
}
