using Models.ModuleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IPermissionRepository
    {
        public Task<IEnumerable<ModuleAccessRightsDTO>> GetAllPermission();
        public Task<IEnumerable<ModuleAccessRightsDTO>> GetAllPermissionByRole(int roleId);
        public Task<ModuleAccessRightsDTO> GetPermission(int permissionId);
        public Task<IEnumerable<AddModuleAccessRightsDTO>> CreatePermissions(IEnumerable<AddModuleAccessRightsDTO> permissionDTOs);
        public Task<IEnumerable<AddModuleAccessRightsDTO>> UpdatePermissions(IEnumerable<AddModuleAccessRightsDTO> permissionDTOs);
        public Task<int> Delete(int permissionId);
    }
}
