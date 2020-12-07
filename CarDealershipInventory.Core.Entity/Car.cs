using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipInventory.Core.Entity
{
    public class Car
    {
        public int CarId { get; set; }
        public Model Model { get; set; }
        public int ModelId { get; set; }
        public int Key { get; set; }
        public string Location { get; set; }
        public DateTime LastInspection { get; set; }
        public int Kilometers { get; set; }
        public int ProductionYear { get; set; }
        public string LicensePlate { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public double PurchasePrice { get; set; }
        public double CurrentPrice { get; set; }
        public DateTime DateOfSale { get; set; }
        public double SoldPrice { get; set; }
        public double VAT { get; set; }
        public bool IsSold { get; set; }
        public int DaysInInventory { get; set; }
        public int CarlId { get; set; }
    }
}
