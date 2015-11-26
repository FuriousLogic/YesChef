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
    public class QuantityTypeTests
    {
        [Test]
        public void ShouldGetQuantityType()
        {
            var qType = QuantityTypeHandler.CreateQuantityType($"name {Guid.NewGuid()}");
            var quantityType = QuantityTypeHandler.GetQuantityType(quantityTypeId: qType.Id);

            Assert.That(quantityType,Is.Not.Null);
            Assert.That(quantityType.Id,Is.EqualTo(qType.Id));
        }
        [Test]
        public void ShouldCreateNewQuantityType()
        {
            var db = new YesChefContext();
            var count = db.QuantityTypes.ToList().Count;

            var quantityTypeName = $"QuantityTypeName_{Guid.NewGuid()}";
            var qt = QuantityTypeHandler.CreateQuantityType(quantityTypeName);

            Assert.That(qt, !Is.Null);
            Assert.That(qt.Id, Is.GreaterThan(0));
            Assert.That(qt.Name, Is.EqualTo(quantityTypeName));
            Assert.That(db.QuantityTypes.ToList().Count, Is.GreaterThan(count));
        }
        [Test]
        public void ShouldGetAllQuantityTypes()
        {
            var quantityTypes = QuantityTypeHandler.GetAllQuantityTypes();
            Assert.That(quantityTypes, !Is.Null);

            QuantityTypeHandler.CreateQuantityType($"QuantityTypeName_{Guid.NewGuid()}");

            Assert.That(quantityTypes.Count, Is.LessThan(QuantityTypeHandler.GetAllQuantityTypes().Count));
        }
    }
}
