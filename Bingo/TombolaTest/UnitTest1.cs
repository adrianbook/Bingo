using System.Security.Cryptography.X509Certificates;
using TombolaProject;

namespace TombolaTest
{
    [TestClass]
    public class UnitTest1
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
    }
}