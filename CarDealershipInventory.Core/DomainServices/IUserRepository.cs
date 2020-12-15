using CarDealershipInventory.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipInventory.Core.DomainServices
{
    public interface IUserRepository
    {
        public User ReadUserFromLoginInput(LoginInputModel model);
    }
}
