using BucklesChatBackend.Models.DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BucklesChatBackend.Models.Entities
{
    [Table("bucklesuser")]
    public class DBUser
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public ulong Id { get; set; }

        [Column("first_name")]
        [StringLength(70, ErrorMessage = "First Name cannot be more than 70 characters long")]
        public string? FirstName { get; set; }


        [Column("last_name")]
        [StringLength(70, ErrorMessage = "Last Name cannot be more than 70 characters long")]
        public string? LastName { get; set; }

        [Column("email")]
        [StringLength(120, ErrorMessage = "Email cannot be more than 120 characters long")]
        [EmailAddress]
        public string? Email { get; set; }

        [Column("handle")]
        [StringLength(12, ErrorMessage = "Handle cannot be more than 12 characters long")]
        public string? Handle { get; set; }

        [Column("dob")]
        public ulong? Dob { get; set; }

        [Column("username")]
        public string? Username { get; set; }

        [Column("password")]
        [MinLength(7, ErrorMessage = "Password must be at least 7 characters long")]
        [MaxLength(30, ErrorMessage = "Password cannot be more then 30 characters")]
        public string? Password { get; set; }

        [Column("password_salt")]
        public string? PasswordSalt { get; set; }

        public void ApplyChanges(BucklesChatUser user)
        {
            FirstName = user.FirstName ?? FirstName;
            LastName = user.LastName ?? LastName;
            Email = user.Email ?? Email;
            Handle = user.Handle ?? Handle;
            Dob = user.Dob ?? Dob;
            Username = user.Username ?? Username;
            Password = user.Password ?? Password;
            PasswordSalt = user.PasswordSalt ?? PasswordSalt;
        }

        public BucklesChatUser ConvertToLocal()
        {
            return new BucklesChatUser
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Dob = Dob,
                Id = Id,
                Handle = Handle,
                Username = Username,
                Password = Password,
                PasswordSalt = PasswordSalt
            };
        }

    }
}
