// <copyright file="InternetTest.cs" company="SuprDewd">Copyright © SuprDewd 2010</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpBag.Net;

namespace SharpBag.Net
{
    [TestClass]
    [PexClass(typeof(Internet))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class InternetTest
    {
        [PexMethod]
        public bool IsPortFree(int port)
        {
            bool result = Internet.IsPortFree(port);
            return result;
            // TODO: add assertions to method InternetTest.IsPortFree(Int32)
        }
    }
}
