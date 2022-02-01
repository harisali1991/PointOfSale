using DataAccess.Data;
using Models.Helpers;
using Models.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IUserRepository
    {
        public Task<PagedList<RegisterDTO>> GetAllUsers(UserListDTO model);
        //public Task<IEnumerable<RegisterDTO>> GetAllUsers(int pageNo,int pageSize);
        public Task<RegisterDTO> GetUserById(int userId);
        public Task<UserDTO> GetUserInformation(int userId);
        public Task<tblUser> GetUser(string username);
        public Task<RegisterDTO> GetUserByUsername(string username);
        public Task<RegisterDTO> GetUserByEmail(string email);
        public Task<RegisterDTO> CreateUser(tblUser user);
        public Task<UpdateUserDTO> UpdateUser(tblUser user);
        public Task<bool> IsAlreadyExist(string parameter);
        public Task<int> Delete(int userId);
        public Task<tblRefreshToken> RefreshToken(string refreshToken);
        public Task<bool> AddRefreshToken(tblRefreshToken refreshToken);

    }
}
