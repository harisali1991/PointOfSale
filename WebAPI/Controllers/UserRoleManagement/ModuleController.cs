using Business.Repository.IRepository;
using DataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers.UserRoleManagement
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : ControllerBase
    {
        private readonly IModuleRepository _moduleRepository;
        public ModuleController(IModuleRepository moduleRepository)
        {
            _moduleRepository = moduleRepository;
        }
        [Route("GetAllModules")]
        [HttpGet]
        public async Task<IActionResult> GetModules()
        {
            var allModules = await _moduleRepository.GetAllModules();
            return Ok(new ResponseModel()
            {
                Status = true,
                Message = "",
                Data = allModules
            });
        }
        [Route("GetModule")]
        [HttpGet]
        public async Task<IActionResult> GetModule(int? moduleId)
        {
            if (moduleId == null)
                return BadRequest(new ResponseModel()
                {
                    Status = false,
                    Message = "Invalid Module Id",
                    Data = null
                });

            var objModule = await _moduleRepository.GetModule(moduleId.Value);

            if (objModule == null)
                return NotFound(new ResponseModel()
                {
                    Status = false,
                    Message = "Invalid Module Id",
                    Data = null
                });
            return Ok(new ResponseModel()
            {
                Status = true,
                Message = "",
                Data = objModule
            });
        }
        [Route("CreateModule")]
        [HttpPost]
        public async Task<IActionResult> CreateModule([FromBody] ModuleDTO moduleDTO)
        {
            if (moduleDTO == null || !ModelState.IsValid)
                return BadRequest();

            var IsExist = await _moduleRepository.IsAlreadyExist(moduleDTO.ModuleName);
            if (IsExist != null)
            {
                return BadRequest(new ResponseModel()
                {
                    Status = false,
                    Message = "Module already exist",
                    Data = null
                });
            }
            var module = await _moduleRepository.CreateModule(moduleDTO);
            if (module == null)
                return BadRequest();

            return Ok(new ResponseModel()
            {
                Status = true,
                Message = "",
                Data = module
            });
        }
        [HttpPut("UpdateModule")]
        public async Task<IActionResult> UpdateModule([FromBody] ModuleDTO moduleDTO)
        {
            if (moduleDTO == null || !ModelState.IsValid)
                return BadRequest();

            var module = await _moduleRepository.UpdateModule(moduleDTO.Id, moduleDTO);
            if (module == null)
                return BadRequest();

            return Ok(new ResponseModel()
            {
                Status = true,
                Message = "",
                Data = module
            });
        }
        [Route("DeleteModule")]
        [HttpDelete]
        public async Task<IActionResult> DeleteRole(int? moduleId)
        {
            if (moduleId == null)
                return BadRequest(new ResponseModel()
                {
                    Status = false,
                    Message = "Unable to delete this module",
                    Data = null
                });

            var module = await _moduleRepository.Delete(moduleId.Value);
            if (module <= 0)
                return BadRequest();

            return Ok();
        }
    }
}
