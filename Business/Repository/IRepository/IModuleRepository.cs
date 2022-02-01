using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IModuleRepository
    {
        public Task<ModuleDTO> CreateModule(ModuleDTO moduleDTO);
        public Task<ModuleDTO> UpdateModule(int moduleId,ModuleDTO moduleDTO);
        public Task<ModuleDTO> GetModule(int moduleId);
        public Task<IEnumerable<ModuleDTO>> GetAllModules();
        public Task<ModuleDTO> IsAlreadyExist(string _name);
        public Task<int> Delete(int moduleId);
    }
}
