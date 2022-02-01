using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.UserDTOs
{
    public class UserListDTO
    {
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

    }
}
