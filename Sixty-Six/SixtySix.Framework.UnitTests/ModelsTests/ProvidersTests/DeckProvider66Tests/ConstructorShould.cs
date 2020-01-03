using Moq;
using NUnit.Framework;
using SixtySix.Crontracts;
using SixtySix.Framework.Providers;
using System;

namespace SixtySix.Framework.UnitTests.ModelsTests.ProvidersTests.DeckProvider66Tests
{
    [TestFixture]
    public class ConstructorShould
    {
        [Test]
        public void ThrowArgumentNullException_WhenCardFactoryPassedIsNull()
        {
            ICardFactory factory = null;

            Assert.Throws<ArgumentNullException>(() => new DeckProvider66(factory));
        }
    }
}
