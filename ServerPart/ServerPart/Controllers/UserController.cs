using Microsoft.AspNetCore.Mvc;
using ServerPart.Data.Models.AuthModels;
using ServerPart.Logic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerPart.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private AccountManager AccountManager;
        private MailManager MailManager;

        public UserController(AccountManager accountManager, MailManager mailManager)
        {
            AccountManager = accountManager;
            MailManager = mailManager;
        }

        [HttpGet("[action]/{userId}")]
        public Wallet GetUserWallet([FromRoute] int userId) => AccountManager.GetUserWallet(userId);

        [HttpGet("[action]/{userId}")]
        public Account GetAccount([FromRoute] int userId) => AccountManager.GetUser(userId);

        [HttpPut("[action]")]
        public void UpdateUserWallet([FromBody] Wallet wallet)
        {
            AccountManager.UpdateUserWallet(wallet);
        }

        [HttpGet("[action]/{code}/{login}")]
        public bool SendUserCode([FromRoute] int code, [FromRoute] string login)
        {
            return AccountManager.CheckUserCode(code, login);
        }

        [HttpPut("[action]/{updateType}")]
        public bool UpdateUserData(Account account, [FromRoute] int updateType)
        {
           return AccountManager.UpdateUserData(account, updateType);
        }

        [HttpPut("[action]/{code}")]
        public bool UpdateUserMail(Account account, [FromRoute] int code)
        {
            return AccountManager.UpdateUserData(account, 3, code);
        }

        [HttpGet("[action]/{mail}")]
        public void SendCodeToMail([FromRoute]string mail)
        {
            MailManager.SendCodeToMail(mail);
        }
    }
}
