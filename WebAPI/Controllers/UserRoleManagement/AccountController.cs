using Business.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.UserDTOs;
using DataAccess.Data;
using AutoMapper;
using WebAPI.Extension;
using WebAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace WebAPI.Controllers.UserRoleManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly JWTSettings _jwtsettings;
        public AccountController(IUserRepository userRepository, IMapper mapper, IOptions<JWTSettings> jwtsettings)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtsettings = jwtsettings.Value;
        }

        [Route("GetAllUser")]
        [HttpPost]
        public async Task<IActionResult> GetAllUser([FromBody] UserListDTO model)
        {
            var users = await _userRepository.GetAllUsers(model);
            return Ok(new PaginationModel()
            {
                TotalCount = users.TotalCount,
                PageSize = users.PageSize,
                CurrentPage = users.CurrentPage,
                HasNext = users.HasNext,
                HasPrevious = users.HasPrevious,
                TotalPages = users.TotalPages,
                Status = true,
                Message = string.Empty,
                Data = users
            });
        }
        [Route("GetUser")]
        [HttpGet]
        public async Task<IActionResult> GetUser(int userId)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null)
            {
                return NotFound(new ResponseModel()
                {
                    Status = false,
                    Message = "User not found",
                    Data = null
                });
            }
            return Ok(new ResponseModel()
            {
                Status = true,
                Message = string.Empty,
                Data = user
            });
        }
        // GET: api/Users
        [HttpPost("RefreshToken")]
        public async Task<ActionResult<UserWithToken>> RefreshToken([FromBody] RefreshRequest refreshRequest)
        {
            UserDTO user = await GetUserFromAccessToken(refreshRequest.AccessToken);

            if (user != null && await ValidateRefreshToken(user, refreshRequest.RefreshToken))
            {
                UserWithToken userWithToken = new UserWithToken(user);
                userWithToken.AccessToken = GenerateAccessToken(user.Id);

                return userWithToken;
            }

            return null;
        }
        [Route("CreateUser")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ResponseModel()
                    {
                        Status = false,
                        Message = string.Join(" | ", ModelState.Values
                                    .SelectMany(v => v.Errors)
                                    .Select(e => e.ErrorMessage)),
                        Data = null
                    });
                }
                if (await _userRepository.IsAlreadyExist(model.Username)
                            || await _userRepository.IsAlreadyExist(model.Email)
                            || await _userRepository.IsAlreadyExist(model.ContactNumber))
                {
                    return BadRequest(new ResponseModel()
                    {
                        Status = false,
                        Message = "User already exist",
                        Data = null
                    });
                }

                tblUser user = _mapper.Map<RegisterDTO, tblUser>(model);

                String[] Password = SharedClass.GeneratePassword(10, model.Username, model.Password);
                user.Password = Password[0];
                user.PasswordSat = Password[1];

                var userDTO = await _userRepository.CreateUser(user);
                if (userDTO == null)
                {
                    return BadRequest(new ResponseModel()
                    {
                        Status = false,
                        Message = "User not saved",
                        Data = null
                    });
                }
                //////////////////////////////
                //load role for registered user
                user = _mapper.Map<RegisterDTO, tblUser>(await _userRepository.GetUserById(user.Id));

                UserWithToken userWithToken = null;

                if (user != null)
                {
                    tblRefreshToken refreshToken = GenerateRefreshToken();
                    //user.tblRefreshTokens.Add(refreshToken);
                    //await _context.SaveChangesAsync();
                    refreshToken.UserId = user.Id;
                    await _userRepository.AddRefreshToken(refreshToken);
                    var userDTOs = _mapper.Map<tblUser, UserDTO>(user);
                    userWithToken = new UserWithToken(userDTOs);
                    userWithToken.RefreshToken = refreshToken.Token;
                }

                if (userWithToken == null)
                {
                    return NotFound();
                }

                //sign your token here here..
                userWithToken.AccessToken = GenerateAccessToken(user.Id);

                /////////////////////////////
                return Ok(new ResponseModel()
                {
                    Status = true,
                    Message = "",
                    Data = userWithToken
                });
            }
            catch
            {
                return BadRequest(new ResponseModel()
                {
                    Status = true,
                    Message = "Something is wrong, Please try again.",
                    Data = null
                });
            }
        }
        [Route("UpdateUser")]
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ResponseModel()
                    {
                        Status = false,
                        Message = string.Join(" | ", ModelState.Values
                                    .SelectMany(v => v.Errors)
                                    .Select(e => e.ErrorMessage)),
                        Data = null
                    });
                }
                tblUser user = _mapper.Map<UpdateUserDTO, tblUser>(model);
                var userDTO = await _userRepository.UpdateUser(user);
                if (userDTO == null)
                {
                    return BadRequest(new ResponseModel()
                    {
                        Status = false,
                        Message = "User not saved",
                        Data = null
                    });
                }
                return Ok(new ResponseModel()
                {
                    Status = true,
                    Message = "",
                    Data = userDTO
                });
            }
            catch
            {
                return BadRequest(new ResponseModel()
                {
                    Status = true,
                    Message = "Something is wrong, Please try again.",
                    Data = null
                });
            }
        }
        // POST: api/Users
        [HttpPost, Route("login")]
        public async Task<ActionResult<UserWithToken>> Login([FromBody] LoginViewModel model)
        {
            var user = await _userRepository.GetUser(model.Username);

            if (user == null)
            {
                return NotFound(new ResponseModel()
                {
                    Status = false,
                    Message = "Invalid username and password",
                    Data = null
                });
            }
            String strPassword = String.Empty;

            try
            {
                strPassword = StringCipher.Decrypt(user.Password, StringCipher.Decrypt(user.PasswordSat, user.Username + StringCipher.PassCode));
            }
            catch
            {
                return NotFound(new ResponseModel()
                {
                    Status = false,
                    Message = "Invalid username and password",
                    Data = null
                });
            }
            if (strPassword.Equals(model.Password, StringComparison.Ordinal))
            {
                UserWithToken userWithToken = null;

                if (user != null)
                {
                    tblRefreshToken refreshToken = GenerateRefreshToken();
                    //user.RefreshTokens.Add(refreshToken);
                    //await _context.SaveChangesAsync();
                    refreshToken.UserId = user.Id;
                    await _userRepository.AddRefreshToken(refreshToken);
                    var userDTO = _mapper.Map<tblUser, UserDTO>(user);
                    userWithToken = new UserWithToken(userDTO);
                    userWithToken.RefreshToken = refreshToken.Token;
                }

                if (userWithToken == null)
                {
                    return NotFound(new ResponseModel()
                    {
                        Status = false,
                        Message = "Invalid username and password",
                        Data = null
                    });
                }

                //sign your token here here..
                userWithToken.AccessToken = GenerateAccessToken(user.Id);
                
                return Ok(new ResponseModel()
                {
                    Status = true,
                    Message = "",
                    Data = userWithToken
                });
            }
            else
            {
                return NotFound(new ResponseModel()
                {
                    Status = false,
                    Message = "Invalid username and password",
                    Data = null
                });
            }
        }
        #region helper-method
        private async Task<UserDTO> GetUserFromAccessToken(string accessToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtsettings.SecretKey);

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                SecurityToken securityToken;
                var principle = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out securityToken);

                JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;

                if (jwtSecurityToken != null && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    var userId = principle.FindFirst(ClaimTypes.Name)?.Value;

                    //return await _context.Users.Where(u => u.UserId == Convert.ToInt32(userId)).FirstOrDefaultAsync();
                    return await _userRepository.GetUserInformation(Convert.ToInt32(userId));
                }
            }
            catch (Exception)
            {
                return new UserDTO();
            }

            return new UserDTO();
        }
        private async Task<bool> ValidateRefreshToken(UserDTO user, string refreshToken)
        {

            //tblRefreshToken refreshTokenUser = _context.RefreshTokens.Where(rt => rt.Token == refreshToken)
            //                                    .OrderByDescending(rt => rt.ExpiryDate)
            //                                    .FirstOrDefault();
            tblRefreshToken refreshTokenUser = await _userRepository.RefreshToken(refreshToken);
            if (refreshTokenUser != null && refreshTokenUser.UserId == user.Id
                && refreshTokenUser.ExpiryDate > DateTime.UtcNow)
            {
                return true;
            }

            return false;
        }
        private tblRefreshToken GenerateRefreshToken()
        {
            tblRefreshToken refreshToken = new tblRefreshToken();

            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                refreshToken.Token = Convert.ToBase64String(randomNumber);
            }
            refreshToken.ExpiryDate = DateTime.UtcNow.AddMonths(6);

            return refreshToken;
        }
        private string GenerateAccessToken(int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtsettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, Convert.ToString(userId))
                }),
                Expires = DateTime.UtcNow.AddMinutes(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        #endregion
    }
}
