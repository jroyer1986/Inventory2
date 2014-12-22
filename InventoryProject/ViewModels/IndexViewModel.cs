using InventoryProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryProject.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<IngredientModel> IngredientList
        {
            get;
            set;
        }

        public IEnumerable<RecipeModel> RecipeList
        {
            get;
            set;
        }

        public IndexViewModel() { }
        public IndexViewModel(IEnumerable<IngredientModel> ingredients, IEnumerable<RecipeModel> recipes)
        {
            IngredientList = ingredients;
            RecipeList = recipes;
        }
    }
}