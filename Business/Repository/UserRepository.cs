using AutoMapper;
using Business.Repository.IRepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models.Helpers;
using Models.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public UserRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<PagedList<RegisterDTO>> GetAllUsers(UserListDTO model)
        {
            try
            {
                var usersList = _db.tblUsers.AsQueryable();
                if (!string.IsNullOrEmpty(model.Username))
                {
                    usersList = usersList.Where(x => x.Username == model.Username).AsQueryable();
                }
                if (!string.IsNullOrEmpty(model.Email))
                {
                    usersList = usersList.Where(x => x.Email == model.Email).AsQueryable();
                }
                if (!string.IsNullOrEmpty(model.ContactNumber))
                {
                    usersList = usersList.Where(x => x.ContactNumber == model.ContactNumber).AsQueryable();
                }
                return PagedList<RegisterDTO>.ToPagedList(_mapper.Map<IEnumerable<tblUser>, IEnumerable<RegisterDTO>>(usersList), model.PageNo, model.PageSize);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<RegisterDTO> GetUserById(int userId)
        {
            try
            {
                RegisterDTO registerDTO =
                    _mapper.Map<tblUser, RegisterDTO>(await _db.tblUsers.FirstOrDefaultAsync(u => u.Id == userId));
                return registerDTO;
            }
            catch
            {
                return null;
            }
        }
        public async Task<UserDTO> GetUserInformation(int userId)
        {
            try
            {
                UserDTO userDTO =
                    _mapper.Map<tblUser, UserDTO>(await _db.tblUsers.FirstOrDefaultAsync(u => u.Id == userId));
                return userDTO;
            }
            catch
            {
                return null;
            }
        }
        public async Task<RegisterDTO> GetUserByUsername(string username)
        {
            try
            {
                RegisterDTO registerDTO =
                    _mapper.Map<tblUser, RegisterDTO>(await _db.tblUsers.FirstOrDefaultAsync(u => u.Username == username));
                return registerDTO;
            }
            catch
            {
                return null;
            }
        }
        public async Task<RegisterDTO> GetUserByEmail(string email)
        {
            try
            {
                RegisterDTO registerDTO =
                    _mapper.Map<tblUser, RegisterDTO>(await _db.tblUsers.FirstOrDefaultAsync(u => u.Email == email));
                return registerDTO;
            }
            catch
            {
                return null;
            }
        }
        public async Task<RegisterDTO> CreateUser(tblUser user)
        {
            try
            {
                user.CreatedAt = DateTime.Now;
                var addedUser = await _db.AddAsync(user);
                await _db.SaveChangesAsync();
                return _mapper.Map<tblUser, RegisterDTO>(addedUser.Entity);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<int> Delete(int userId)
        {
            try
            {
                var user = await _db.tblUsers.FindAsync(userId);
                if (user != null)
                {
                    _db.tblUsers.Remove(user);
                    return await _db.SaveChangesAsync();
                }
                return 0;
            }
            catch
            {
                return -1;
            }
        }


        public async Task<bool> IsAlreadyExist(string parameter)
        {
            try
            {
                var user = await _db.tblUsers.Where(u => u.Username == parameter
                                        || u.Email == parameter
                                        || u.ContactNumber == parameter).FirstOrDefaultAsync();
                if (user == null)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<UpdateUserDTO> UpdateUser(tblUser user)
        {
            try
            {
                var objUser = await _db.tblUsers.FindAsync(user.Id);
                var addedUser = _db.tblUsers.Attach(objUser);
                objUser.Firstname = string.IsNullOrEmpty(user.Firstname) ? objUser.Firstname : user.Firstname;
                objUser.Lastname = string.IsNullOrEmpty(user.Lastname) ? objUser.Lastname : user.Lastname;
                objUser.UserLevelId = user.UserLevelId == 0 ? objUser.UserLevelId : user.UserLevelId;
                objUser.UserLevel = user.UserLevel == 0 ? objUser.UserLevel : user.UserLevel;
                objUser.RoleId = user.RoleId == 0 ? objUser.RoleId : user.RoleId;
                objUser.IsEnabled = user.IsEnabled == false ? objUser.IsEnabled : user.IsEnabled;
                objUser.ModifiedAt = DateTime.Now;
                //_db.Entry(user).State = EntityState.Unchanged;
                await _db.SaveChangesAsync();
                return _mapper.Map<tblUser, UpdateUserDTO>(addedUser.Entity);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<tblRefreshToken> RefreshToken(string refreshToken)
        {
           return await _db.tblRefreshTokens.Where(rt => rt.Token == refreshToken)
                                                    .OrderByDescending(rt => rt.ExpiryDate)
                                                    .FirstOrDefaultAsync();
        }

        public async Task<bool> AddRefreshToken(tblRefreshToken refreshToken)
        {
            try
            {
                var addedUser = await _db.tblRefreshTokens.AddAsync(refreshToken);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        } 

        public async Task<tblUser> GetUser(string username)
        {
            return await _db.tblUsers.Where(x => x.Username == username).FirstOrDefaultAsync();
        }
    }
}
