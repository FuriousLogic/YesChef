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
    public class QuantityTypeTests
    {
        [Test]
        public void ShouldCreateNewQuantityType()
        {
            var db = new YesChefContext();
            var count = db.QuantityTypes.ToList().Count;

            var quantityTypeName =string.Format("QuantityTypeName_{0}", Guid.NewGuid().ToString());
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

            var qt = QuantityTypeHandler.CreateQuantityType(string.Format("QuantityTypeName_{0}", Guid.NewGuid().ToString()));

            Assert.That(quantityTypes.Count, Is.LessThan(QuantityTypeHandler.GetAllQuantityTypes().Count));
        }
    }
}
