using Models.RoleDTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ModuleDTOs
{
    public class AddModuleAccessRightsDTO
    {
        public int Id { get; set; }
        [Required]
        public int ModuleId { get; set; }
        public int RoleId { get; set; }
        public bool ViewRight { get; set; }
        public bool AddRight { get; set; }
        public bool EditRight { get; set; }
        public bool DeleteRight { get; set; }
    }
}
