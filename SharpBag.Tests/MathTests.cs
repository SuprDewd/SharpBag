using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpBag.Math;
using SharpBag.Strings;

namespace SharpBag.Tests
{
    [TestClass]
    public class MathTests
    {
        [TestMethod]
        public void ProperDivisors()
        {
            Assert.AreEqual(new int[] { }.ToStringPretty(), 1.ProperDivisors().ToStringPretty());
            Assert.AreEqual(new int[] { 1 }.ToStringPretty(), 5.ProperDivisors().ToStringPretty());
            Assert.AreEqual(new int[] { 1, 2 }.ToStringPretty(), 4.ProperDivisors().OrderBy(i => i).ToStringPretty());
            Assert.AreEqual(new int[] { 1, 2, 5 }.ToStringPretty(), 10.ProperDivisors().OrderBy(i => i).ToStringPretty());
            Assert.AreEqual(new int[] { 1, 2, 4, 5, 10, 20, 25, 50 }.ToStringPretty(), 100.ProperDivisors().OrderBy(i => i).ToStringPretty());
        }

        [TestMethod]
        public void ProperDivisors64()
        {
            Assert.AreEqual(new long[] { }.ToStringPretty(), 1L.ProperDivisors().ToStringPretty());
            Assert.AreEqual(new long[] { 1 }.ToStringPretty(), 5L.ProperDivisors().ToStringPretty());
            Assert.AreEqual(new long[] { 1, 2 }.ToStringPretty(), 4L.ProperDivisors().OrderBy(i => i).ToStringPretty());
            Assert.AreEqual(new long[] { 1, 2, 5 }.ToStringPretty(), 10L.ProperDivisors().OrderBy(i => i).ToStringPretty());
            Assert.AreEqual(new long[] { 1, 2, 4, 5, 10, 20, 25, 50 }.ToStringPretty(), 100L.ProperDivisors().OrderBy(i => i).ToStringPretty());
        }

        [TestMethod]
        public void ProperDivisorsBig()
        {
            Assert.AreEqual(new BigInteger[] { }.ToStringPretty(), new BigInteger(1).ProperDivisors().ToStringPretty());
            Assert.AreEqual(new BigInteger[] { 1 }.ToStringPretty(), new BigInteger(5).ProperDivisors().ToStringPretty());
            Assert.AreEqual(new BigInteger[] { 1, 2 }.ToStringPretty(), new BigInteger(4).ProperDivisors().OrderBy(i => i).ToStringPretty());
            Assert.AreEqual(new BigInteger[] { 1, 2, 5 }.ToStringPretty(), new BigInteger(10).ProperDivisors().OrderBy(i => i).ToStringPretty());
            Assert.AreEqual(new BigInteger[] { 1, 2, 4, 5, 10, 20, 25, 50 }.ToStringPretty(), new BigInteger(100).ProperDivisors().OrderBy(i => i).ToStringPretty());
        }

        [TestMethod]
        public void Divisors()
        {
            Assert.AreEqual(new int[] { 1 }.ToStringPretty(), 1.Divisors().ToStringPretty());
            Assert.AreEqual(new int[] { 1, 5 }.ToStringPretty(), 5.Divisors().ToStringPretty());
            Assert.AreEqual(new int[] { 1, 2, 4 }.ToStringPretty(), 4.Divisors().OrderBy(i => i).ToStringPretty());
            Assert.AreEqual(new int[] { 1, 2, 5, 10 }.ToStringPretty(), 10.Divisors().OrderBy(i => i).ToStringPretty());
            Assert.AreEqual(new int[] { 1, 2, 4, 5, 10, 20, 25, 50, 100 }.ToStringPretty(), 100.Divisors().OrderBy(i => i).ToStringPretty());
        }

        [TestMethod]
        public void Divisors64()
        {
            Assert.AreEqual(new long[] { 1 }.ToStringPretty(), 1L.Divisors().ToStringPretty());
            Assert.AreEqual(new long[] { 1, 5 }.ToStringPretty(), 5L.Divisors().ToStringPretty());
            Assert.AreEqual(new long[] { 1, 2, 4 }.ToStringPretty(), 4L.Divisors().OrderBy(i => i).ToStringPretty());
            Assert.AreEqual(new long[] { 1, 2, 5, 10 }.ToStringPretty(), 10L.Divisors().OrderBy(i => i).ToStringPretty());
            Assert.AreEqual(new long[] { 1, 2, 4, 5, 10, 20, 25, 50, 100 }.ToStringPretty(), 100L.Divisors().OrderBy(i => i).ToStringPretty());
        }

        [TestMethod]
        public void DivisorsBig()
        {
            Assert.AreEqual(new BigInteger[] { 1 }.ToStringPretty(), new BigInteger(1).Divisors().ToStringPretty());
            Assert.AreEqual(new BigInteger[] { 1, 5 }.ToStringPretty(), new BigInteger(5).Divisors().ToStringPretty());
            Assert.AreEqual(new BigInteger[] { 1, 2, 4 }.ToStringPretty(), new BigInteger(4).Divisors().OrderBy(i => i).ToStringPretty());
            Assert.AreEqual(new BigInteger[] { 1, 2, 5, 10 }.ToStringPretty(), new BigInteger(10).Divisors().OrderBy(i => i).ToStringPretty());
            Assert.AreEqual(new BigInteger[] { 1, 2, 4, 5, 10, 20, 25, 50, 100 }.ToStringPretty(), new BigInteger(100).Divisors().OrderBy(i => i).ToStringPretty());
        }
    }
}