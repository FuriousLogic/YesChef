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
        //[Test]
        //[Ignore]
        //public void ShouldCreateMeal()
        //{
        //    string name = $"Name {Guid.NewGuid()}";
        //    var sousChef = SousChefHandler.CreateSousChef($"Name {Guid.NewGuid()}", "1@1.com", "password");
        //    var meal = MealHandler.CreateMeal(name, sousChef);
        //    Assert.That(meal, Is.Not.Null);
        //    Assert.That(meal.Name, Is.EqualTo(name));
        //    Assert.That(meal.Id, Is.GreaterThan(0));
        //}
        //[Test]
        //[Ignore]
        //public void ShouldCreateMealWithRecipe()
        //{
        //    string name = $"Name {Guid.NewGuid()}";
        //    var sousChef = SousChefHandler.CreateSousChef($"Name {Guid.NewGuid()}", "1@1.com", "password");
        //    var recipe=RecipeHandler.CreateRecipe($"name {Guid.NewGuid()}");
        //    var meal = MealHandler.CreateMeal(name, sousChef, recipe);
        //    Assert.That(meal, Is.Not.Null);
        //    Assert.That(meal.RecipeInstances.Count, Is.EqualTo(1));
        //}
        //[Test]
        //[Ignore]
        //public void ShouldCreateMealWithRecipies()
        //{
        //    string name = $"Name {Guid.NewGuid()}";
        //    var sousChef = SousChefHandler.CreateSousChef($"Name {Guid.NewGuid()}", "1@1.com", "password");
        //    var recipe1 = RecipeHandler.CreateRecipe($"name {Guid.NewGuid()}");
        //    var recipe2 = RecipeHandler.CreateRecipe($"name {Guid.NewGuid()}");
        //    var meal = MealHandler.CreateMeal(name, sousChef, recipe1);
        //    Assert.That(meal, Is.Not.Null);
        //    meal = MealHandler.AddRecipe(meal, recipe2);
        //    Assert.That(meal.RecipeInstances.Count, Is.EqualTo(2));
        //}
    }
}
