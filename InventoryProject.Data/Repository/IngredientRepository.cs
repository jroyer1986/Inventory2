﻿using System;
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
    }
}
