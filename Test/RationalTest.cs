using System;
using System.Diagnostics;
using Math.Rational;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class RationalTest
    {
        [TestMethod]
        public void DummyTest()
        {
            Console.WriteLine("Rational Test Start");
        }

        [TestMethod]
        public void PrintTest()
        {
            int from = -4;
            int to = 7;
            for (int i = from; i <= to; i++)
            {
                for (int j = from ; j <= to; j++)
                {
                    Console.WriteLine("({0}, {1}) : {2}", i, j, new rational(i, j));
                }
            }
        }

        [TestMethod]
        public void EqualityTest()
        {
            // Zeros
            rational[] zeros =
            {
                new rational(0), 
                new rational(0, 1), 
                new rational(0, -1), 
                new rational(0, 2), 
                new rational(0, 3), 
                new rational(0, 30), 
                new rational(0, -511), 
            };
            foreach (rational t in zeros)
            {
                foreach (rational s in zeros)
                {
                    Assert.AreEqual(t, s);
                }
            }

            // Reducibles
            int range = 57;
            for (int i = -range; i <= range; i++)
            {
                for (int j = -range; j <= range; j++)
                {
                    rational a = new rational(i, j);
                    for (int k = -range; k <= range; k++)
                    {
                        if (k * j == 0) continue;

                        rational b = new rational(i * k, j * k);

                        Assert.AreEqual(a, b);
                    }
                }
            }
        }
    }
}
