using ServerPart.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerPart.Data.Models.AuthModels
{
    public class Account
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public RoleEnum Role { get; set; }
    }
}
