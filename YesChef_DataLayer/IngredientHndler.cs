using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YesChef_DataClasses;

namespace YesChef_DataLayer
{
    public static class IngredientHandler
    {
        public static Ingredient CreateIngredient(Recipe recipe, QuantityType quantityType, string name, int quantity)
        {
            var db = new YesChefContext();

            var ri = db.Ingredients.Add(new Ingredient {
                Recipe = recipe,
                Name = name,
                QuantityType = quantityType,
                Quantity = quantity
            });

            return ri;
        }
    }
}
