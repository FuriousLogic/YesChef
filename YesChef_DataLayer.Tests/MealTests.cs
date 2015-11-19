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
        public void ShouldCreateMeal()
        {
            string name = $"Name {Guid.NewGuid()}";
            var sousChef=SousChefHandler.CreateSousChef($"Name {Guid.NewGuid()}", "1@1.com", "password");
            var meal = MealHandler.CreateMeal(name, sousChef);
            Assert.That(meal, Is.Not.Null);
            Assert.That(meal.Name,Is.EqualTo(name));
            Assert.That(meal.Id, Is.GreaterThan(0));
        }
    }
}
