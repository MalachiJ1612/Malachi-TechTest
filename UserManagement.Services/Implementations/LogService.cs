using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserManagement.Data;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Services.Domain.Implementations
{
    public class LogService : ILogService
    {
        private readonly DataContext _context;

        public LogService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Log>> GetAllAsync()
        {
            return await _context.Logs.ToListAsync();
        }

        public async Task<Log?> GetByIdAsync(long id)
        {
            return await _context.Logs.FindAsync(id);
        }

        public async Task<IEnumerable<Log>> GetByUserIdAsync(long userId)
        {
            return await _context.Logs
                .Where(l => l.UserId == userId)
                .OrderByDescending(l => l.Timestamp)
                .ToListAsync();
        }

        public async Task CreateAsync(Log log)
        {
            _context.Logs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}
