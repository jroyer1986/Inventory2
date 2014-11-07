using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Data
{
    public class Inventory
    {
        List<Ingredient> Ingredients
        { get; set; }

        public void Add(Ingredient ingredient)
        {
            Ingredients.Add(ingredient);
        }

        public bool Has(string ingredientName){
            return Ingredients.Any(m => m.Name == ingredientName);
        }

        public Inventory()
        {
            Ingredients = new List<Ingredient>();
        }
    }
}
