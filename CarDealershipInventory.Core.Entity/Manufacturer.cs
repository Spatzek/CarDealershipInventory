using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipInventory.Core.Entity
{
    public class Manufacturer
    {
        public int ManufacturerId { get; set; }
        public string Name { get; set; }
        public List<Model> Models { get; set; }
    }
}
