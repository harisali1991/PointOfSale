using AutoMapper;
using Business.Repository.IRepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models.ModuleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public PermissionRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ModuleAccessRightsDTO>> GetAllPermission()
        {
            try
            {
                IEnumerable<ModuleAccessRightsDTO> permissionDTOs =
                        _mapper.Map<IEnumerable<tblModuleAccessRight>, IEnumerable<ModuleAccessRightsDTO>>(_db.tblModuleAccessRights.Include(x=>x.tblModule));
                return permissionDTOs;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<IEnumerable<AddModuleAccessRightsDTO>> CreatePermissions(IEnumerable<AddModuleAccessRightsDTO> permissionDTOs)
        {
            try
            {
                IEnumerable<tblModuleAccessRight> permissions =
                _mapper.Map<IEnumerable<AddModuleAccessRightsDTO>, IEnumerable<tblModuleAccessRight>>(permissionDTOs);
                await _db.tblModuleAccessRights.AddRangeAsync(permissions);
                await _db.SaveChangesAsync();
                return _mapper.Map<IEnumerable<tblModuleAccessRight>, IEnumerable<AddModuleAccessRightsDTO>>(permissions);
            }
            catch
            {
                return null;
            }
        }
        public Task<int> Delete(int permissionId)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<ModuleAccessRightsDTO>> GetAllPermissionByRole(int roleId)
        {
            try
            {
                IEnumerable<ModuleAccessRightsDTO> permissionDTO =
                    _mapper.Map<IEnumerable<tblModuleAccessRight>, IEnumerable<ModuleAccessRightsDTO>>(_db.tblModuleAccessRights.Where(x => x.RoleId == roleId));

                return permissionDTO;
            }
            catch
            {
                return null;
            }
        }

        public Task<ModuleAccessRightsDTO> GetPermission(int permissionId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AddModuleAccessRightsDTO>> UpdatePermissions(IEnumerable<AddModuleAccessRightsDTO> permissionDTOs)
        {
            try
            {
                IEnumerable<tblModuleAccessRight> permissions =
                _mapper.Map<IEnumerable<AddModuleAccessRightsDTO>, IEnumerable<tblModuleAccessRight>>(permissionDTOs);
                _db.tblModuleAccessRights.UpdateRange(permissions);
                await _db.SaveChangesAsync();
                return _mapper.Map<IEnumerable<tblModuleAccessRight>, IEnumerable<AddModuleAccessRightsDTO>>(permissions);
            }
            catch
            {
                return null;
            }
        }
    }
}
