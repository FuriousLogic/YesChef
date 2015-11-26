using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YesChef_DataLayer.DataClasses
{
    public class Ingredient
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public int Quantity { get; set; }

        public int RecipeId { get; set; }
        [ForeignKey("RecipeId")]
        public virtual Recipe Recipe { get; set; }

        public int QuantityTypeId { get; set; }
        [ForeignKey("QuantityTypeId")]
        public virtual QuantityType QuantityType { get; set; }
    }
}
