// <copyright file="MathExtensionsTest.cs" company="SuprDewd">Copyright © SuprDewd 2010</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpBag.Math;

namespace SharpBag.Math
{
    [TestClass]
    [PexClass(typeof(MathExtensions))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class MathExtensionsTest
    {
        [PexMethod]
        public long CollatzCount01(long n)
        {
            long result = MathExtensions.CollatzCount(n);
            return result;
            // TODO: add assertions to method MathExtensionsTest.CollatzCount01(Int64)
        }
        [PexMethod]
        public int CollatzCount(int n)
        {
            int result = MathExtensions.CollatzCount(n);
            return result;
            // TODO: add assertions to method MathExtensionsTest.CollatzCount(Int32)
        }
        [PexMethod]
        public double Bound01(
            double d,
            double lower,
            double upper
        )
        {
            double result = MathExtensions.Bound(d, lower, upper);
            return result;
            // TODO: add assertions to method MathExtensionsTest.Bound01(Double, Double, Double)
        }
        [PexMethod]
        public int Bound(
            int d,
            int lower,
            int upper
        )
        {
            int result = MathExtensions.Bound(d, lower, upper);
            return result;
            // TODO: add assertions to method MathExtensionsTest.Bound(Int32, Int32, Int32)
        }
    }
}
