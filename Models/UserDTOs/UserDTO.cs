using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.UserDTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public Nullable<int> UserLevel { get; set; }
        public int UserLevelId { get; set; }
        public Nullable<int> RoleId { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public bool IsEnabled { get; set; }

        public DateTime CreatedAt { get; set; }
        public Nullable<System.DateTime> ModifiedAt { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> ModifiedBy { get; set; }

    }
}
