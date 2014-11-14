using System;
using NUnit.Framework;
namespace InventoryProject.Data.Tests
{
    [TestFixture]
    public class InventoryTests
    {

        public Inventory GetInventory()
        {
            Inventory inventory = new Inventory();
            inventory.Add(new Ingredient() { Name = "flour", Amount = 200 });
            inventory.Add(new Ingredient() { Name = "sugar", Amount = 18 });
            inventory.Add(new Ingredient() { Name = "salt", Amount = 12});
            inventory.Add(new Ingredient() { Name = "hops", Amount = 99 });
            inventory.Add(new Ingredient() { Name = "rice", Amount = 7 });
            inventory.Add(new Ingredient() { Name = "oats", Amount = 80 });
            inventory.Add(new Ingredient() { Name = "lavender", Amount = 20 });
            inventory.Add(new Ingredient() { Name = "chamomile", Amount = 2 });

            return inventory;
        }

        [Test]
        public void CanAddIngredientToInventory()
        {
            Inventory myInventory = new Inventory();
            myInventory.Add(new Ingredient() { Name = "flour", Amount = 20 });
            Assert.IsTrue(myInventory.Has("flour"));
        }  

        [Test]
        public void CanUseIngredientFromInventory()
        {
            Inventory myInventory = new Inventory();
            myInventory.Add(new Ingredient() { Name = "flour", Amount = 1000 });

            Ingredient usedIngredient = myInventory.GetByName("flour");

            myInventory.Use(usedIngredient.Name, 200);
            Assert.AreEqual(800, (decimal)usedIngredient.Amount);            
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CannotUseMoreOfAnIngredientThanAvailable() 
        {
            Inventory myInventory = GetInventory();

            Ingredient usedIngredient = myInventory.GetByName("flour");

            myInventory.Use(usedIngredient.Name, 2000);
           
       }

        [Test]
        public void SameIngredientsCombineAmounts()
        {
            Inventory myInventory = GetInventory();
            Ingredient ingredientToAdd = new Ingredient() { Name = "flour", Amount = 500 };

            if (myInventory.Has(ingredientToAdd.Name) == true)
            {
                var ingredientFromDB = myInventory.GetByName(ingredientToAdd.Name);
                myInventory.CombineAmounts(ingredientFromDB.Name, ingredientToAdd.Amount);
            }

            Assert.AreEqual(700, myInventory.GetAmount("flour"));
        }
        
        [Test]
        public void TestingInventory()
        {
            Inventory myInventory = GetInventory();
            Assert.That(myInventory.Has("flour"), "no flour");
        }

        [Test]
        public void CanDeleteIngredientFromInventory()
        {
            Ingredient newIngredient = new Ingredient() {Name = "corn starch", Amount = 222};

            Inventory myInventory = GetInventory();
            myInventory.Add(newIngredient);
            myInventory.Remove(newIngredient);

            Assert.IsFalse(myInventory.Has("corn starch"));
        }


    }
}
