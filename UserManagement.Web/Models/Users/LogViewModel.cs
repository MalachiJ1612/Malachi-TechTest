using System;

namespace UserManagement.Web.Models.Users
{
    public class LogViewModel
    {
        public string Action { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}
