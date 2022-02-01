using Models;
using Models.RoleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IRoleRepository
    {
        public Task<IEnumerable<RoleDTO>> GetAllRoles();
        public Task<AddRoleDTO> GetRole(int roleId);
        public Task<AddRoleDTO> CreateRole(AddRoleDTO roleDTO);
        public Task<AddRoleDTO> UpdateRole(int roleId, AddRoleDTO roleDTO);
        public Task<AddRoleDTO> IsAlreadyExist(string _name);
        public Task<int> Delete(int roleId);
    }
}
