using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserManagement.Models;

namespace UserManagement.Data
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            // ❌ Remove Database.EnsureCreated() because it conflicts with EF migrations
            // ✅ Migrations handle database creation now
        }

        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Log> Logs { get; set; } = default!;

        // ========== SEED INITIAL DATA ==========
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Forename = "Peter", Surname = "Loew", Email = "ploew@example.com", IsActive = true, DateOfBirth = new DateTime(1985, 3, 15) },
                new User { Id = 2, Forename = "Benjamin Franklin", Surname = "Gates", Email = "bfgates@example.com", IsActive = true, DateOfBirth = new DateTime(1974, 10, 25) },
                new User { Id = 3, Forename = "Castor", Surname = "Troy", Email = "ctroy@example.com", IsActive = false, DateOfBirth = new DateTime(1980, 7, 12) },
                new User { Id = 4, Forename = "Memphis", Surname = "Raines", Email = "mraines@example.com", IsActive = true, DateOfBirth = new DateTime(1982, 2, 8) },
                new User { Id = 5, Forename = "Stanley", Surname = "Goodspeed", Email = "sgodspeed@example.com", IsActive = true, DateOfBirth = new DateTime(1979, 9, 30) },
                new User { Id = 6, Forename = "H.I.", Surname = "McDunnough", Email = "himcdunnough@example.com", IsActive = true, DateOfBirth = new DateTime(1987, 4, 20) },
                new User { Id = 7, Forename = "Cameron", Surname = "Poe", Email = "cpoe@example.com", IsActive = false, DateOfBirth = new DateTime(1990, 12, 2) },
                new User { Id = 8, Forename = "Edward", Surname = "Malus", Email = "emalus@example.com", IsActive = false, DateOfBirth = new DateTime(1988, 6, 17) },
                new User { Id = 9, Forename = "Damon", Surname = "Macready", Email = "dmacready@example.com", IsActive = false, DateOfBirth = new DateTime(1991, 8, 5) },
                new User { Id = 10, Forename = "Johnny", Surname = "Blaze", Email = "jblaze@example.com", IsActive = true, DateOfBirth = new DateTime(1983, 11, 11) },
                new User { Id = 11, Forename = "Robin", Surname = "Feld", Email = "rfeld@example.com", IsActive = true, DateOfBirth = new DateTime(1975, 1, 28) }
            );
        }

        // ========== GENERIC ASYNC DATA OPERATIONS ==========

        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class
            => Set<TEntity>();

        public async Task CreateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            await Set<TEntity>().AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task UpdateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            Set<TEntity>().Update(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class
        {
            Set<TEntity>().Remove(entity);
            await SaveChangesAsync();
        }
    }
}
