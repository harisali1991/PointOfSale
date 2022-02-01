using AutoMapper;
using DataAccess.Data;
using Models;
using Models.ModuleDTOs;
using Models.RoleDTOs;
using Models.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddRoleDTO, tblRole>().ReverseMap();
            CreateMap<RoleDTO, tblRole>().ReverseMap();
            CreateMap<ModuleDTO, tblModule>().ReverseMap();
            CreateMap<ModuleAccessRightsDTO, tblModuleAccessRight>().ReverseMap();
            CreateMap<AddModuleAccessRightsDTO, tblModuleAccessRight>().ReverseMap();
            CreateMap<RegisterDTO, tblUser>().ReverseMap();
            CreateMap<UpdateUserDTO, tblUser>().ReverseMap();
            CreateMap<UserDTO, tblUser>().ReverseMap();

        }
    }
}
