using BucklesChatBackend.Models.Entities;
using Newtonsoft.Json;

namespace BucklesChatBackend.Models.DTO
{
    [JsonObject(MemberSerialization.OptIn)]
    public class BucklesChatUser
    {
        [JsonProperty("firstName")]
        public string? FirstName { get; set; }

        [JsonProperty("lastName")]
        public string? LastName { get; set; }

        [JsonProperty("email")]
        public string? Email { get; set; }

        [JsonProperty("handle")]
        public string? Handle { get; set; }

        [JsonProperty("dob")]
        public ulong? Dob { get; set; }

        [JsonProperty("username")]
        public string? Username { get; set; }

        [JsonProperty("password")]
        public string? Password { get; set; }

        [JsonProperty("passwordSalt")]
        public string? PasswordSalt { get; set; }

        public ulong? Id { get; set; }

        public DBUser ToDBUser()
        {
            return new DBUser() { FirstName = FirstName, LastName = LastName, Email = Email, Handle = Handle, Dob = Dob, Username = Username, Password = Password, PasswordSalt = PasswordSalt };
        }

    }
}
