using ServerPart.Data.Context;
using ServerPart.Data.Helper;
using ServerPart.Data.Models.AuthModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerPart.Logic.Managers
{
    public class AccountManager
    {
        private readonly UserContext m_UserContext;
        private readonly WalletContext m_WalletContext;
        private readonly MailManager m_MailManager;
        private readonly OrderContext m_OrderContext;
        private readonly ParkingRaitingContext m_prContext;

        public AccountManager(UserContext userContext, WalletContext walletContext, MailManager mailManager, OrderContext orderContext, ParkingRaitingContext psContext)
        {
            m_UserContext = userContext;
            m_WalletContext = walletContext;
            m_MailManager = mailManager;
            m_OrderContext = orderContext;
            m_prContext = psContext;
        }

        public IEnumerable<Account> GetUsersAccounts()
        {
            return m_UserContext.GetAll();
        }

        public Account GetUser(int id)
        {
            var accountInfo = m_UserContext.Get(id);
            if (accountInfo != null)
            {
                accountInfo.Wallet = m_WalletContext.Get(accountInfo.WalletId) ?? new Wallet();
                return accountInfo;
            }
            return new Account();
        }

        public Wallet GetUserWallet(int userId)
        {
            var accountInfo = m_UserContext.Get(userId);
            if (accountInfo != null)
                return m_WalletContext.Get(accountInfo.WalletId);
            return new Wallet();
        }

        public void UpdateUserWallet(Wallet wallet)
        {
            m_WalletContext.Update(wallet);
        }

        public bool CheckUserCode(int code, string login)
        {
            if (m_MailManager.CheckVerifyCode(code))
            {
                m_UserContext.ConfirmEmail(login);
                return true;
            }
            return false;
        }

        public bool UpdateUserData(Account account, int updateType, int code = 0)
        {
            var allUsers = m_UserContext.GetAll();
            var actualUser = m_UserContext.Get(account.Id);
            var data = string.Empty;
            var updateColumn = string.Empty;
            switch (updateType)
            {
                case 1:
                    if (allUsers.Find(x => x.Login.Equals(account.Login)) == null)
                    {
                        data = account.Login;
                        updateColumn = "[LOGIN]";
                    }
                    else
                        return false;
                    break;
                case 2:
                    data = account.Password;
                    updateColumn = "[PASSWORD]";
                    break;
                case 3:
                    if (allUsers.Find(x => x.Email.Equals(account.Email)) == null && code > 0 && m_MailManager.CheckVerifyCode(code))
                    {
                        data = account.Email;
                        updateColumn = "[EMAIL]";
                    }
                    else
                        return false;
                    break;
                case 4:
                    if (allUsers.Find(x => x.CarNumber.Equals(account.CarNumber)) == null)
                    {
                        data = account.CarNumber;
                        updateColumn = "[CarNumber]";
                    }
                    else
                        return false;
                    break;
                case 5:
                    data = DateTime.Now.ToString();
                    updateColumn = "[LastLogin]";
                    break;
            }
            m_UserContext.UpdateColumn(data, updateColumn, actualUser.Id);
            return true;
        }

        public void DeleteUser(int userId)
        {
            var userOrders = m_OrderContext.GetAll().Where(x => x.OrderUserId == userId).ToList();
            userOrders.ForEach(x => m_OrderContext.Delete(x.OrderId));
            m_prContext.DeleteByUser(userId);
            m_UserContext.Delete(userId);
        }
    }
}
