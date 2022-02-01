﻿using Models.ModuleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RoleDTOs
{
    public class RoleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<int> RoleType { get; set; }
        public bool IsEnabled { get; set; }
        public virtual ICollection<ModuleAccessRightsDTO> tblModuleAccessRights { get; set; }
    }
}
