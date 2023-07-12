using System.Security.Cryptography.X509Certificates;
using Accessories;

namespace TombolaTest
{
    [TestClass]
    public class TombolaTests
    {
        [TestMethod]
        public void TestNumberProduced()
        {
            Tombola t = new Tombola();
            int num = t.NextNumber();

            Assert.IsTrue(num < 76);
            Assert.IsTrue(num > 0);
        }

        [TestMethod]
        public void TestNumbersNotInSequence()
        {
            Tombola t = new Tombola();
            for (int i = 75; i > 60; i--)
            {
                int n = t.NextNumber();
                Console.WriteLine(n);
                if (n != i) break;
                if (i == 61) Assert.Fail();
            }
        }

        [TestMethod]
        public void TestReturns0WhenExactlyEmpty()
        {
            Tombola t = new();
            for (int i = 0; i < 74; i++)
            {
                t.NextNumber();
            }
            Assert.AreNotEqual(0, t.NextNumber());
            Assert.AreEqual(0, t.NextNumber());
            Assert.AreEqual(0, t.NextNumber());
        }

        [TestMethod]
        public void TestNumbersAreUnique()
        {
            Tombola t = new();
            var testNumbers = new HashSet<int>();
            for (int i = 0; i < 76; i++)
            {
                Assert.IsTrue(testNumbers.Add(t.NextNumber()));
            }
        }

        [TestMethod]
        public void TestDrawnNumbersReturnedInSequence()
        {
            Tombola t = new();
            int previous = 0;
            bool currentSmallerThanPrevious = false;
            for (int i = 0; i < 67; i++)
            {
                int ball = t.NextNumber();
                if (ball < previous) currentSmallerThanPrevious = true;
                previous= ball;
            }
            Assert.IsTrue(currentSmallerThanPrevious);
            currentSmallerThanPrevious= false;
            previous= 0;
            foreach (int number in t.GetDrawnNumbers())
            {
                if (number < previous) currentSmallerThanPrevious = true;
                previous= number;
            }
            Assert.IsFalse(currentSmallerThanPrevious);
        }
    }
}