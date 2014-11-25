using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Data.Tests
{
    [TestFixture]
    public class RecipeTests
    {
        public Inventory GetInventory()
        {
            Inventory inventory = new Inventory();
            inventory.Add(new IngredientModel() { Name = "flour", Amount = new AmountModel(600) });
            inventory.Add(new IngredientModel() { Name = "sugar", Amount = new AmountModel(18) });
            inventory.Add(new IngredientModel() { Name = "salt", Amount = new AmountModel(12) });
            inventory.Add(new IngredientModel() { Name = "hops", Amount = new AmountModel(99) });
            inventory.Add(new IngredientModel() { Name = "rice", Amount = new AmountModel(7) });
            inventory.Add(new IngredientModel() { Name = "oats", Amount = new AmountModel(80) });
            inventory.Add(new IngredientModel() { Name = "lavender", Amount = new AmountModel(20) });
            inventory.Add(new IngredientModel() { Name = "chamomile", Amount = new AmountModel(2) });
            inventory.Add(new IngredientModel() { Name = "baking soda", Amount = new AmountModel(100) });
            inventory.Add(new IngredientModel() { Name = "corn starch", Amount = new AmountModel(150) });

            return inventory;
        }
        public RecipeBookModel GetRecipeBook()
        {
            List<IngredientModel> listOfMuffinIngredients = new List<IngredientModel>(){
                new IngredientModel("flour", 1200),
                new IngredientModel("sugar", 10),
                new IngredientModel("salt", 2)
            };
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
            myRecipes.Add(new RecipeModel("bread recipe", listOfBreadIngredients, "mix ingredients. Bake at 400", "created recipe" ));
            myRecipes.Add(new RecipeModel("cookie recipe", listOfCookieIngredients, "mix ingredients together. Bake at 350", "created recipe"));
            myRecipes.Add(new RecipeModel("muffin recipe", listOfMuffinIngredients, "mix ingredients togther well. Bake at 340 for 20 minutes", "created recipe"));
            
            return myRecipes;
        }

        [Test]
        public void AddRecipeToRecipeBook()
        {
            RecipeBookModel myRecipeBook = GetRecipeBook();
            List<IngredientModel> listOfCookieIngredients = new List<IngredientModel>(){
            new IngredientModel("flour", 400),
            new IngredientModel("salt", 5),
            new IngredientModel("baking soda", 75)
            };
            RecipeModel newRecipe = new RecipeModel("Cookies", listOfCookieIngredients, "combine all ingredients. Bake at 400", "created recipe");
            myRecipeBook.Add(newRecipe);
            Assert.IsTrue(myRecipeBook.Has(newRecipe.NameOfRecipe));
        }

        [Test]
        public void CreatingRecipeDoesNotRemoveAmountsOfIngredientsFromInventory()
        {
            Inventory myInventory = GetInventory();
            List<IngredientModel> listOfIngredientsForRecipe = new List<IngredientModel>(){
                new IngredientModel("flour", 600),
                new IngredientModel("sugar", 300),
                new IngredientModel("corn starch", 100)                
            };
            RecipeModel newRecipe = new RecipeModel("Bread Recipe", listOfIngredientsForRecipe, "Mix dry ingredients. Add water.  Bake at 365 for 20 min", "created recipe");

            var AmountInDatabase = myInventory.GetAmount("flour");

            newRecipe.AddIngredientAndAmountToRecipe(myInventory.GetByName("flour").ToString(), 300);

            Assert.AreEqual(AmountInDatabase, myInventory.GetAmount("flour"));
            Assert.AreEqual(newRecipe.GetIngredientAmountFromRecipe("flour").Total, 600);
        }

        [Test]
        public void UsingRecipeRemovesAmountsOfIngredientsFromInventory()
        {
            Inventory myInventory = GetInventory();
            RecipeBookModel myRecipes = GetRecipeBook();
            
            var AmountInDatabase = myInventory.GetByName("flour").Amount.Total;
            
            RecipeModel recipeToUse = myRecipes.GetRecipe("cookie recipe");
            if (recipeToUse != null)
            {
                myRecipes.UseRecipe(myInventory, recipeToUse.NameOfRecipe.ToString());
            }

            decimal expectedFlourAmt = AmountInDatabase - recipeToUse.GetIngredientAmountFromRecipe("flour").Total;
            Assert.AreEqual(expectedFlourAmt, myInventory.GetByName("flour").Amount.Total);
        }
        
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void UsingRecipeThrowsExceptionWhenInsufficientAmountsExist()
        {
            Inventory myInventory = GetInventory();
            RecipeBookModel myRecipes = GetRecipeBook();

            RecipeModel recipeToUse = myRecipes.GetRecipe("muffin recipe");

            myRecipes.UseRecipe(myInventory, recipeToUse.NameOfRecipe);
        }

        [Test]
        public void ScalingRecipeAmounts()
        {
            decimal scaleFactor = 2;
            Inventory myInventory = GetInventory();
            RecipeBookModel myRecipes = GetRecipeBook();
            RecipeModel RecipeToScaleCheck = myRecipes.GetRecipe("bread recipe");

            RecipeModel recipeToScale = myRecipes.GetRecipe("bread recipe");

            if(recipeToScale != null)
            {
                myRecipes.MultipleRecipeAmounts(recipeToScale.NameOfRecipe, scaleFactor);
            }

            Assert.AreEqual(600, myRecipes.GetRecipe("bread recipe").GetIngredientAmountFromRecipe("flour").Total);
        }

        [Test]
        public void EditsToRecipeAreLoggedInRecipeHistory()
        {
            RecipeBookModel myRecipes = GetRecipeBook();
            string editComments = "this is where I would describe the changes I made and why I made them";
            List<IngredientModel> newCookieIngredients = new List<IngredientModel>()
            {
                new IngredientModel("flour", 111),
                new IngredientModel("sugar", 11),
                new IngredientModel("salt", 1)
            };
            RecipeModel recipeToEdit = myRecipes.GetRecipe("cookie recipe");
            RecipeModel EditedRecipe = new RecipeModel(recipeToEdit.NameOfRecipe, newCookieIngredients, recipeToEdit.ListOfDirections, recipeToEdit.EditHistory);

            myRecipes.Edit(recipeToEdit.NameOfRecipe, EditedRecipe, editComments);

            Assert.AreEqual(111, myRecipes.GetRecipe("cookie recipe").GetIngredientAmountFromRecipe("flour").Total);
        }

    }
}
