using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Data
{
    public class RecipeModel
    {
        public string NameOfRecipe { get; set; }
        public List<IngredientModel> Ingredients { get; set; }
        public string ListOfDirections {get;set;}
        public string EditHistory {get;set;}



        public RecipeModel(string nameOfRecipe, List<IngredientModel> ingredients, string listOfDirections, string editHistory)
        {
            NameOfRecipe = nameOfRecipe;
            Ingredients = ingredients;
            ListOfDirections = listOfDirections;
            EditHistory = editHistory;
        }

        public RecipeModel() { }

        public void AddIngredientAndAmountToRecipe(string Name, decimal Amount) { }

        public AmountModel GetIngredientAmountFromRecipe(string Name)
        {
            IngredientModel ingredientFromRecipe = Ingredients.FirstOrDefault(m => m.Name == Name);
            return ingredientFromRecipe.Amount;
        }


    }
}
