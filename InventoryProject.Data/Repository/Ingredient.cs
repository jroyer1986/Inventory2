//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InventoryProject.Data.Repository
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ingredient
    {
        public int id { get; set; }
        public string name { get; set; }
        public int ingredientAmountID { get; set; }
        public Nullable<System.DateTime> expirationDate { get; set; }
        public string placeOfPurchase { get; set; }
        public string Notes { get; set; }
    
        public virtual IngredientAmount IngredientAmount { get; set; }
    }
}
