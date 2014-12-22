using InventoryProject.Data;
using InventoryProject.Data.Repository;
using InventoryProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryProject.Controllers
{
    public class HomeController : Controller
    {
        RecipeRepository _recipeRepository = new RecipeRepository();
        IngredientRepository _ingredientRepository = new IngredientRepository();

        public ActionResult Index()
        {
            //get a list of Ingredients from the repository
            IEnumerable<IngredientModel> listOfIngredients = _ingredientRepository.GetListOfIngredients();
            IEnumerable<RecipeModel> listOfRecipes = _recipeRepository.GetListOfRecipes();

            IndexViewModel viewIndex = new IndexViewModel(listOfIngredients, listOfRecipes);
            return View(viewIndex);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}