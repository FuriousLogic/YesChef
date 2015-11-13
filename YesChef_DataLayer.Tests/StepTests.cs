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
    public class StepTests
    {
        [Test]
        public void ShouldGetStepById()
        {
            var recipe = RecipeHandler.CreateRecipe($"Name {Guid.NewGuid()}");
            var step = StepHandler.CreateStep($"description {new Guid()}", 1, recipe);

            var stepFetched = StepHandler.GetStep(step.Id);
            Assert.That(stepFetched.Id, Is.EqualTo(step.Id));
        }
        [Test]
        public void ShouldCreateStep()
        {
            var description = string.Format("description_{0}", Guid.NewGuid().ToString());
            var minutesDuration = 10;
            var recipeName = string.Format("recipeName_{0}", Guid.NewGuid().ToString());
            Recipe recipe = RecipeHandler.CreateRecipe(recipeName);
            Assert.That(recipe, !Is.Null);

            var s = StepHandler.CreateStep(description, minutesDuration, recipe);
            Assert.That(s, !Is.Null);
            Assert.That(s.Id, Is.GreaterThan(0));
            Assert.That(s.Description, Is.EqualTo(description));
            Assert.That(s.Recipe.Id, Is.EqualTo(recipe.Id));
        }
    }
}
