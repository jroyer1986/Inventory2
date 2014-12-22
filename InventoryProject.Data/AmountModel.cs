using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Data
{
    public class AmountModel
    {
        public decimal Total { get; set; }
        public UnitEnum Unit { get; set; }

        public AmountModel(decimal amount, UnitEnum unit)
        {
            Total = amount;
            Unit = unit;
        }
        public AmountModel(decimal amount, string unit)
        {
            UnitEnum unitEnum = UnitEnum.grams;
            Enum.TryParse<UnitEnum>(unit, out unitEnum);

            Unit = unitEnum;
            Total = amount;
        }
    }
}
