using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Models;

namespace UserManagement.Services.Domain.Interfaces
{
    public interface ILogService
    {
        /// <summary>
        /// Get all logs.
        /// </summary>
        Task<IEnumerable<Log>> GetAllAsync();

        /// <summary>
        /// Get a single log by Id.
        /// </summary>
        Task<Log?> GetByIdAsync(long id);

        /// <summary>
        /// Get all logs for a specific user.
        /// </summary>
        Task<IEnumerable<Log>> GetByUserIdAsync(long userId);

        /// <summary>
        /// Create a new log.
        /// </summary>
        Task CreateAsync(Log log);
    }
}
