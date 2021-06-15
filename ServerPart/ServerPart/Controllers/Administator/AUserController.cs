using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerPart.Data.Models.AuthModels;
using ServerPart.Logic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerPart.Controllers.Administator
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class AUserController: ControllerBase
    {
        private readonly AccountManager m_AccountManager;

        public AUserController(AccountManager accountManager)
        {
            m_AccountManager = accountManager;
        }

        [HttpGet("[action]")]
        public IEnumerable<Account> GetAccounts()
        {
            return m_AccountManager.GetUsersAccounts();
        }

        [HttpGet("[action]/{userId}")]
        public Account GetAccount([FromRoute] int userId)
        {
            return m_AccountManager.GetUser(userId);
        }

        [HttpGet("[action]")]
        public void DeleteUser([FromRoute] int userId)
        {
            m_AccountManager.DeleteUser(userId);
        }
    }
}
