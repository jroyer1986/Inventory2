using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryProject.Data.Repository
{
    public class RecipeRepository
    {
        InventoryDatabaseEntities _recipeRepository = new InventoryDatabaseEntities();

        public void Create(RecipeModel recipeToAdd)
        {
            Recipe newRecipe = new Recipe()
            {
                id = recipeToAdd.ID,
                name = recipeToAdd.NameOfRecipe,
                listOfDirections = recipeToAdd.ListOfDirections,
                editHistory = recipeToAdd.EditHistory
            };
        }
    }
}
