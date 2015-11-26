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
    public class SousChefTests
    {
        [Test]
        public void ShouldCreateSousChef()
        {
            string name = $"Name {Guid.NewGuid()}";
            string email = $"email {Guid.NewGuid()}";
            string plainTextPassword = $"plainTextPassword {Guid.NewGuid()}";
            var sousChef = SousChefHandler.CreateSousChef(name, email, plainTextPassword);
            Assert.That(sousChef, Is.Not.Null);
            Assert.That(sousChef.Id, Is.GreaterThan(0));
            Assert.That(sousChef.PasswordHash, Is.Not.EqualTo("spaceSaver"));
            Assert.That(plainTextPassword, Is.Not.EqualTo(sousChef.PasswordHash));

            //Confirm Password
            Assert.That(sousChef.PasswordHash, Is.EqualTo(EncryptionHandler.CreateHash(plainTextPassword, sousChef.Id)));
        }
    }
}
