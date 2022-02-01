using Business.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ModuleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers.UserRoleManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionRepository _permissionRepository;
        public PermissionController(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }
        [Route("GetAllPermission")]
        [HttpGet]
        public async Task<IActionResult> GetAllPermission()
        {
            var allPermission = await _permissionRepository.GetAllPermission();
            if (allPermission == null)
            {
                return BadRequest(new ResponseModel()
                {
                    Status = false,
                    Message = "Something is wrong!",
                    Data = null
                });
            }
            return Ok(new ResponseModel()
            {
                Status = true,
                Message = "",
                Data = allPermission
            });
        }
        [Route("GetAllPermissionByRole")]
        [HttpGet]
        public async Task<IActionResult> GetAllPermissionByRole(int roleId)
        {
            var allPermission = await _permissionRepository.GetAllPermissionByRole(roleId);
            if (allPermission == null)
            {
                return BadRequest(new ResponseModel()
                {
                    Status = false,
                    Message = "Something is wrong!",
                    Data = null
                });
            }
            return Ok(new ResponseModel()
            {
                Status = true,
                Message = "",
                Data = allPermission
            });
        }
        [Route("CreatePermissions")]
        [HttpPost]
        public async Task<IActionResult> CreatePermissions([FromBody] IEnumerable<AddModuleAccessRightsDTO> permissionDTO)
        {
            if (permissionDTO == null || !ModelState.IsValid)
                return BadRequest();

            var module = await _permissionRepository.CreatePermissions(permissionDTO);
            if (module == null)
                return BadRequest();

            return Ok(new ResponseModel()
            {
                Status = true,
                Message = "",
                Data = module
            });
        }
        [Route("UpdatePermissions")]
        [HttpPut]
        public async Task<IActionResult> UpdatePermissions([FromBody] IEnumerable<AddModuleAccessRightsDTO> permissionDTO)
        {
            if (permissionDTO == null || !ModelState.IsValid)
                return BadRequest();

            var module = await _permissionRepository.UpdatePermissions(permissionDTO);
            if (module == null)
                return BadRequest();

            return Ok(new ResponseModel()
            {
                Status = true,
                Message = "",
                Data = module
            });
        }
    }
}
