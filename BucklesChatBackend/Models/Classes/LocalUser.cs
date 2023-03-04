using BucklesChatBackend.Models.Entities;

namespace BucklesChatBackend.Models.Classes
{
    public class LocalUser
    {
        public ulong? Id { get; set; }
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? Handle { get; set; }

        public ulong? DateOfBirth { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public string? PasswordSalt { get; set; }

    }
}
