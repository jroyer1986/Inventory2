using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Data
{
    public class Inventory
    {
        List<IngredientModel> Ingredients
        { get; set; }

        public void Add(IngredientModel ingredient)
        {
            //check if exists
            //if it does, call combine
            if (!Ingredients.Any(m => m.Name == ingredient.Name))
            {
                Ingredients.Add(ingredient);
            }
            else CombineAmounts(ingredient);
        }

        public bool Has(string ingredientName){
            return Ingredients.Any(m => m.Name == ingredientName);
        }

        public IngredientModel GetByName(string name)
        {
            IngredientModel ingredient = Ingredients.FirstOrDefault(m => m.Name == name);
            return ingredient;
        }

        public void Use(string Name, decimal Amount)
        {
            IngredientModel ingredientToUse = Ingredients.FirstOrDefault(m => m.Name == Name);

            AmountModel currentAmount = ingredientToUse.Amount;
            decimal newAmountTotal = ingredientToUse.Amount.Total - Amount;

            ingredientToUse.Amount.Total = newAmountTotal;
  
            if(ingredientToUse.Amount.Total < (decimal)0)
            {                
                ingredientToUse.Amount = currentAmount;
                throw new InvalidOperationException();
            }

        }

        public void Remove(IngredientModel ingredient)
        {
            Ingredients.Remove(ingredient);
        }

        public void CombineAmounts(IngredientModel ingredientToCombine)
        {
            IngredientModel ingredientFromDB = Ingredients.FirstOrDefault(m => m.Name == ingredientToCombine.Name);
            AmountModel originalAmount = ingredientFromDB.Amount;
            decimal newTotalAmount = originalAmount.Total + ingredientToCombine.Amount.Total;

            ingredientFromDB.Amount.Total = newTotalAmount;            
        }

        public bool Any(string ingredientName)
        {
            return true;
        }

        public decimal GetAmount(string IngredientName)
        {
            IngredientModel ingredientFromDB = Ingredients.FirstOrDefault(m => m.Name == IngredientName);
            return ingredientFromDB.Amount.Total;
        }

        public Inventory()
        {
            Ingredients = new List<IngredientModel>();
        }
    }
}
