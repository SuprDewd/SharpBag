using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpBag.Games;
using SharpBag.Math;

namespace SharpBag.Tests
{
    /// <summary>
    ///This is a test class for DiceTest and is intended
    ///to contain all DiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DiceTest
    {
        /// <summary>
        ///A test for Throw
        ///</summary>
        [TestMethod()]
        public void ThrowTest()
        {
            const int sides = 6;
            Random r = new Random();

            for (int i = 0; i < 10000; i++) Assert.IsTrue(this.InitializeDice(sides, r).Throw().IsBetweenOrEqualTo(1, sides));
        }

        public Dice InitializeDice(int sides, Random r)
        {
            return new Dice(r, sides);
        }
    }
}