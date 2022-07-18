using LibraryNetwork;
using LibraryNetwork.Classes;
using NUnit.Framework;
using System;

namespace LibraryNetworkTests
{
    internal class NullObjectValidatorTest
    {
        private Patent GetPatent()
        {
            Patent patent = new Patent(3, "Patent", 100, new DateTime(2002, 1, 1), 111, "Rx", "USA", 33, new DateTime(2002, 1, 1), "312");

            return patent;
        }

        [Test]
        public void IsValid_StandartObject_WorkAsExcpected()
        {
            Patent patent = GetPatent();
            NullObjectValidator validator = new NullObjectValidator();

            var valid = validator.IsValid(patent, out var validationResults);

            Assert.AreEqual(true, valid);
        }

        [Test]
        public void IsValid_NullableObject_WorkAsExcpected()
        {
            Patent patent = null;
            NullObjectValidator validator = new NullObjectValidator();

            var valid = validator.IsValid(patent, out var validationResults);

            Assert.AreEqual(false, valid);
        }
    }
}
