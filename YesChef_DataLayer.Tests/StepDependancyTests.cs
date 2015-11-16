using System;
using System.Linq;
using NUnit.Framework;

namespace YesChef_DataLayer.Tests
{
    [TestFixture]
    public class StepDependancyTests
    {
        [Test]
        public void ShouldCreateStepDependancy()
        {
            var recipe = RecipeHandler.CreateRecipe($"recipeName {Guid.NewGuid()}");
            Assert.That(recipe, !Is.Null);
            var step1 = StepHandler.CreateStep($"step description {Guid.NewGuid()}", 1, recipe);
            Assert.That(step1, !Is.Null);
            var step2 = StepHandler.CreateStep($"step description {Guid.NewGuid()}", 1, recipe);
            Assert.That(step2, !Is.Null);

            var stepDependancy = StepDependancyHandler.CreateStepDependancy(parentStep: step1, dependantStep: step2);
            Assert.That(stepDependancy, !Is.Null);
            Assert.That(stepDependancy.Id, Is.GreaterThan(0));

            Assert.That(stepDependancy.ParentStep.Id, Is.EqualTo(step1.Id));
            Assert.That(stepDependancy.ChildStep.Id, Is.EqualTo(step2.Id));
        }

        [Test]
        public void ShouldCreateStepWithTwoDependancies()
        {
            var recipe = RecipeHandler.CreateRecipe($"recipeName {Guid.NewGuid()}");
            Assert.That(recipe, !Is.Null);
            var step1 = StepHandler.CreateStep($"step description {Guid.NewGuid()}", 1, recipe);
            Assert.That(step1, !Is.Null);
            var step2 = StepHandler.CreateStep($"step description {Guid.NewGuid()}", 1, recipe);
            Assert.That(step2, !Is.Null);
            var step3 = StepHandler.CreateStep($"step description {Guid.NewGuid()}", 1, recipe);
            Assert.That(step3, !Is.Null);

            var stepDependancy1 = StepDependancyHandler.CreateStepDependancy(parentStep: step1, dependantStep: step3);
            Assert.That(stepDependancy1, !Is.Null);
            var stepDependancy2 = StepDependancyHandler.CreateStepDependancy(parentStep: step2, dependantStep: step3);
            Assert.That(stepDependancy2, !Is.Null);

            Assert.That(stepDependancy1.ParentStep.Id, Is.EqualTo(step1.Id));
            Assert.That(stepDependancy1.ChildStep.Id, Is.EqualTo(step3.Id));
            Assert.That(stepDependancy2.ParentStep.Id, Is.EqualTo(step2.Id));
            Assert.That(stepDependancy2.ChildStep.Id, Is.EqualTo(step3.Id));

            step1 = StepHandler.GetStep(step1.Id);
            step3 = StepHandler.GetStep(step3.Id);
            Assert.That(step3.StepDependancies.Count, Is.EqualTo(2));
            Assert.That(step1.StepDependants.ToList()[0].ChildStepId, Is.EqualTo(step3.Id));
        }

        [Test]
        public void ShouldNotCreateStepDependancyASecondTime()
        {
            Assert.Fail();
        }
    }
}
