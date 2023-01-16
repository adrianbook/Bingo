using Accessories.BingoCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AccessoriesTest
{
    [TestClass]
    public class CardGenerationTests
    {
        [TestMethod]
        public void TestCardSerialization()
        {
            var fact = new CardFactory();
            var c = fact.MakeCard();
            string serial = JsonSerializer.Serialize(c);
            Console.WriteLine(serial);
        }

        [TestMethod]
        public void TestRowNumbersInRange() {
            var factory = new CardFactory();
            var cards = factory.MakeCards(1000);
            foreach (Card card in cards)
            {
                foreach (var row in card.Rows)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        int lowBound = i*15 + 1;
                        int highBound = i*15 + 15;
                        if (row[i] < lowBound || row[i] > highBound)
                        {
                            Assert.Fail();
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void TestIdsConsistent() {
            var factory = new CardFactory();
            var cards = factory.MakeCards(1000);
            foreach (Card card in cards)
            {
                Assert.AreEqual(50, card.Id.Length);
            }
        }
    }
}
