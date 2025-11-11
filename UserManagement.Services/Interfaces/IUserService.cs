using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Models;

namespace UserManagement.Services.Domain.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Return users by active state.
        /// </summary>
        Task<IEnumerable<User>> FilterByActiveAsync(bool isActive);

        /// <summary>
        /// Get all users.
        /// </summary>
        Task<IEnumerable<User>> GetAllAsync();

        /// <summary>
        /// Get one user by Id.
        /// </summary>
        Task<User?> GetByIdAsync(long id);

        /// <summary>
        /// Create a new user.
        /// </summary>
        Task CreateAsync(User user);

        /// <summary>
        /// Update an existing user.
        /// </summary>
        Task UpdateAsync(User user);

        /// <summary>
        /// Delete user by Id.
        /// </summary>
        Task DeleteAsync(long id);
    }
}
