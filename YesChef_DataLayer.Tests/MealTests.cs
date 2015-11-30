using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using YesChef_DataLayer.DataClasses;

namespace YesChef_DataLayer.Tests
{
    [TestFixture]
    public class MealTests
    {
        [Test]
        public void ShouldCreateMealAndNav()
        {
            string name = $"Name {Guid.NewGuid()}";
            var sousChef = SousChefHandler.CreateSousChef($"Name {Guid.NewGuid()}", "1@1.com", "password");
            var meal = MealHandler.CreateMeal(name, sousChef.Id);
            Assert.That(meal, Is.Not.Null);
            Assert.That(meal.Name, Is.EqualTo(name));
            Assert.That(meal.Id, Is.GreaterThan(0));

            //Chef nav
            sousChef = SousChefHandler.GetSousChef(sousChef.Id);
            Assert.That(sousChef.Meals, Is.Not.Null);
            Assert.That(sousChef.Meals.Count, Is.EqualTo(1));
            Assert.That(sousChef.Meals.First().Id,Is.EqualTo(meal.Id));

            //Meal nav
            Assert.That(meal.SousChef, Is.Not.Null);
            Assert.That(meal.SousChef.Id, Is.EqualTo(sousChef.Id));
        }
        [Test]
        public void ShouldCreateMealWithRecipe()
        {
            string name = $"Name {Guid.NewGuid()}";
            var sousChef = SousChefHandler.CreateSousChef($"Name {Guid.NewGuid()}", "1@1.com", "password");
            var recipe = RecipeHandler.CreateRecipe($"name {Guid.NewGuid()}");
            var meal = MealHandler.CreateMeal(name, sousChef.Id, recipe.Id);
            Assert.That(meal, Is.Not.Null);
            Assert.That(meal.RecipeInstances.Count, Is.EqualTo(1));
        }
        [Test]
        public void ShouldCreateMealWithRecipies()
        {
            string name = $"Name {Guid.NewGuid()}";
            var sousChef = SousChefHandler.CreateSousChef($"Name {Guid.NewGuid()}", "1@1.com", "password");
            var recipe1 = RecipeHandler.CreateRecipe($"name {Guid.NewGuid()}");
            var recipe2 = RecipeHandler.CreateRecipe($"name {Guid.NewGuid()}");
            var meal = MealHandler.CreateMeal(name, sousChef.Id, recipe1.Id);
            Assert.That(meal, Is.Not.Null);
            meal = MealHandler.AddRecipe(meal.Id, recipe2.Id);
            Assert.That(meal.RecipeInstances.Count, Is.EqualTo(2));
        }
    }
}
