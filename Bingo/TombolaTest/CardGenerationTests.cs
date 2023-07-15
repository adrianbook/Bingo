using Accessories.BingoCard;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public void TestArrays()
        {
            var o = new int[3];
            Assert.AreEqual(0, o[2]);
        }


        [TestMethod]
        public void TestCardSize()
        {
            var fact = new CardDataFactory();
            Assert.AreEqual(25, fact.MakeCard().Numbers.Count());
        }

        [TestMethod]
        public void TestRowNumbersInRange()
        {
            var fails = new List<string>();
            var factory = new CardDataFactory();
            var cards = factory.MakeCards(1000);
            foreach (CardData card in cards)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = i*5; j < (i+1)*5; j++)
                    {
                        int lowBound = (i*15) + 1;
                        int highBound = (i*15) + 15;
                        if (card.Numbers[j] < lowBound || card.Numbers[j] > highBound)
                        {
                            Debug.WriteLine("{0} {1} low{2} high{3}", i, card.Numbers[j], lowBound, highBound);
                            fails.Add($"{j} {card.Numbers[j]}");
                        }
                    }
                }
            }
            Assert.IsFalse(fails.Any());
        }

        [TestMethod]
        public void TestRowNumbersNotInSequence()
        {
            var entriesNotInSequence = 0;
            var factory = new CardDataFactory();
            var cards = factory.MakeCards(10);

            foreach (CardData card in cards)
            {
                for (int i = 0; i < 25; i++)
                {
                    if (i % 5 != 0 && card.Numbers[i] > card.Numbers[i - 1])
                    {
                        entriesNotInSequence++;
                    }
                }
            }
            Debug.WriteLine(entriesNotInSequence);
            Assert.IsTrue(entriesNotInSequence > 0);
        }

        [TestMethod]
        public void TestIdsConsistent() {
            var factory = new CardDataFactory();
            var cards = factory.MakeCards(1000);
            foreach (CardData card in cards)
            {
                Assert.AreEqual(50, card.Id.Length);
            }
        }

        [TestMethod]
        public void TestRowsFetchedCorrectly()
        {
            var card = new CardData(new List<int> { 1, 2, 3, 4, 5,
                                                1, 2, 3, 4, 5,
                                                1, 2, 3, 4, 5,
                                                1, 2, 3, 4, 5,
                                                1, 2, 3, 4, 5 });
                                                           
            var row1 = card.GetRow(0).Select(i => i.ToString()).Aggregate((p, c) => p + ", " + c);
            var row2 = card.GetRow(1).Select(i => i.ToString()).Aggregate((p, c) => p + ", " + c);
            var row3 = card.GetRow(2).Select(i => i.ToString()).Aggregate((p, c) => p + ", " + c);
            var row4 = card.GetRow(3).Select(i => i.ToString()).Aggregate((p, c) => p + ", " + c);
            
            Assert.AreEqual("1, 1, 1, 1, 1", row1);
            Assert.AreEqual("2, 2, 2, 2, 2", row2);
            Assert.AreEqual("3, 3, 3, 3, 3", row3);
            Assert.AreEqual("4, 4, 4, 4, 4", row4);
        }

        [TestMethod]
        public void TestColumnsFetchedCorrectly()
        {
            var card = new CardData(new List<int> { 1, 1, 1, 1, 1,
                                                2, 2, 2, 2, 2,
                                                3, 3, 3, 3, 3,
                                                4, 4, 4, 4, 4,
                                                5, 5, 5, 5, 5 });

            var column1 = card.GetColumn(0).Select(i => i.ToString()).Aggregate((p, c) => p + ", " + c);
            var column2 = card.GetColumn(1).Select(i => i.ToString()).Aggregate((p, c) => p + ", " + c);
            var column3 = card.GetColumn(2).Select(i => i.ToString()).Aggregate((p, c) => p + ", " + c);
            var column4 = card.GetColumn(3).Select(i => i.ToString()).Aggregate((p, c) => p + ", " + c);
            var column5 = card.GetColumn(4).Select(i => i.ToString()).Aggregate((p, c) => p + ", " + c);

            Assert.AreEqual("1, 1, 1, 1, 1", column1);
            Assert.AreEqual("2, 2, 2, 2, 2", column2);
            Assert.AreEqual("3, 3, 3, 3, 3", column3);
            Assert.AreEqual("4, 4, 4, 4, 4", column4);
            Assert.AreEqual("5, 5, 5, 5, 5", column5);
        }
    }
}
