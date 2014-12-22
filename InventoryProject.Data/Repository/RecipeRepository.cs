using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Data.Repository
{
    public class RecipeRepository
    {
        InventoryDatabaseEntities _inventoryDatabaseEntities = new InventoryDatabaseEntities();
        IngredientRepository _ingredientRepository = new IngredientRepository();

        public void Create(RecipeModel recipeToAdd)
        {
            Recipe newRecipe = new Recipe()
            {
                id = recipeToAdd.ID,
                name = recipeToAdd.NameOfRecipe,
                listOfDirections = recipeToAdd.ListOfDirections,
                editHistory = recipeToAdd.EditHistory
            };
        }

        //public void Edit(RecipeModel EditedRecipe) 
        //{
        //    //get the ingredient from the database
        //    Recipe recipeToEdit = _inventoryDatabaseEntities.Recipe
        //                                                    .Include("RecipeIngredientList")
        //                                                    .Include("RecipeIngredientList_Ingredient")
        //                                                    .Include("RecipeIngredientList_IngredientAmount")
        //                                                    .FirstOrDefault(m => m.id == EditedRecipe.ID);
        //    if (recipeToEdit != null)
        //    {
        //        RecipeIngredientList parsedIngredients = new RecipeIngredientList();

        //        //convert list of IngredientModels to a RecipeIngredientList
        //        foreach(var oneListItem in recipeToEdit.RecipeIngredientList)
        //        {
                    
        //        }

        //        recipeToEdit.name = EditedRecipe.NameOfRecipe;
        //        recipeToEdit.RecipeIngredientList = EditedRecipe.Ingredients;
        //        recipeToEdit.listOfDirections = EditedRecipe.ListOfDirections;
        //        recipeToEdit.editHistory = EditedRecipe.EditHistory;

        //        _inventoryDatabaseEntities.SaveChanges();
        //    }
        //}

        public void Use(int id) 
        {
            Recipe recipeToUse = _inventoryDatabaseEntities.Recipe.
                                        Include("RecipeIngredientList")
                                        .Include("RecipeIngredientList_Ingredient")
                                        .Include("RecipeIngredientList_IngredientAmount")
                                        .Include("RecipeIngredientList_Ingredient_IngredientAmount")
                                        .FirstOrDefault(m => m.id == id);
            foreach (RecipeIngredientList recipeIngredient in recipeToUse.RecipeIngredientList)
            {
                Ingredient ingredientToUse = recipeIngredient.Ingredient;
                IngredientAmount ingredientAmountFromRecipe = recipeIngredient.IngredientAmount;
                
                var currentAmount = ingredientToUse.IngredientAmount.amount;
                var newAmount = currentAmount - ingredientAmountFromRecipe.amount;

                if (newAmount < 0)
                {
                    throw new System.ArgumentException("Not enough in current inventory!", "original");
                }
            }
            
            foreach (var recipeIngredient in recipeToUse.RecipeIngredientList)
            {
                Ingredient ingredientToUse = recipeIngredient.Ingredient;
                IngredientAmount ingredientAmountFromRecipe = recipeIngredient.IngredientAmount;

                _ingredientRepository.UseIngredient(ingredientToUse.id, ingredientAmountFromRecipe.amount);
            }
        }

        public void ScaleAmounts(int id, decimal scaleFactor) 
        {
            //get recipe from db
            Recipe recipeToScale = _inventoryDatabaseEntities.Recipe.
                                        Include("RecipeIngredientList")
                                        .Include("RecipeIngredientList_Ingredient")
                                        .Include("RecipeIngredientList_IngredientAmount")
                                        .FirstOrDefault(m => m.id == id);

            foreach(RecipeIngredientList recipeIngredient in recipeToScale.RecipeIngredientList)
            {
                IngredientAmount amountToScale = recipeIngredient.IngredientAmount;
                amountToScale.amount *= scaleFactor;
            }
            
        }

        public void Delete(int id) 
        {
            Recipe recipeToDelete = _inventoryDatabaseEntities.Recipe.FirstOrDefault(m => m.id == id);

            if (recipeToDelete != null)
            {
                _inventoryDatabaseEntities.Recipe.Remove(recipeToDelete);
                _inventoryDatabaseEntities.SaveChanges();
            }
        }

        public RecipeModel GetRecipeByID(int id)
        {
            Recipe recipe = _inventoryDatabaseEntities.Recipe.Include("RecipeIngredientList")
                                                    .Include("RecipeIngredientList_Ingredient")
                                                    .Include("RecipeIngredientList_IngredientAmount")
                                                    .FirstOrDefault(m => m.id == id);
            
            //convert recipe.listofrecipeingredient to list of ingredientmodels for the controller
            var listOfParsedIngredientsFromDatabase = new List<IngredientModel>();
            foreach (RecipeIngredientList ingredient in recipe.RecipeIngredientList)
            {
                //convert ingredientAmount to AmountModel
                IngredientAmount ingredientAmount = ingredient.IngredientAmount;
                AmountModel parsedAmount = new AmountModel(ingredientAmount.amount, ingredientAmount.units);

                IngredientModel ingredientModel = new IngredientModel(ingredient.Ingredient.id, ingredient.Ingredient.name, parsedAmount, ingredient.Ingredient.expirationDate, ingredient.Ingredient.placeOfPurchase, ingredient.Ingredient.notes);
                listOfParsedIngredientsFromDatabase.Add(ingredientModel);
            }
            
            //convert recipe to recipemodel
            RecipeModel recipeModel = new RecipeModel(recipe.name, listOfParsedIngredientsFromDatabase, recipe.listOfDirections, recipe.editHistory);
            return recipeModel;
        }

        public List<RecipeModel> GetListOfRecipes() 
        {
            List<RecipeModel> listOfRecipes = new List<RecipeModel>();

            var listOfRecipesDB = _inventoryDatabaseEntities.Recipe
                .Include("RecipeIngredientList")
                .Include("RecipeIngredientList.Ingredient")
                .Include("RecipeIngredientList.IngredientAmount")
                .ToList();

            foreach(Recipe singleDBItem in listOfRecipesDB)
            {
                var listOfIngredientsForRecipe = new List<IngredientModel>();

                //convert recipeIngredientList to list of IngredientModels for the recipeModel
                foreach(RecipeIngredientList singleDBItemIngredients in singleDBItem.RecipeIngredientList)
                {
                    IngredientAmount ingredientAmount = singleDBItemIngredients.IngredientAmount;
                    AmountModel parsedAmount = new AmountModel(ingredientAmount.amount, ingredientAmount.units);

                    IngredientModel ingredientModelForRecipe = new IngredientModel(singleDBItemIngredients.Ingredient.id, singleDBItemIngredients.Ingredient.name, parsedAmount, singleDBItemIngredients.Ingredient.expirationDate, singleDBItemIngredients.Ingredient.placeOfPurchase, singleDBItemIngredients.Ingredient.notes);
                    listOfIngredientsForRecipe.Add(ingredientModelForRecipe);
                }

                //convert each Recipe to a RecipeModel
                RecipeModel recipeModel = new RecipeModel(singleDBItem.name, listOfIngredientsForRecipe, singleDBItem.listOfDirections, singleDBItem.editHistory);
                listOfRecipes.Add(recipeModel);
            }
            return listOfRecipes;
        }

        public List<RecipeModel> Search(string name) 
        {
            List<RecipeModel> listOfRecipes = new List<RecipeModel>();

            var listOfRecipesDB = _inventoryDatabaseEntities.Recipe
                .Include("RecipeIngredientList")
                .Include("RecipeIngredientList_Ingredient")
                .Include("RecipeIngredientList_IngredientAmount")
                .Where(m => m.name.Contains(name));

            foreach (Recipe singleDBItem in listOfRecipesDB)
            {
                var listOfIngredientsForRecipe = new List<IngredientModel>();

                //convert recipeIngredientList to list of IngredientModels for the recipeModel
                foreach (RecipeIngredientList singleDBItemIngredients in singleDBItem.RecipeIngredientList)
                {
                    IngredientAmount ingredientAmount = singleDBItemIngredients.IngredientAmount;
                    AmountModel parsedAmount = new AmountModel(ingredientAmount.amount, ingredientAmount.units);

                    IngredientModel ingredientModelForRecipe = new IngredientModel(singleDBItemIngredients.Ingredient.id, singleDBItemIngredients.Ingredient.name, parsedAmount, singleDBItemIngredients.Ingredient.expirationDate, singleDBItemIngredients.Ingredient.placeOfPurchase, singleDBItemIngredients.Ingredient.notes);
                    listOfIngredientsForRecipe.Add(ingredientModelForRecipe);
                }

                //convert each Recipe to a RecipeModel
                RecipeModel recipeModel = new RecipeModel(singleDBItem.name, listOfIngredientsForRecipe, singleDBItem.listOfDirections, singleDBItem.editHistory);
                listOfRecipes.Add(recipeModel);
            }
            return listOfRecipes;
        }
    }
}
