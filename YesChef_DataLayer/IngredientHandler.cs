using System.Data.Entity;
using System.Linq;
using YesChef_DataLayer.DataClasses;

namespace YesChef_DataLayer
{
    public static class IngredientHandler
    {
        public static Ingredient CreateIngredient(int recipeId, int quantityTypeId, string name, int quantity)
        {
            var db = new YesChefContext();

            var ingredient = db.Ingredients.Add(new Ingredient
            {
                RecipeId = recipeId,
                Name = name,
                QuantityTypeId = quantityTypeId,
                Quantity = quantity
            });
            db.SaveChanges();

            return GetIngredient(ingredient.Id);
        }

        public static Ingredient GetIngredient(int ingredientId)
        {
            var db = new YesChefContext();
            var ingredient = db.Ingredients
                .Include(i=>i.QuantityType)
                .Include(i=>i.Recipe)
                .Single(i=>i.Id==ingredientId);
            return ingredient;
        }
    }
}
