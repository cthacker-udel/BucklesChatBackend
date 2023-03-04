using BucklesChatBackend.Database;
using BucklesChatBackend.Models.Classes;
using BucklesChatBackend.Models.Entities;

namespace BucklesChatBackend.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly AppDbContext appDbContext;

        public UserRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public bool DoesUserWithUsernameExist(string username)
        {
            if (String.IsNullOrEmpty(username)) return true;
            return this.appDbContext.Users.Any(e => e.Username != null && e.Username.Equals(username));
        }

        public async Task<bool> AddUser(LocalUser user)
        {
            this.appDbContext.Add(user);
            var addResult = await this.appDbContext.SaveChangesAsync();

            return addResult > 0;
        }

        public async Task<bool> DeleteUser(ulong userId)
        {
            var foundEntity = this.appDbContext.Users.FirstOrDefault(eachUser => eachUser.Id == userId);

            if (foundEntity == null) return false;

            var deleteResult = this.appDbContext.Remove(foundEntity);

            if (deleteResult == null) return false;

            var deleteCount = await this.appDbContext.SaveChangesAsync();
            return deleteCount > 0;
        }

        public IEnumerable<DBUser> GetAllUsers()
        {
            return this.appDbContext.Users.ToList();
        }

        public IEnumerable<LocalUser> GetAllLocalUsers()
        {
            return this.appDbContext.Users.ToList().Select(e => e.ConvertToLocal()).ToList();
        }

        public async Task<bool> UpdateUser(ulong Id, LocalUser user)
        {
            var foundEntity = this.appDbContext.Users.FirstOrDefault(eachUser => eachUser.Id == Id);

            if (foundEntity == null) return false;

            foundEntity.ApplyChanges(user);

            var result = await this.appDbContext.SaveChangesAsync();

            return result > 0;

        }
    }
}
