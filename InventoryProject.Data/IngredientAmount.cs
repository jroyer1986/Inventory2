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
    
    public partial class IngredientAmount
    {
        public IngredientAmount()
        {
            this.Ingredient = new HashSet<Ingredient>();
        }
    
        public int id { get; set; }
        public decimal amount { get; set; }
        public string units { get; set; }
    
        public virtual ICollection<Ingredient> Ingredient { get; set; }
    }
}
