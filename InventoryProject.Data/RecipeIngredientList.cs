//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InventoryProject.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class RecipeIngredientList
    {
        public RecipeIngredientList()
        {
            this.Recipe = new HashSet<Recipe>();
        }
    
        public int id { get; set; }
        public int ingredientID { get; set; }
        public int recipeID { get; set; }
    
        public virtual ICollection<Recipe> Recipe { get; set; }
    }
}