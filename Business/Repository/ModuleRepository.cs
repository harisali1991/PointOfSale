using AutoMapper;
using Business.Repository.IRepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public ModuleRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<ModuleDTO> CreateModule(ModuleDTO moduleDTO)
        {
            tblModule module = _mapper.Map<ModuleDTO, tblModule>(moduleDTO);

            var addedModule = await _db.tblModules.AddAsync(module);

            await _db.SaveChangesAsync();
            return _mapper.Map<tblModule, ModuleDTO>(addedModule.Entity);
        }

        public async Task<int> Delete(int moduleId)
        {
            try
            {
                var module = await _db.tblModules.FindAsync(moduleId);
                if (module != null)
                {
                    _db.tblModules.Remove(module);
                    return await _db.SaveChangesAsync();
                }
                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<IEnumerable<ModuleDTO>> GetAllModules()
        {
            try
            {
                IEnumerable<ModuleDTO> moduleDTOs =
                        _mapper.Map<IEnumerable<tblModule>, IEnumerable<ModuleDTO>>(_db.tblModules);
                return moduleDTOs;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ModuleDTO> GetModule(int moduleId)
        {
            try
            {
                ModuleDTO moduleDTO =
                    _mapper.Map<tblModule, ModuleDTO>(await _db.tblModules.FirstOrDefaultAsync(x => x.Id == moduleId));
                return moduleDTO;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ModuleDTO> UpdateModule(int moduleId, ModuleDTO moduleDTO)
        {
            try
            {
                if (moduleId == moduleDTO.Id)
                {
                    var moduleDetail = await _db.tblModules.FindAsync(moduleId);
                    tblModule module = _mapper.Map<ModuleDTO, tblModule>(moduleDTO, moduleDetail);
                    var updatedModule = _db.tblModules.Update(module);
                    await _db.SaveChangesAsync();
                    return _mapper.Map<tblModule, ModuleDTO>(updatedModule.Entity);
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
        public async Task<ModuleDTO> IsAlreadyExist(string _name)
        {
            try
            {
                ModuleDTO moduleDTO =
                    _mapper.Map<tblModule, ModuleDTO>(await _db.tblModules.FirstOrDefaultAsync(x => x.ModuleName == _name));
                return moduleDTO;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
