// <copyright file="RandomExtensionsTest.cs" company="SuprDewd">Copyright © SuprDewd 2010</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpBag;

namespace SharpBag
{
    [TestClass]
    [PexClass(typeof(RandomExtensions))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class RandomExtensionsTest
    {
        [PexMethod]
        public TimeSpan NextTimeSpan01(Random random)
        {
            TimeSpan result = RandomExtensions.NextTimeSpan(random);
            return result;
            // TODO: add assertions to method RandomExtensionsTest.NextTimeSpan01(Random)
        }
        [PexMethod]
        public TimeSpan NextTimeSpan(
            Random random,
            TimeSpan minValue,
            TimeSpan maxValue
        )
        {
            TimeSpan result = RandomExtensions.NextTimeSpan(random, minValue, maxValue);
            return result;
            // TODO: add assertions to method RandomExtensionsTest.NextTimeSpan(Random, TimeSpan, TimeSpan)
        }
        [PexMethod]
        public string NextString(
            Random random,
            int numChars,
            CharType mode
        )
        {
            string result = RandomExtensions.NextString(random, numChars, mode);
            return result;
            // TODO: add assertions to method RandomExtensionsTest.NextString(Random, Int32, CharType)
        }
        [PexMethod]
        public double NextDouble(
            Random random,
            double minValue,
            double maxValue
        )
        {
            double result = RandomExtensions.NextDouble(random, minValue, maxValue);
            return result;
            // TODO: add assertions to method RandomExtensionsTest.NextDouble(Random, Double, Double)
        }
        [PexMethod]
        public DateTime NextDateTime01(Random random)
        {
            DateTime result = RandomExtensions.NextDateTime(random);
            return result;
            // TODO: add assertions to method RandomExtensionsTest.NextDateTime01(Random)
        }
        [PexMethod]
        public DateTime NextDateTime(
            Random random,
            DateTime minValue,
            DateTime maxValue
        )
        {
            DateTime result = RandomExtensions.NextDateTime(random, minValue, maxValue);
            return result;
            // TODO: add assertions to method RandomExtensionsTest.NextDateTime(Random, DateTime, DateTime)
        }
        [PexMethod]
        public char NextChar01(Random random)
        {
            char result = RandomExtensions.NextChar(random);
            return result;
            // TODO: add assertions to method RandomExtensionsTest.NextChar01(Random)
        }
        [PexMethod]
        public char NextChar(Random random, CharType mode)
        {
            char result = RandomExtensions.NextChar(random, mode);
            return result;
            // TODO: add assertions to method RandomExtensionsTest.NextChar(Random, CharType)
        }
        [PexMethod]
        public bool NextBool(Random random, double probability)
        {
            bool result = RandomExtensions.NextBool(random, probability);
            return result;
            // TODO: add assertions to method RandomExtensionsTest.NextBool(Random, Double)
        }
    }
}
