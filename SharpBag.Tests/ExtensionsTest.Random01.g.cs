// <copyright file="ExtensionsTest.Random01.g.cs" company="SuprDewd">Copyright � SuprDewd 2010</copyright>
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
        [PexRaisedContractException(PexExceptionState.Expected)]
        public void Random01ThrowsContractException824()
        {
            try
            {
                int i;
                i = this.Random01<int>((IEnumerable<int>)null, (Random)null);
                throw
                  new AssertFailedException("expected an exception of type ContractException");
            }
            catch (Exception ex)
            {
                if (!PexContract.IsContractException(ex))
                    throw ex;
            }
        }
[TestMethod]
[PexGeneratedBy(typeof(ExtensionsTest))]
[PexRaisedContractException(PexExceptionState.Expected)]
public void Random01ThrowsContractException859()
{
    try
    {
      int i;
      int[] ints = new int[0];
      i = this.Random01<int>((IEnumerable<int>)ints, (Random)null);
      throw 
        new AssertFailedException("expected an exception of type ContractException");
    }
    catch(Exception ex)
    {
      if (!PexContract.IsContractException(ex))
        throw ex;
    }
}
[TestMethod]
[PexGeneratedBy(typeof(ExtensionsTest))]
public void Random57501()
{
    int i;
    int[] ints = new int[0];
    i = this.Random<int>((IEnumerable<int>)ints);
    Assert.AreEqual<int>(0, i);
}
[TestMethod]
[PexGeneratedBy(typeof(ExtensionsTest))]
public void Random431()
{
    int i;
    int[] ints = new int[1];
    i = this.Random<int>((IEnumerable<int>)ints);
    Assert.AreEqual<int>(0, i);
}
[TestMethod]
[PexGeneratedBy(typeof(ExtensionsTest))]
[PexRaisedContractException(PexExceptionState.Expected)]
public void RandomThrowsContractException872()
{
    try
    {
      int i;
      i = this.Random<int>((IEnumerable<int>)null);
      throw 
        new AssertFailedException("expected an exception of type ContractException");
    }
    catch(Exception ex)
    {
      if (!PexContract.IsContractException(ex))
        throw ex;
    }
}
    }
}