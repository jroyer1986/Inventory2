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

        public AmountModel(decimal amount)
        {
            Total = amount;
        }
    }
}
