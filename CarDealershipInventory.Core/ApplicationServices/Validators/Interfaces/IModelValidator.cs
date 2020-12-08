using CarDealershipInventory.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealershipInventory.Core.ApplicationServices.Validators.Interfaces
{
    public interface IModelValidator
    {
        public void ValidateModel(Model model);
        
    }
}
