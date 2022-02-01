using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class tblRole
    {
        public tblRole()
        {
            this.tblModuleAccessRights = new HashSet<tblModuleAccessRight>();
            this.tblUsers = new HashSet<tblUser>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<int> RoleType { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsVisible { get; set; }
        public virtual ICollection<tblModuleAccessRight> tblModuleAccessRights { get; set; }
        public virtual ICollection<tblUser> tblUsers { get; set; }
    }
}
