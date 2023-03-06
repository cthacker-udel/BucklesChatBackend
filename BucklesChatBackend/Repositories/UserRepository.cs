using BucklesChatBackend.Database;
using BucklesChatBackend.Models.DTO;
using BucklesChatBackend.Models.Entities;

namespace BucklesChatBackend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext appDbContext;

        public UserRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public bool DoesUserWithUsernameExist(string username)
        {
            if (String.IsNullOrEmpty(username)) return true;
            return appDbContext.Users.Any(e => e.Username != null && e.Username.Equals(username));
        }

        public async Task<bool> AddUser(BucklesChatUser user)
        {
            appDbContext.Users.Add(user);
            int addResult = await appDbContext.SaveChangesAsync();

            return addResult > 0;
        }

        public async Task<bool> DeleteUser(ulong userId)
        {
            DBUser? foundEntity = appDbContext.Users.FirstOrDefault(eachUser => eachUser.Id == userId);

            if (foundEntity == null) return false;

            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<DBUser> deleteResult = appDbContext.Users.Remove(foundEntity);

            if (deleteResult == null) return false;

            int deleteCount = await appDbContext.SaveChangesAsync();
            return deleteCount > 0;
        }

        public IEnumerable<DBUser> GetAllUsers()
        {
            return appDbContext.Users.ToList();
        }

        public IEnumerable<BucklesChatUser> GetAllLocalUsers()
        {
            return appDbContext.Users.ToList().Select(e => e.ConvertToLocal()).ToList();
        }

        public async Task<bool> UpdateUser(ulong Id, BucklesChatUser user)
        {
            DBUser? foundEntity = appDbContext.Users.FirstOrDefault(eachUser => eachUser.Id == Id);

            if (foundEntity == null) return false;

            foundEntity.ApplyChanges(user);

            int result = await appDbContext.SaveChangesAsync();

            return result > 0;

        }
    }
}
