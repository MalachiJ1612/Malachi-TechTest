using System.Threading.Tasks;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Web.Controllers
{
    [Route("logs")]
    public class LogsController : Controller
    {
        private readonly ILogService _logService;

        public LogsController(ILogService logService)
        {
            _logService = logService;
        }

        // ================================
        // LIST ALL LOGS
        // ================================
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var logs = await _logService.GetAllAsync();
            return View(logs);
        }

        // ================================
        // VIEW LOG DETAILS
        // ================================
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(long id)
        {
            var log = await _logService.GetByIdAsync(id);
            if (log == null)
                return NotFound();

            return View(log);
        }
    }
}
