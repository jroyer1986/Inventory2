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
            inventory.Add(new Ingredient() { Name = "flour", Amount = 200 });
            inventory.Add(new Ingredient() { Name = "sugar", Amount = 18 });
            inventory.Add(new Ingredient() { Name = "salt", Amount = 12 });
            inventory.Add(new Ingredient() { Name = "hops", Amount = 99 });
            inventory.Add(new Ingredient() { Name = "rice", Amount = 7 });
            inventory.Add(new Ingredient() { Name = "oats", Amount = 80 });
            inventory.Add(new Ingredient() { Name = "lavender", Amount = 20 });
            inventory.Add(new Ingredient() { Name = "chamomile", Amount = 2 });

            return inventory;
        }

        [Test]
        public void CreatingRecipeDoesNotRemoveAmountsOfIngredientsFromInventory()
        {
            Inventory myInventory = GetInventory();
            Recipe newRecipe = new Recipe();
            var AmountInDatabase = myInventory.GetAmount("flour");

            newRecipe.Add(myInventory.GetByName("flour"), 300);
            Assert.AreEqual(AmountInDatabase, myInventory.GetAmount("flour"));
            Assert.AreEqual(newRecipe.IngredientAmount("flour"), 300);
        }

        [Test]
        public void UsingRecipeRemovesAmountsOfIngredientsFromInventory() { }
        
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void UsingRecipeThrowsExceptionWhenInsufficientAmountsExist() { }

        [Test]
        public void ScalingRecipeAmounts() { }

        [Test]
        public void EditsToRecipeAreLoggedInRecipeHistory() { }

    }
}
