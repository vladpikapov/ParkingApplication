using ServerPart.Data.Interfaces;
using ServerPart.Data.Models.AuthModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerPart.Data.Context
{
    public class WalletContext : IMainContext<Wallet>
    {



        public void Delete(int itemId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Wallet Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Wallet> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(Wallet item)
        {
            throw new NotImplementedException();
        }

        public void Update(Wallet item)
        {
            throw new NotImplementedException();
        }
    }
}
