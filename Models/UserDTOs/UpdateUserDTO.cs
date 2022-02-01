using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.UserDTOs
{
    public class UpdateUserDTO
    {
        public int Id { get; set; }
        public int UserLevel { get; set; }
        public int UserLevelId { get; set; }
        public int RoleId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public bool IsEnabled { get; set; }
    }
}
