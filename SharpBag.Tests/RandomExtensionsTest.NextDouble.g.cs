// <copyright file="RandomExtensionsTest.NextDouble.g.cs" company="SuprDewd">Copyright � SuprDewd 2010</copyright>
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
using Microsoft.Pex.Engine.Exceptions;
using Microsoft.Pex.Framework.Generated;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharpBag
{
    public partial class RandomExtensionsTest
    {
        [TestMethod]
        [PexGeneratedBy(typeof(RandomExtensionsTest))]
        [PexRaisedContractException(PexExceptionState.Expected)]
        public void NextDoubleThrowsContractException907()
        {
            try
            {
                double d;
                d = this.NextDouble((Random)null, 0, 0);
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