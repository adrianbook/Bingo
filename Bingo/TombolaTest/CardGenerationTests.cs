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
    }
}
