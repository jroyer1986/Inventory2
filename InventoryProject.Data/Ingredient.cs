using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Data
{
    public class Ingredient
    {
        public string Name
        { get; set; }
        public decimal Amount
        { get; set; }

        public Ingredient(string name, decimal amount)
        {
            Name = name;
            Amount = amount;
        }

        public Ingredient(Ingredient ingredient)
        {
            Name = ingredient.Name;
            Amount = ingredient.Amount;
        }

        public Ingredient() { }
    }
}
