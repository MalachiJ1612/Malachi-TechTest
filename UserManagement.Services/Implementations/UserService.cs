using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Data;

namespace UserManagement.Services.Domain.Implementations
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users!.ToListAsync();
        }

        public async Task<IEnumerable<User>> FilterByActiveAsync(bool isActive)
        {
            return await _context.Users!
                .Where(u => u.IsActive == isActive)
                .ToListAsync();
        }

        public async Task<User?> GetByIdAsync(long id)
        {
            return await _context.Users!
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task CreateAsync(User user)
        {
            await _context.Users!.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users!.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var user = await GetByIdAsync(id);
            if (user != null)
            {
                _context.Users!.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
