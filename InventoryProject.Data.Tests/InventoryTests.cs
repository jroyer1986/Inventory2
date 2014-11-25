﻿using System;
using NUnit.Framework;
namespace InventoryProject.Data.Tests
{
    [TestFixture]
    public class InventoryTests
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
            inventory.Add(new IngredientModel() { Name = "lavender", Amount = new AmountModel (20) });
            inventory.Add(new IngredientModel() { Name = "chamomile", Amount = new AmountModel(2) });

            return inventory;
        }

        [Test]
        public void CanAddIngredientToInventory()
        {
            Inventory myInventory = new Inventory();
            myInventory.Add(new IngredientModel() { Name = "flour", Amount = new AmountModel(20) });
            Assert.IsTrue(myInventory.Has("flour"));
        }  

        [Test]
        public void CanUseIngredientFromInventory()
        {
            Inventory myInventory = new Inventory();
            myInventory.Add(new IngredientModel() { Name = "flour", Amount = new AmountModel(1000) });

            IngredientModel usedIngredient = myInventory.GetByName("flour");

            myInventory.Use(usedIngredient.Name, 200);
            Assert.AreEqual(800, (decimal)usedIngredient.Amount.Total);            
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CannotUseMoreOfAnIngredientThanAvailable() 
        {
            Inventory myInventory = GetInventory();

            IngredientModel usedIngredient = myInventory.GetByName("flour");

            myInventory.Use(usedIngredient.Name, 2000);           
       }

        [Test]
        public void SameIngredientsCombineAmounts()
        {
            Inventory myInventory = GetInventory();
            IngredientModel ingredientToAdd = new IngredientModel() { Name = "flour", Amount = new AmountModel(50) };

            myInventory.Add(ingredientToAdd);

            Assert.AreEqual(650, myInventory.GetAmount("flour"));
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
            IngredientModel newIngredient = new IngredientModel() {Name = "corn starch", Amount = new AmountModel(222)};

            Inventory myInventory = GetInventory();
            myInventory.Add(newIngredient);
            myInventory.Remove(newIngredient);

            Assert.IsFalse(myInventory.Has("corn starch"));
        }


    }
}
