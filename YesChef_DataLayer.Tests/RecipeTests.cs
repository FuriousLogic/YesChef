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
        public void ShouldBeAbleToNavigateToAndFromStep()
        {
            var recipe = RecipeHandler.CreateRecipe($"name {Guid.NewGuid()}");
            var step = StepHandler.CreateStep($"desc {Guid.NewGuid()}", 1, recipe.Id);
            Assert.That(step, Is.Not.Null);
            Assert.That(step.Recipe, Is.Not.Null);
            Assert.That(step.Recipe.Id, Is.EqualTo(recipe.Id));

            recipe = RecipeHandler.GetRecipe(recipe.Id);
            Assert.That(recipe.Steps.Count, Is.EqualTo(1));
            Assert.That(recipe.Steps.ToList()[0].Id, Is.EqualTo(step.Id));
        }
        [Test]
        public void ShouldBeAbleToNavigateToAndFromIngredient()
        {
            var recipe = RecipeHandler.CreateRecipe($"name {Guid.NewGuid()}");
            var quantityType = QuantityTypeHandler.CreateQuantityType($"name {Guid.NewGuid()}");
            var ingredient = IngredientHandler.CreateIngredient(recipe.Id, quantityType.Id, $"name {Guid.NewGuid()}", 1);
            Assert.That(ingredient, Is.Not.Null);
            Assert.That(ingredient.Recipe, Is.Not.Null);
            Assert.That(ingredient.Recipe.Id, Is.EqualTo(recipe.Id));

            recipe = RecipeHandler.GetRecipe(recipe.Id);
            Assert.That(recipe.Ingredients.Count, Is.EqualTo(1));
            Assert.That(recipe.Ingredients.ToList()[0].Id, Is.EqualTo(ingredient.Id));
        }
        [Test]
        public void ShouldCreateRecipeWithoutItems()
        {
            var db = new YesChefContext();
            var count = db.Recipies.ToList().Count;

            var r = RecipeHandler.CreateRecipe($"RecipeName_{Guid.NewGuid()}");
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

            RecipeHandler.CreateRecipe($"RecipeName_{Guid.NewGuid()}");

            Assert.That(recipies.Count, Is.LessThan(RecipeHandler.GetAllRecipies().Count));
        }

        [Test]
        public void ShouldGetRecipe()
        {
            var recipe1 = RecipeHandler.CreateRecipe($"RecipeName_{Guid.NewGuid()}");
            Assert.That(recipe1, !Is.Null);
            Assert.That(recipe1.Id, Is.GreaterThan(0));

            var recipe2 = RecipeHandler.GetRecipe(recipe1.Id);
            Assert.That(recipe2, !Is.Null);
            Assert.That(recipe2.Id, Is.EqualTo(recipe1.Id));
        }
        [Test]
        public void ShouldCalcTimeForOneStepRecipe()
        {
            var recipe = RecipeHandler.CreateRecipe($"recipe name {Guid.NewGuid()}");
            StepHandler.CreateStep($"description {Guid.NewGuid()}", 9, recipe.Id);

            var minutes = RecipeHandler.GetRecipeBusyTime(recipe.Id);

            Assert.That(minutes, Is.EqualTo(9));
        }

        //[Test]
        //public void ShouldCalcTimeForSeriesStepRecipe()
        //{
        //    var recipe = RecipeHandler.CreateRecipe($"recipe name {Guid.NewGuid()}");
        //    var id = recipe.Id;

        //    var step1 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 7, recipe.Id);
        //    var step2 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 8, recipe.Id);
        //    var step3 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 9, recipe.Id);
        //    Assert.That(recipe.Id, Is.EqualTo(id));

        //    var s1Id = step1.Id;
        //    var s2Id = step2.Id;
        //    var s3Id = step3.Id;
        //    StepDependancyHandler.CreateStepDependancy(step1.Id, step2.Id);
        //    StepDependancyHandler.CreateStepDependancy(step2.Id, step3.Id);
        //    Assert.That(step1.Id, Is.EqualTo(s1Id));
        //    Assert.That(step2.Id, Is.EqualTo(s2Id));
        //    Assert.That(step3.Id, Is.EqualTo(s3Id));

        //    var minutes = RecipeHandler.GetRecipeBusyTime(recipe.Id);

        //    Assert.That(minutes, Is.EqualTo(24));
        //}

        [Test]
        public void ShouldCalcTimeForParallelStepRecipeNoFreeTime()
        {
            var recipe = RecipeHandler.CreateRecipe($"recipe name {Guid.NewGuid()}");
            var id = recipe.Id;

            var step1 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 7, recipe.Id);
            var step2 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 8, recipe.Id);
            Assert.That(recipe.Id, Is.EqualTo(id));

            var minutesBusy = RecipeHandler.GetRecipeBusyTime(recipe.Id);
            var minutesFree = RecipeHandler.GetRecipeFreeTime(recipe.Id);

            Assert.That(minutesBusy, Is.EqualTo(15));
            Assert.That(minutesFree, Is.EqualTo(0));
        }

        [Test]
        public void ShouldCalcTimeForParallelStepRecipeFreeTime()
        {
            var recipe = RecipeHandler.CreateRecipe($"recipe name {Guid.NewGuid()}");
            var id = recipe.Id;

            var step1 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 7, recipe.Id, isFreeTime: true);
            var step2 = StepHandler.CreateStep($"description {Guid.NewGuid()}", 8, recipe.Id);
            Assert.That(recipe.Id, Is.EqualTo(id));

            var minutesBusy = RecipeHandler.GetRecipeBusyTime(recipe.Id);
            var minutesFree = RecipeHandler.GetRecipeFreeTime(recipe.Id);

            Assert.That(minutesBusy, Is.EqualTo(8));
            Assert.That(minutesFree, Is.EqualTo(7));
        }
    }
}
