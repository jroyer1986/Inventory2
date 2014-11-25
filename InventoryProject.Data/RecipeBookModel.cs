using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Data
{
    public class RecipeBookModel
    {
        List<RecipeModel> Recipes { get; set; }

        public RecipeBookModel GetRecipeBook()
        {
            List<IngredientModel> listOfCookieIngredients = new List<IngredientModel>(){
            new IngredientModel("flour", 400),
            new IngredientModel("salt", 5),
            new IngredientModel("baking soda", 75)
            };
            List<IngredientModel> listOfBreadIngredients = new List<IngredientModel>(){
                new IngredientModel("flour", 600),
                new IngredientModel("sugar", 300),
                new IngredientModel("corn starch", 100)                
            };

            RecipeBookModel myRecipes = new RecipeBookModel();
            myRecipes.Add(new RecipeModel("bread recipe", listOfBreadIngredients, "mix ingredients. Bake at 400", "created recipe"));
            myRecipes.Add(new RecipeModel("cookie recipe", listOfCookieIngredients, "mix ingredients together. Bake at 350", "created recipe"));

            return myRecipes;
        }

        public RecipeBookModel()
        {
            Recipes = new List<RecipeModel>();
        }

        public void Add(RecipeModel recipe)
        {
            Recipes.Add(recipe);
        }

        public bool Has(string name)
        {
            return Recipes.Any(m => m.NameOfRecipe == name);
        }

        public RecipeModel GetRecipe(string name)
        {            
            RecipeModel myRecipe =Recipes.FirstOrDefault(m => m.NameOfRecipe == name);

            return myRecipe;
        }

        public void UseRecipe(Inventory myInventory, string name)
        {
            RecipeModel recipeToUse = Recipes.FirstOrDefault(m => m.NameOfRecipe == name);

            foreach(IngredientModel ingredientFromRecipe in recipeToUse.Ingredients)
            {
                myInventory.Use(ingredientFromRecipe.Name, recipeToUse.GetIngredientAmountFromRecipe(ingredientFromRecipe.Name).Total);
            }
        }

        public RecipeModel MultipleRecipeAmounts(string recipeName, decimal scaleFactor)
        {
            RecipeModel recipeToScale = Recipes.FirstOrDefault(m => m.NameOfRecipe == recipeName);
            if (recipeToScale != null)
            {
                List<IngredientModel> newIngredientAmounts = new List<IngredientModel>();

                foreach (IngredientModel ingredientToScale in recipeToScale.Ingredients)
                {
                    IngredientModel newlyScaledIngredient = new IngredientModel(ingredientToScale.Name, (ingredientToScale.Amount.Total * scaleFactor));
                    newIngredientAmounts.Add(newlyScaledIngredient);
                }
                RecipeModel scaledRecipe = new RecipeModel(recipeToScale.NameOfRecipe, newIngredientAmounts, recipeToScale.ListOfDirections, recipeToScale.EditHistory);
                return scaledRecipe;
            }

            else return null;
        }

        public void Edit(string recipeName, RecipeModel editsForRecipe, string commentsToAdd)
        {
            RecipeModel recipeToEdit = Recipes.FirstOrDefault(m => m.NameOfRecipe == recipeName);
            recipeToEdit.NameOfRecipe = editsForRecipe.NameOfRecipe;
            recipeToEdit.Ingredients = editsForRecipe.Ingredients;
            recipeToEdit.ListOfDirections = editsForRecipe.ListOfDirections;
            recipeToEdit.EditHistory = editsForRecipe.EditHistory + commentsToAdd;
        }

    }
}
