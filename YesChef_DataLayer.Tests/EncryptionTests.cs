using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace YesChef_DataLayer.Tests
{
    [TestFixture]
    public class EncryptionTests
    {
        [Test]
        public void ShouldGenerateAndCheckHash()
        {
            string plainText = $"password{Guid.NewGuid()}";
            const int saltValue = 1;
            var hash = EncryptionHandler.CreateHash(plainText, saltValue);
            Assert.That(hash,Is.Not.EqualTo(plainText));

            Assert.That(hash, Is.EqualTo(EncryptionHandler.CreateHash(plainText, saltValue)));
        }

        [Test]
        public void ShouldCreateDifferentHashesWithDifferentSaltValues()
        {
            string plainText = $"password{Guid.NewGuid()}";
            const int saltValue = 1;
            var hash = EncryptionHandler.CreateHash(plainText, saltValue);
            Assert.That(hash, Is.Not.EqualTo(plainText));

            Assert.That(hash, Is.Not.EqualTo(EncryptionHandler.CreateHash(plainText, 2)));
        }

        [Test]
        public void ShouldCreateDifferentHashesWithDifferentCasePasswords()
        {
            var guid = Guid.NewGuid();
            string plainText = $"password{guid}";
            const int saltValue = 1;
            var hash = EncryptionHandler.CreateHash(plainText, saltValue);
            Assert.That(hash, Is.Not.EqualTo(plainText));

            Assert.That(hash, Is.Not.EqualTo(EncryptionHandler.CreateHash($"Password{guid}", saltValue)));
        }
    }
}
