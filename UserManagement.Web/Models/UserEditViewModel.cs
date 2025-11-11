using System;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Web.Models.Users
{
    public class UserEditViewModel
    {
        public long Id { get; set; }

        [Required, StringLength(50)]
        public string Forename { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string Surname { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
    }
}
