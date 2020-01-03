using NUnit.Framework;
using SixtySix.Framework.Models;
using System;
using System.Collections.Generic;

namespace SixtySix.Framework.UnitTests.ModelsTests.CardTests
{
    [TestFixture]
    class CompareToShould
    {
        [Test]
        public void ReturnNegativeValue_WhenComparingToLargerValidCard()
        {
            Card smaller = new Card("K", "hearts", 3);
            Card larger = new Card("A", "hearts", 5);

            Assert.IsTrue(smaller.CompareTo(larger) < 0);
        }  
        
        [Test]
        public void ReturnPositiveValue_WhenComparingToSmallerValidCard()
        {
            Card smaller = new Card("9", "hearts", 0);
            Card larger = new Card("10", "spades", 4);

            Assert.IsTrue(larger.CompareTo(smaller) > 0);
        }
        
        [Test]
        public void ReturnZero_WhenComparingToEqualValidCard()
        {
            Card card1 = new Card("J", "hearts", 1);
            Card card2 = new Card("J", "spades", 1);

            Assert.IsTrue(card1.CompareTo(card2) == 0);
        }

        [Test]
        public void ThrowArgumentNullException_WhenComparedToNullObject()
        {
            Card card = new Card("9", "clubs", 0);

            Assert.Throws<ArgumentNullException>(() => card.CompareTo(null));
        }

        [Test]
        public void ThrowArgumentException_WhenComparedToObjectThatIsNotCard()
        {
            Card card = new Card("9", "clubs", 0);
            List<int> notCard = new List<int>();

            Assert.Throws<ArgumentException>(() => card.CompareTo(notCard));
        }
    }
}
