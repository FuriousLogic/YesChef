using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YesChef_DataClasses;

namespace YesChef_DataLayer.Tests
{
    [TestFixture]
    public class IngredientTests
    {
        [Test]
        public void ShouldCreateIngredient()
        {
            var r = RecipeHandler.CreateRecipe(string.Format("RecipeName_{0}", Guid.NewGuid().ToString()));
            Assert.That(r, !Is.Null);

            var qt = QuantityTypeHandler.CreateQuantityType(string.Format("quantityTypeName_{0}", Guid.NewGuid().ToString()));
            Assert.That(qt, !Is.Null);

            var i = IngredientHandler.CreateIngredient(
                recipe: r, 
                quantityType: qt,
                name: string.Format("ingredientName_{0}", Guid.NewGuid()),
                quantity: 1);
            Assert.That(i, !Is.Null);
            Assert.That(i.Id, Is.GreaterThan(0));

            Assert.That(r.Ingredients.Count, Is.EqualTo(1));
            Assert.That(i.Recipe.Id, Is.EqualTo(r.Id));
        }
    }
}
