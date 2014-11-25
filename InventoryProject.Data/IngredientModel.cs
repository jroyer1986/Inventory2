using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Data
{
    public class IngredientModel
    {
        public int ID
        {get;set;}
        public string Name
        { get; set; }
        public AmountModel Amount
        { get; set; }
        public DateTime ExpirationDate
        { get; set; }
        public string PlaceOfPurchase
        { get; set; }
        public string Notes
        { get; set; }

        public IngredientModel(string name, decimal amount)
        {
            Name = name;
            AmountModel newAmount = new AmountModel(amount);
            Amount = newAmount;
        }

        public IngredientModel(IngredientModel ingredient)
        {
            Name = ingredient.Name;
            Amount = ingredient.Amount;
        }

        public IngredientModel() { }
    }
}
