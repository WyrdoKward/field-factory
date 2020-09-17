using System;
using System.Collections.Generic;
using System.Text;

namespace FieldFactory.DataAccess.DTO
{
    internal class UserDTO
    {
        public int IdUser { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
