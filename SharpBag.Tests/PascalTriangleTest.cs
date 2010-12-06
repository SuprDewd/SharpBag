// <copyright file="PascalTriangleTest.cs" company="SuprDewd">Copyright © SuprDewd 2010</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpBag.Math;

namespace SharpBag.Math
{
    [TestClass]
    [PexClass(typeof(PascalTriangle))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class PascalTriangleTest
    {
        [PexMethod]
        public long GetEntry(int row, int column)
        {
            long result = PascalTriangle.GetEntry(row, column);
            return result;
            // TODO: add assertions to method PascalTriangleTest.GetEntry(Int32, Int32)
        }
    }
}
