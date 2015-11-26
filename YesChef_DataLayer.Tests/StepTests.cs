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

        [Test]
        public void ShouldGetChildSteps()
        {
            var recipe = RecipeHandler.CreateRecipe($"Recipe name {Guid.NewGuid()}");
            var step = StepHandler.CreateStep($"step description {Guid.NewGuid()}", 0, recipe.Id);
            var childStep1 = StepHandler.CreateStep($"step description {Guid.NewGuid()}", 0, recipe.Id);
            var childStep2 = StepHandler.CreateStep($"step description {Guid.NewGuid()}", 0, recipe.Id);
            Assert.That(recipe, !Is.Null);
            Assert.That(step, !Is.Null);
            Assert.That(childStep1, !Is.Null);
            Assert.That(childStep2, !Is.Null);

            var stepDependancy1 = StepDependancyHandler.CreateStepDependancy(step.Id, childStep1.Id);
            var stepDependancy2 = StepDependancyHandler.CreateStepDependancy(step.Id, childStep2.Id);
            Assert.That(stepDependancy1, !Is.Null);
            Assert.That(stepDependancy2, !Is.Null);

            Assert.That(StepHandler.GetChildSteps(step.Id).Count, Is.EqualTo(2));
        }

        [Test]
        public void ShouldGetParentSteps()
        {
            var recipe = RecipeHandler.CreateRecipe($"Recipe name {Guid.NewGuid()}");
            var step = StepHandler.CreateStep($"step description {Guid.NewGuid()}", 0, recipe.Id);
            var parentStep1 = StepHandler.CreateStep($"step description {Guid.NewGuid()}", 0, recipe.Id);
            var parentStep2 = StepHandler.CreateStep($"step description {Guid.NewGuid()}", 0, recipe.Id);
            Assert.That(recipe, !Is.Null);
            Assert.That(step, !Is.Null);
            Assert.That(parentStep1, !Is.Null);
            Assert.That(parentStep2, !Is.Null);

            var stepDependancy1 = StepDependancyHandler.CreateStepDependancy(parentStep1.Id, step.Id);
            var stepDependancy2 = StepDependancyHandler.CreateStepDependancy(parentStep2.Id, step.Id);
            Assert.That(stepDependancy1, !Is.Null);
            Assert.That(stepDependancy2, !Is.Null);

            Assert.That(StepHandler.GetChildSteps(parentStep1.Id).Count, Is.EqualTo(1));
            Assert.That(StepHandler.GetChildSteps(parentStep2.Id).Count, Is.EqualTo(1));
            Assert.That(StepHandler.GetParentSteps(step.Id).Count, Is.EqualTo(2));
        }
    }
}
