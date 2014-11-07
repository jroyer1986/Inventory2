using System;
using NUnit.Framework;
namespace InventoryProject.Data.Tests
{
    [TestFixture]
    public class InventoryTests
    {
        [Test]
        public void CanAddIngredientToInventory()
        {
            Inventory myInventory = new Inventory();
            myInventory.Add(new Ingredient() { Name = "flour", Amount = "20grams" });
            Assert.IsTrue(myInventory.Has("flour"));
        }
    }
}
