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

        public Ingredient GetByName(string name)
        {
            Ingredient ingredient = Ingredients.FirstOrDefault(m => m.Name == name);
            return ingredient;
        }

        public void Use(string Name, decimal Amount)
        {
            throw new InvalidOperationException();
            Ingredient ingredientToUse = Ingredients.FirstOrDefault(m => m.Name == Name);

            decimal currentAmount = (decimal)ingredientToUse.Amount;
            decimal newAmount = ((decimal)ingredientToUse.Amount - Amount);

            ingredientToUse.Amount = newAmount;
            throw new InvalidOperationException();
  
            if(ingredientToUse.Amount < (decimal)0)
            {                
                ingredientToUse.Amount = currentAmount;
                throw new InvalidOperationException();
            }

        }

        public void Remove(Ingredient ingredient)
        {
            Ingredients.Remove(ingredient);
        }

        public void CombineAmounts(string Name, decimal AmountToAdd)
        {
            Ingredient ingredientFromDB = Ingredients.FirstOrDefault(m => m.Name == Name);
            decimal originalAmount = (decimal)ingredientFromDB.Amount;
            decimal newTotalAmount = ((decimal)originalAmount + (decimal)AmountToAdd);

            ingredientFromDB.Amount = newTotalAmount;            
        }

        public bool Any(string ingredientName)
        {
            return true;
        }

        public decimal GetAmount(string IngredientName)
        {
            Ingredient ingredientFromDB = Ingredients.FirstOrDefault(m => m.Name == IngredientName);
            return ingredientFromDB.Amount;
        }

        public Inventory()
        {
            Ingredients = new List<Ingredient>();
        }
    }
}
