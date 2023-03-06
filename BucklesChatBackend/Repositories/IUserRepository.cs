using BucklesChatBackend.Models.DTO;
using BucklesChatBackend.Models.Entities;

namespace BucklesChatBackend.Repositories
{
    public interface IUserRepository
    {
        bool DoesUserWithUsernameExist(string username);
        IEnumerable<DBUser> GetAllUsers();
        IEnumerable<BucklesChatUser> GetAllLocalUsers();
        Task<bool> AddUser(BucklesChatUser user);
        Task<bool> UpdateUser(ulong Id, BucklesChatUser user);
        Task<bool> DeleteUser(ulong userId);
    }
}
