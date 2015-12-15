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
    public class StepRecipeDependancyTests
    {
        [Test]
        public void ShouldCreateStepRecipeDependancy()
        {
            //Create Recipe1
            var recipe1 = RecipeHandler.CreateRecipe($"Name {Guid.NewGuid()}");
            var recipeId = recipe1.Id;
            var step1 = StepHandler.CreateStep($"desc {Guid.NewGuid()}", 1, recipeId);
            var step2 = StepHandler.CreateStep($"desc {Guid.NewGuid()}", 1, recipeId);
            StepDependancyHandler.CreateStepDependancy(step1.Id, step2.Id);

            //Create Recipe2
            var recipe2 = RecipeHandler.CreateRecipe($"Name {Guid.NewGuid()}");
            var step3 = StepHandler.CreateStep($"desc {Guid.NewGuid()}", 1, recipe2.Id);
            var step4 = StepHandler.CreateStep($"desc {Guid.NewGuid()}", 1, recipe2.Id);
            var stepId = step3.Id;
            StepDependancyHandler.CreateStepDependancy(stepId, step4.Id);

            //Make a step3 dependant on recipe1
            var stepRecipeDependancy = StepRecipeDependancyHandler.CreateStepRecipeDependancy(stepId, recipeId);
            Assert.That(stepRecipeDependancy, Is.Not.Null);
            Assert.That(stepRecipeDependancy.Id, Is.GreaterThan(0));
            Assert.That(stepRecipeDependancy.DependantStep, Is.Not.Null);
            Assert.That(stepRecipeDependancy.Recipe, Is.Not.Null);
            Assert.That(stepRecipeDependancy.Recipe.Id, Is.EqualTo(recipeId));
            Assert.That(stepRecipeDependancy.DependantStep.Id, Is.EqualTo(stepId));
            Assert.That(recipe1.Id, Is.EqualTo(recipeId));
            Assert.That(step3.Id, Is.EqualTo(stepId));
        }
    }
}
