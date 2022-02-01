using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class tblUser
    {
        [Key]
        public int Id { get; set; }
        public Nullable<int> UserLevel { get; set; }
        public int UserLevelId { get; set; }
        public Nullable<int> RoleId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PasswordSat { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string ContactNumber { get; set; }
        public bool IsEnabled { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Nullable<System.DateTime> ModifiedAt { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> ModifiedBy { get; set; }

        [ForeignKey("RoleId")]
        public virtual tblRole tblRole { get; set; }
        public virtual ICollection<tblRefreshToken> tblRefreshTokens { get; set; }
    }
}
