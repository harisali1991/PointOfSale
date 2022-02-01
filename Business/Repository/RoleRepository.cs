using AutoMapper;
using Business.Repository.IRepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.RoleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public RoleRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<AddRoleDTO> CreateRole(AddRoleDTO roleDTO)
        {
            tblRole role = _mapper.Map<AddRoleDTO, tblRole>(roleDTO);

            role.IsVisible = true;
            var addedRole = await _db.tblRoles.AddAsync(role);

            await _db.SaveChangesAsync();
            return _mapper.Map<tblRole, AddRoleDTO>(addedRole.Entity);
        }

        public async Task<int> Delete(int roleId)
        {
            try
            {
                var role = await _db.tblRoles.FindAsync(roleId);
                if (role != null)
                {
                    _db.tblRoles.Remove(role);
                    return await _db.SaveChangesAsync();
                }
                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<IEnumerable<RoleDTO>> GetAllRoles()
        {
            try
            {
                IEnumerable<RoleDTO> roleDTOs =
                        _mapper.Map<IEnumerable<tblRole>, IEnumerable<RoleDTO>>(_db.tblRoles);
                return roleDTOs;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<AddRoleDTO> GetRole(int roleId)
        {
            try
            {
                AddRoleDTO roleDTO =
                    _mapper.Map<tblRole, AddRoleDTO>(await _db.tblRoles.FirstOrDefaultAsync(x => x.Id == roleId));
                return roleDTO;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<AddRoleDTO> IsAlreadyExist(string _name)
        {
            try
            {
                AddRoleDTO roleDTO =
                    _mapper.Map<tblRole, AddRoleDTO>(await _db.tblRoles.FirstOrDefaultAsync(x => x.Name == _name));
                return roleDTO;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<AddRoleDTO> UpdateRole(int roleId, AddRoleDTO roleDTO)
        {
            try
            {
                if (roleId == roleDTO.Id)
                {
                    var roleDetail = await _db.tblRoles.FindAsync(roleId);
                    tblRole role = _mapper.Map<AddRoleDTO, tblRole>(roleDTO, roleDetail);
                    role.IsVisible = true;
                    var updatedRole = _db.tblRoles.Update(role);
                    await _db.SaveChangesAsync();
                    return _mapper.Map<tblRole, AddRoleDTO>(updatedRole.Entity);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
