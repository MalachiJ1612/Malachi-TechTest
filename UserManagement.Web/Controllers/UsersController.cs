using System;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;

namespace UserManagement.Web.Controllers
{
    [Route("users")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogService _logService;

        public UsersController(IUserService userService, ILogService logService)
        {
            _userService = userService;
            _logService = logService;
        }

        // ================================
        // LIST USERS
        // ================================
        [HttpGet("")]
        public async Task<IActionResult> List(string filter)
        {
            var users = await _userService.GetAllAsync();

            if (!string.IsNullOrEmpty(filter))
            {
                if (filter.Equals("active", StringComparison.OrdinalIgnoreCase))
                    users = users.Where(u => u.IsActive);
                else if (filter.Equals("inactive", StringComparison.OrdinalIgnoreCase))
                    users = users.Where(u => !u.IsActive);
            }

            var items = users.Select(p => new UserListItemViewModel
            {
                Id = p.Id,
                Forename = p.Forename,
                Surname = p.Surname,
                Email = p.Email,
                IsActive = p.IsActive,
                DateOfBirth = p.DateOfBirth
            }).ToList();

            return View(new UserListViewModel { Items = items, Filter = filter });
        }

        // ================================
        // VIEW USER (with logs)
        // ================================
        [HttpGet("view/{id}")]
        public async Task<IActionResult> View(long id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            // Get all logs for this user
            var logs = await _logService.GetByUserIdAsync(user.Id);

            // Log the fact that user was viewed
            await _logService.CreateAsync(new Log
            {
                UserId = user.Id,
                Action = "Viewed user",
                Description = $"Viewed details for {user.Forename} {user.Surname}",
                Timestamp = DateTime.UtcNow
            });

            // Use ViewBag to pass both objects
            ViewBag.User = user;
            ViewBag.Logs = logs?.ToList() ?? new List<Log>();

            return View();
        }

        // ================================
        // ADD USER
        // ================================
        [HttpGet("add")]
        public IActionResult Add() => View(new UserEditViewModel());

        [HttpPost("add")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(UserEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new User
            {
                Forename = model.Forename,
                Surname = model.Surname,
                Email = model.Email,
                IsActive = model.IsActive,
                DateOfBirth = model.DateOfBirth
            };

            await _userService.CreateAsync(user);

            await _logService.CreateAsync(new Log
            {
                UserId = user.Id,
                Action = "Created user",
                Description = $"Added new user {user.Forename} {user.Surname}",
                Timestamp = DateTime.UtcNow
            });

            return RedirectToAction("List");
        }

        // ================================
        // EDIT USER
        // ================================
        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(long id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            var model = new UserEditViewModel
            {
                Id = user.Id,
                Forename = user.Forename,
                Surname = user.Surname,
                Email = user.Email,
                IsActive = user.IsActive,
                DateOfBirth = user.DateOfBirth
            };

            return View(model);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, UserEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            user.Forename = model.Forename;
            user.Surname = model.Surname;
            user.Email = model.Email;
            user.IsActive = model.IsActive;
            user.DateOfBirth = model.DateOfBirth;

            await _userService.UpdateAsync(user);

            await _logService.CreateAsync(new Log
            {
                UserId = user.Id,
                Action = "Edited user",
                Description = $"Edited user {user.Forename} {user.Surname}",
                Timestamp = DateTime.UtcNow
            });

            return RedirectToAction("List");
        }

        // ================================
        // DELETE USER
        // ================================
        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            await _userService.DeleteAsync(id);

            await _logService.CreateAsync(new Log
            {
                UserId = id,
                Action = "Deleted user",
                Description = $"Deleted user {user.Forename} {user.Surname}",
                Timestamp = DateTime.UtcNow
            });

            return RedirectToAction("List");
        }
    }
}
