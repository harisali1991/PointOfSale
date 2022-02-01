using DataAccess.Data;
using Models.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class UserWithToken : UserDTO
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public UserWithToken(UserDTO user)
        {
            this.Id = user.Id;
            this.Username = user.Username;
            this.Firstname = user.Firstname;
            this.Lastname = user.Lastname;
            this.Email = user.Email;
            this.ContactNumber = user.ContactNumber;
            this.IsEnabled = user.IsEnabled;
            this.CreatedAt = user.CreatedAt;
        }
    }
}
