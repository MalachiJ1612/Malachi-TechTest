using System; // ✅ Required for DateTime
namespace UserManagement.Models
{
    public class Log
    {
        public long Id { get; set; }
        public long? UserId { get; set; } // nullable if system-wide log
        public string Action { get; set; } = string.Empty; // e.g. "User Created", "User Deleted"
        public string? Description { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow; // ✅ uses System.DateTime
    }
}
