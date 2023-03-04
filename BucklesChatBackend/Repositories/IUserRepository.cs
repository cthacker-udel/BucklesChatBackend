using BucklesChatBackend.Models.Classes;
using BucklesChatBackend.Models.Entities;

namespace BucklesChatBackend.Repositories
{
    public interface IUserRepository
    {
        bool DoesUserWithUsernameExist(string username);
        IEnumerable<DBUser> GetAllUsers();
        IEnumerable<LocalUser> GetAllLocalUsers();
        Task<bool> AddUser(LocalUser user);
        Task<bool> UpdateUser(ulong Id, LocalUser user);
        Task<bool> DeleteUser(ulong userId);
    }
}
