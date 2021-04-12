using ServerPart.Data.Context;
using ServerPart.Data.Models.AuthModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerPart.Logic.Managers
{
    public class AccountManager
    {
        private readonly UserContext m_UserContext;
        private readonly WalletContext m_WalletContext;

        public AccountManager(UserContext userContext, WalletContext walletContext)
        {
            m_UserContext = userContext;
            m_WalletContext = walletContext;
        }

        public IEnumerable<Account> GetUsersAccounts()
        {
            return m_UserContext.GetAll();
        }

        public Account GetUser(int id)
        {
            var accountInfo = m_UserContext.Get(id);
            accountInfo.Wallet = m_WalletContext.Get(accountInfo.WalletId);
            return accountInfo;
        }
    }
}
