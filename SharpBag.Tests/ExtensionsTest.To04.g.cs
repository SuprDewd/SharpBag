// <copyright file="ExtensionsTest.To04.g.cs" company="SuprDewd">Copyright � SuprDewd 2010</copyright>
// <auto-generated>
// This file contains automatically generated unit tests.
// Do NOT modify this file manually.
//
// When Pex is invoked again,
// it might remove or update any previously generated unit tests.
//
// If the contents of this file becomes outdated, e.g. if it does not
// compile anymore, you may delete this file and invoke Pex again.
// </auto-generated>
using System;
using System.Collections.Generic;
using Microsoft.Pex.Engine.Exceptions;
using Microsoft.Pex.Framework.Generated;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharpBag
{
    public partial class ExtensionsTest
    {
        [TestMethod]
        [PexGeneratedBy(typeof(ExtensionsTest))]
        public void To0459801()
        {
            IEnumerable<char> iEnumerable;
            iEnumerable = this.To04(' ', ' ', 1);
            Assert.IsNotNull((object)iEnumerable);
        }

        [TestMethod]
        [PexGeneratedBy(typeof(ExtensionsTest))]
        public void To0482501()
        {
            IEnumerable<char> iEnumerable;
            iEnumerable = this.To04(' ', '!', 1);
            Assert.IsNotNull((object)iEnumerable);
        }

        [TestMethod]
        [PexGeneratedBy(typeof(ExtensionsTest))]
        public void To0488601()
        {
            IEnumerable<char> iEnumerable;
            iEnumerable = this.To04('P', '!', 1);
            Assert.IsNotNull((object)iEnumerable);
        }

        [TestMethod]
        [PexGeneratedBy(typeof(ExtensionsTest))]
        [PexRaisedContractException(PexExceptionState.Expected)]
        public void To04ThrowsContractException193()
        {
            try
            {
                IEnumerable<char> iEnumerable;
                iEnumerable = this.To04(' ', ' ', 0);
                throw
                  new AssertFailedException("expected an exception of type ContractException");
            }
            catch (Exception ex)
            {
                if (!PexContract.IsContractException(ex))
                    throw ex;
            }
        }
    }
}