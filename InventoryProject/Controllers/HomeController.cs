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

        public ActionResult RecipeList()
        {
            IEnumerable<RecipeModel> listOfRecipes = _recipeRepository.GetListOfRecipes();
            return View(listOfRecipes);
        }

        public ActionResult IngredientList()
        {
            IEnumerable<IngredientModel> listOfIngredients = _ingredientRepository.GetListOfIngredients();
            return View(listOfIngredients);
        }

        public ActionResult IngredientDetails(int id)
        {
            IngredientModel ingredientToDetail = _ingredientRepository.GetIngredientByID(id);
            return View(ingredientToDetail);
        }

        public ActionResult RecipeDetails(int id)
        {
            RecipeModel recipeToDetail = _recipeRepository.GetRecipeByID(id);
            return View(recipeToDetail);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult CreateIngredient(IngredientModel newIngredientModel)
        {
            _ingredientRepository.AddIngredient(newIngredientModel);
            return RedirectToAction("IngredientList");
        }
    }
}