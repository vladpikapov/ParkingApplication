using ServerPart.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ServerPart.Data.Models.AuthModels
{
    [Table("ACCOUNTS")]
    public class Account
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        [Column("ROLE_ID")]
        public RoleEnum RoleId { get; set; }

        public int WalletId { get; set; }

        public Wallet Wallet { get; set; }
    }
}
