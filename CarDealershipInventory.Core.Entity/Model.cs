using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipInventory.Core.Entity
{
    public class Model
    {
        public int ModelId { get; set; }
        public string Name { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public int ManufacturerId { get; set; }
        public List<Car> Cars { get; set; }

    }
}
