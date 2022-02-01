using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.UserDTOs
{
    public class RegisterDTO
    {
        public int Id { get; set; }
        [Required]
        public int UserLevel { get; set; }
        public int UserLevelId { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Password { get; set; }
        
        [Required]
        [EmailAddress(ErrorMessage ="Invalid email address.")]
        public string Email { get; set; }
        [Required]
        public string ContactNumber { get; set; }
        public bool IsEnabled { get; set; }
    }
}
