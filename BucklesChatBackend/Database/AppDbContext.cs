using BucklesChatBackend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BucklesChatBackend.Database
{
    /// <summary>
    /// The application database context, which is the communuication layer between the database and the program, and allows for the database to be edited and modified
    /// </summary>
    public class AppDbContext: DbContext
    {
        /// <summary>
        /// The constructor, which is used to initialize the DbContext
        /// </summary>
        /// <param name="options">The dbcontext options that are injected and then passed into the super constructor</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// The user db set, which is the table that contains User information
        /// </summary>
        public DbSet<DBUser> Users { get; set; }
    }
}
