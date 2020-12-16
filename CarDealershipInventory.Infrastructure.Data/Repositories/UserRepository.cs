using CarDealershipInventory.Core.DomainServices;
using CarDealershipInventory.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarDealershipInventory.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CarDealershipInventoryContext _ctx;

        public UserRepository(CarDealershipInventoryContext ctx)
        {
            _ctx = ctx;
        }

        public User ReadUserFromLoginInput(LoginInputModel model)
        {
            return _ctx.Users.FirstOrDefault(u => u.Username == model.Username);
        }
    }
}
