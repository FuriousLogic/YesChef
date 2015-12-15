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
    public class StepTests
    {
        [Test]
        public void ShouldGetStepByIdWithNav()
        {
            var recipe = RecipeHandler.CreateRecipe($"Name {Guid.NewGuid()}");
            var step = StepHandler.CreateStep($"description {new Guid()}", 1, recipe.Id);

            var stepFetched = StepHandler.GetStep(step.Id);
            Assert.That(stepFetched.Id, Is.EqualTo(step.Id));
            Assert.That(step.Recipe,Is.Not.Null);
            Assert.That(step.Recipe.Id, Is.EqualTo(recipe.Id));
        }
        [Test]
        public void ShouldCreateStep()
        {
            var description = $"description_{Guid.NewGuid()}";
            const int minutesDuration = 10;
            var recipeName = $"recipeName_{Guid.NewGuid()}";
            var recipe = RecipeHandler.CreateRecipe(recipeName);
            Assert.That(recipe, !Is.Null);

            var s = StepHandler.CreateStep(description, minutesDuration, recipe.Id);
            Assert.That(s, !Is.Null);
            Assert.That(s.Id, Is.GreaterThan(0));
            Assert.That(s.Description, Is.EqualTo(description));
            Assert.That(s.Recipe.Id, Is.EqualTo(recipe.Id));
        }

    }
}
