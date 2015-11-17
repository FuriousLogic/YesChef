using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YesChef_DataLayer.DataClasses;

namespace YesChef_DataLayer.Tests
{
    [TestFixture]
    public class RecipeTests
    {
        [Test]
        public void ShouldCreateRecipeWithoutItems()
        {
            var db = new YesChefContext();
            var count = db.Recipies.ToList().Count;

            var r = RecipeHandler.CreateRecipe(string.Format("RecipeName_{0}", Guid.NewGuid().ToString()));
            Assert.That(r, !Is.Null);
            Assert.That(r.Id, Is.GreaterThan(0));
            Assert.That(count, Is.LessThan(db.Recipies.ToList().Count));
            Assert.That(r.Ingredients.Count, Is.EqualTo(0));
        }
        [Test]
        public void ShouldGetAllRecipies()
        {
            List<Recipe> recipies = RecipeHandler.GetAllRecipies();
            Assert.That(recipies, !Is.Null);

            var r = RecipeHandler.CreateRecipe(string.Format("RecipeName_{0}", Guid.NewGuid().ToString()));

            Assert.That(recipies.Count, Is.LessThan(RecipeHandler.GetAllRecipies().Count));
        }
    }
}
