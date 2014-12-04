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
        public DateTime? ExpirationDate
        { get; set; }
        public string PlaceOfPurchase
        { get; set; }
        public string Notes
        { get; set; }

        public IngredientModel(int id, string name, AmountModel amount, DateTime? expirationDate, string placeOfPurchase, string notes)
        {
            ID = id;
            Name = name;
            AmountModel newAmount = new AmountModel(amount.Total);
            Amount = newAmount;
            ExpirationDate = expirationDate;
            PlaceOfPurchase = placeOfPurchase;
            Notes = notes;
        }

        public IngredientModel(IngredientModel ingredient)
        {
            ID = ingredient.ID;
            Name = ingredient.Name;
            Amount = ingredient.Amount;
            ExpirationDate = ingredient.ExpirationDate;
            PlaceOfPurchase = ingredient.PlaceOfPurchase;
            Notes = ingredient.Notes;
        }

        public IngredientModel() { }

        public IngredientModel(string name, decimal amount)
        {
            AmountModel amountModel = new AmountModel(amount);
            Amount = amountModel;
        }
    }
}
