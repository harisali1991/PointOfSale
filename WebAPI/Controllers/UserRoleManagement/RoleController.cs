using Business.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.RoleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers.UserRoleManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;
        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        [Route("GetAllRoles")]
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var allRoles = await _roleRepository.GetAllRoles();
            //return Ok(allRoles);
            return Ok(new ResponseModel()
            {
                Status = true,
                Message = "",
                Data = allRoles
            });
        }
        [Route("GetRole")]
        [HttpGet]
        public async Task<IActionResult> GetRole(int? roleId)
        {
            if (roleId == null)
                return BadRequest(new ResponseModel()
                {
                    Status = false,
                    Message = "Invalid Role Id",
                    Data = null
                });

            var objRole = await _roleRepository.GetRole(roleId.Value);

            if (objRole == null)
                return NotFound(new ResponseModel()
                {
                    Status = false,
                    Message = "Role doest not exist!",
                    Data = null
                });
            return Ok(new ResponseModel()
            {
                Status = true,
                Message = "",
                Data = objRole
            });
        }
        [Route("CreateRole")]
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] AddRoleDTO roleDTO)
        {
            if (roleDTO == null || !ModelState.IsValid)
                return BadRequest(new ResponseModel()
                {
                    Status = false,
                    Message = "Invalid Role",
                    Data = null
                });

            var IsExist = await _roleRepository.IsAlreadyExist(roleDTO.Name);
            if (IsExist != null)
            {
                return BadRequest(new ResponseModel()
                {
                    Status = false,
                    Message = "Role name already exist!",
                    Data = null
                });
            }
            var role = await _roleRepository.CreateRole(roleDTO);
            if (role == null)
                return NotFound(new ResponseModel()
                {
                    Status = false,
                    Message = "Role does not exist!",
                    Data = null
                });

            return Ok(new ResponseModel()
            {
                Status = true,
                Message = "",
                Data = role
            });
        }
        [HttpPut("UpdateRole")]
        public async Task<IActionResult> UpdateRole([FromBody] AddRoleDTO roleDTO)
        {
            if (roleDTO == null || !ModelState.IsValid)
                return BadRequest(new ResponseModel()
                {
                    Status = false,
                    Message = "Invalid Role",
                    Data = null
                });

            var role = await _roleRepository.UpdateRole(roleDTO.Id,roleDTO);
            if (role == null)
                return BadRequest(new ResponseModel()
                {
                    Status = false,
                    Message = "Unable to update this role",
                    Data = null
                });

            return Ok(new ResponseModel()
            {
                Status = true,
                Message = "",
                Data = role
            });
        }
        [Route("DeleteRole")]
        [HttpDelete]
        public async Task<IActionResult> DeleteRole(int? roleId)
        {
            if (roleId == null)
                return BadRequest(new ResponseModel()
                {
                    Status = false,
                    Message = "Invalid Role Id",
                    Data = null
                });

            var role = await _roleRepository.Delete(roleId.Value);
            if (role <= 0)
                return BadRequest(new ResponseModel()
                {
                    Status = false,
                    Message = "Unable to delete this role",
                    Data = null
                });

            return Ok();
        }
    }
}
