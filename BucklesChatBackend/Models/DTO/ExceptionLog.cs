using Newtonsoft.Json;

namespace BucklesChatBackend.Models.DTO
{
    [JsonObject]
    public class ExceptionLog
    {
        public ExceptionLog(Exception ex, string? _id = null)
        {
            Id = _id ?? Guid.NewGuid().ToString();
            Message = ex.Message;
            StackTrace = ex.StackTrace;
            Timestamp = DateTime.Now.Ticks;
        }

        [JsonProperty("id")]
        public string? Id { get; set; } = null;

        [JsonProperty("message")]
        public string? Message { get; set; } = null;

        [JsonProperty("type")]
        public string? Type { get; set; } = null;

        [JsonProperty("stackTrace")]
        public string? StackTrace { get; set; } = null;

        [JsonProperty("timestamp")]
        public long? Timestamp { get; set; } = null;

    }
}
