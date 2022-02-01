using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RoleDTOs
{
    public class AddRoleDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Role name should be given")]
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<int> RoleType { get; set; }
        public bool IsEnabled { get; set; }
    }
}
