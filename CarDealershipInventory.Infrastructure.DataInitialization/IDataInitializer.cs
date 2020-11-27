using CarDealershipInventory.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipInventory.Infrastructure.DataInitialization
{
    public interface IDataInitializer
    {
        public void Initialize(CarDealershipInventoryContext ctx);
    }
}
