using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SimpleBlog.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace SimpleBlog.Models.ViewModels
{
    public class CreateUserModel {
        [Required(ErrorMessage = "Please, enter a name")]
        [StringLength(20, ErrorMessage = "{0} length, must be between {2} and {1}", MinimumLength = 3)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please, enter an email")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please, enter a password")]
        [StringLength(30, ErrorMessage = "{0} length, must be between {2} and {1}", MinimumLength = 6)]
        public string Password { get; set; }
    }
    
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
    
    public class RoleEditModel {
        public IdentityRole Role { get; set; }
        public IEnumerable<User> Members { get; set; }
        public IEnumerable<User> NonMembers { get; set; }
    }
    public class RoleModificationModel {
        [Required]
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set; }
    }
}