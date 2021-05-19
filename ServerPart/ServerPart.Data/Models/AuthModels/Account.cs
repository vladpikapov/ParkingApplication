using ServerPart.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ServerPart.Data.Models.AuthModels
{
    public class Account
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string CarNumber { get; set; }

        public string Email { get; set; }

        public int ConfirmEmail { get; set; }

        public RoleEnum RoleId { get; set; }

        public int WalletId { get; set; }

        public Wallet Wallet { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime LastLogin { get; set; }
    }
}
