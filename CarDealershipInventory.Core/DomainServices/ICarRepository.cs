using CarDealershipInventory.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipInventory.Core.DomainServices
{

    public interface ICarRepository
    {
        public List<Car> ReadAllCars();

        public Car ReadCarById(int id);
        
    }

    
}
