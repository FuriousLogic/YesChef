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
            var recipies = RecipeHandler.GetAllRecipies();
            Assert.That(recipies, !Is.Null);

            var recipe = RecipeHandler.CreateRecipe($"RecipeName_{Guid.NewGuid()}");

            Assert.That(recipies.Count, Is.LessThan(RecipeHandler.GetAllRecipies().Count));
        }

        [Test]
        public void ShouldCalcTimeForOneStepRecipe()
        {
            var recipe = RecipeHandler.CreateRecipe($"recipe name {Guid.NewGuid()}");
            StepHandler.CreateStep($"description {Guid.NewGuid()}", 9, recipe);

            var minutes = RecipeHandler.GetRecipeBusyTime(recipe);

            Assert.That(minutes, Is.EqualTo(9));
        }

        [Test]
        public void ShouldCalcTimeForSeriesStepRecipe()
        {
            var recipe = RecipeHandler.CreateRecipe($"recipe name {Guid.NewGuid()}");

            var step1 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 7, recipe);
            var step2 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 8, recipe);
            var step3 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 9, recipe);

            StepDependancyHandler.CreateStepDependancy(step1, step2);
            StepDependancyHandler.CreateStepDependancy(step2, step3);

            var minutes = RecipeHandler.GetRecipeBusyTime(recipe);

            Assert.That(minutes, Is.EqualTo(24));
        }

        [Test]
        public void ShouldCalcTimeForParallelStepRecipeNoFreeTime()
        {
            var recipe = RecipeHandler.CreateRecipe($"recipe name {Guid.NewGuid()}");

            var step1 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 7, recipe);
            var step2 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 8, recipe);

            var minutesBusy = RecipeHandler.GetRecipeBusyTime(recipe);
            var minutesFree = RecipeHandler.GetRecipeFreeTime(recipe);

            Assert.That(minutesBusy, Is.EqualTo(15));
            Assert.That(minutesFree, Is.EqualTo(0));
        }

        [Test]
        public void ShouldCalcTimeForParallelStepRecipeFreeTime()
        {
            var recipe = RecipeHandler.CreateRecipe($"recipe name {Guid.NewGuid()}");

            var step1 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 7, recipe, isFreeTime: true);
            var step2 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 8, recipe);

            var minutesBusy = RecipeHandler.GetRecipeBusyTime(recipe);
            var minutesFree = RecipeHandler.GetRecipeFreeTime(recipe);

            Assert.That(minutesBusy, Is.EqualTo(8));
            Assert.That(minutesFree, Is.EqualTo(7));
        }
    }
}
