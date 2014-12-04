using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Data.Repository
{
    public class IngredientRepository
    {
        InventoryDatabaseEntities _inventoryDatabaseEntities = new InventoryDatabaseEntities();

        public IEnumerable<IngredientModel> GetIngredients()
        {
            var ingredients = _inventoryDatabaseEntities.Ingredient.AsEnumerable();

            List<IngredientModel> ingredientsForController = new List<IngredientModel>();

            foreach (Ingredient ingredient in ingredients)
            {
                AmountModel amountParsed = new AmountModel(ingredient.IngredientAmount.amount);

                IngredientModel ingredientModel = new IngredientModel(ingredient.id, ingredient.name, amountParsed, ingredient.expirationDate, ingredient.placeOfPurchase, ingredient.notes);
                ingredientsForController.Add(ingredientModel);
            }
           
            return ingredientsForController;
        }

        public void AddIngredient(IngredientModel newIngredient)
        {
            if (!_inventoryDatabaseEntities.Ingredient.Any(m => m.name == newIngredient.Name))
            {
                IngredientAmount newIngredientAmount = new IngredientAmount();
                newIngredientAmount.amount = newIngredient.Amount.Total;

                Ingredient dbIngredient = new Ingredient();
                dbIngredient.id = newIngredient.ID;
                dbIngredient.name = newIngredient.Name;
                dbIngredient.IngredientAmount = newIngredientAmount;
                dbIngredient.expirationDate = newIngredient.ExpirationDate;
                dbIngredient.placeOfPurchase = newIngredient.PlaceOfPurchase;
                dbIngredient.notes = newIngredient.Notes;

                _inventoryDatabaseEntities.Ingredient.Add(dbIngredient);
                _inventoryDatabaseEntities.SaveChanges();
            }
            else Combine(newIngredient);
        }

        public void CombineIngredientAmounts(IngredientModel ingredientToCombine)
        {
            Ingredient dbIngredient = _inventoryDatabaseEntities.Ingredient.Include("IngredientAmount").FirstOrDefault(m => m.name == ingredientToCombine.Name);
            decimal currentAmount = dbIngredient.IngredientAmount.amount;
            decimal newAmount = currentAmount + ingredientToCombine.Amount.Total;

            dbIngredient.IngredientAmount.amount = newAmount;

            _inventoryDatabaseEntities.SaveChanges();
        }

        public void DeleteIngredient(string name)
        {
            Ingredient ingredentToDelete = _inventoryDatabaseEntities.Ingredient.FirstOrDefault(m => m.name == name);

            _inventoryDatabaseEntities.Ingredient.Remove(ingredentToDelete);
            _inventoryDatabaseEntities.SaveChanges();
        }

        public IEnumerable<IngredientModel> Search(string name)
        {
            var searchResults = _inventoryDatabaseEntities.Ingredient.Where(m => m.name.Contains(name));

            var listOfIngredientsForController = new List<IngredientModel>();
            
            foreach(Ingredient singleSearchResult in searchResults)
            {
                AmountModel ingredientAmount = new AmountModel(singleSearchResult.IngredientAmount.amount);
                IngredientModel ingredientFromSearch = new IngredientModel(singleSearchResult.id, singleSearchResult.name, ingredientAmount, singleSearchResult.expirationDate, singleSearchResult.placeOfPurchase, singleSearchResult.notes);

                listOfIngredientsForController.Add(ingredientFromSearch);
            }
            return listOfIngredientsForController;
        }

        public IngredientModel GetIngredientByID(int id)
        {
            Ingredient dbIngredient = _inventoryDatabaseEntities.Ingredient.Include("IngredientAmount").FirstOrDefault(m => m.id == id);

            if(dbIngredient != null)
            {
                AmountModel amountModel = new AmountModel(dbIngredient.IngredientAmount.amount);
                IngredientModel ingredientModel = new IngredientModel(dbIngredient.id, dbIngredient.name, amountModel, dbIngredient.expirationDate, dbIngredient.placeOfPurchase, dbIngredient.notes);
                return ingredientModel;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<IngredientModel> GetListOfIngredients()
        {
            var listOfDatabaseIngredients = _inventoryDatabaseEntities.Ingredient.AsEnumerable();

            List<IngredientModel> listOfIngredientsForController = new List<IngredientModel>();

            if (listOfDatabaseIngredients != null)
            {
                foreach (Ingredient ingredientFromList in listOfDatabaseIngredients)
                {
                    AmountModel ingredientAmount = new AmountModel(ingredientFromList.IngredientAmount.amount);
                    IngredientModel ingredientModel = new IngredientModel(ingredientFromList.id, ingredientFromList.name, ingredientAmount, ingredientFromList.expirationDate, ingredientFromList.placeOfPurchase, ingredientFromList.notes);

                    listOfIngredientsForController.Add(ingredientModel);
                }
                return listOfIngredientsForController;
            }
            return null;
        }

        public void UseIngredient(string name, decimal amountToUse)
        {
            Ingredient ingredientToUse = _inventoryDatabaseEntities.Ingredient.Include("IngredientAmount").FirstOrDefault(m => m.name == name);

            var currentAmount = ingredientToUse.IngredientAmount.amount;
            var newAmount = currentAmount - amountToUse
        }
    }
}
