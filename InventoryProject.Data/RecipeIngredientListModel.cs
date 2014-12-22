using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Data
{
    public class RecipeIngredientListModel
    {
        public int ID { get; set; }
        public int IngredientID { get; set; }
        public int RecipeID { get; set; }
        public int AmountID { get; set; }

        public RecipeIngredientListModel(int id, int ingredientID, int recipeID, int amountID)
        {
            ID = id;
            IngredientID = ingredientID;
            RecipeID = recipeID;
            AmountID = amountID;
        }

		public RecipeIngredientListModel(RecipeIngredientListModel recipeIngredientList)
        {
            ID = recipeIngredientList.ID;
            IngredientID = recipeIngredientList.IngredientID;
            RecipeID = recipeIngredientList.RecipeID;
            AmountID = recipeIngredientList.AmountID;
        }

        public RecipeIngredientListModel() { }
    }
}
