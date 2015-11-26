using System;
using System.Linq;
using NUnit.Framework;

namespace YesChef_DataLayer.Tests
{
    [TestFixture]
    [Ignore]
    public class StepDependancyTests
    {
        //[Test]
        //public void ShouldCreateStepDependancy()
        //{
        //    var recipe = RecipeHandler.CreateRecipe($"recipeName {Guid.NewGuid()}");
        //    Assert.That(recipe, !Is.Null);
        //    var step1 = StepHandler.CreateStep($"step description {Guid.NewGuid()}", 1, recipe.Id);
        //    Assert.That(step1, !Is.Null);
        //    var step2 = StepHandler.CreateStep($"step description {Guid.NewGuid()}", 1, recipe.Id);
        //    Assert.That(step2, !Is.Null);

        //    var stepDependancy = StepDependancyHandler.CreateStepDependancy(step1.Id, step2.Id);
        //    Assert.That(stepDependancy, !Is.Null);
        //    Assert.That(stepDependancy.Id, Is.GreaterThan(0));

        //    Assert.That(stepDependancy.ParentStep.Id, Is.EqualTo(step1.Id));
        //    Assert.That(stepDependancy.ChildStep.Id, Is.EqualTo(step2.Id));
        //}

        //[Test]
        //public void ShouldNotCreateStepDependancyASecondTime()
        //{
        //    var recipe = RecipeHandler.CreateRecipe($"recipeName {Guid.NewGuid()}");
        //    Assert.That(recipe, !Is.Null);
        //    var step1 = StepHandler.CreateStep($"step description {Guid.NewGuid()}", 1, recipe.Id);
        //    Assert.That(step1, !Is.Null);
        //    var step2 = StepHandler.CreateStep($"step description {Guid.NewGuid()}", 1, recipe.Id);
        //    Assert.That(step2, !Is.Null);

        //    var stepDependancy = StepDependancyHandler.CreateStepDependancy(step1.Id, step2.Id);
        //    Assert.That(stepDependancy, !Is.Null);

        //    Assert.That(() => StepDependancyHandler.CreateStepDependancy(step1.Id, step2.Id), Throws.Exception);
        //}
    }
}
