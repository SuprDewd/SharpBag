// <copyright file="MediaExtensionsTest.GetSubRectangles.g.cs" company="SuprDewd">Copyright � SuprDewd 2010</copyright>
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
using System.Drawing;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Pex.Framework.Generated;

namespace SharpBag.Media
{
    public partial class MediaExtensionsTest
    {
[TestMethod]
[PexGeneratedBy(typeof(MediaExtensionsTest))]
public void GetSubRectangles899()
{
    IEnumerable<Rectangle> iEnumerable;
    iEnumerable = this.GetSubRectangles(default(Rectangle), 0, 1);
    Assert.IsNotNull((object)iEnumerable);
}
[TestMethod]
[PexGeneratedBy(typeof(MediaExtensionsTest))]
public void GetSubRectangles179()
{
    IEnumerable<Rectangle> iEnumerable;
    iEnumerable = this.GetSubRectangles(default(Rectangle), 0, int.MinValue);
    Assert.IsNotNull((object)iEnumerable);
}
[TestMethod]
[PexGeneratedBy(typeof(MediaExtensionsTest))]
public void GetSubRectangles932()
{
    IEnumerable<Rectangle> iEnumerable;
    iEnumerable = this.GetSubRectangles(default(Rectangle), 0, 0);
    Assert.IsNotNull((object)iEnumerable);
}
    }
}
