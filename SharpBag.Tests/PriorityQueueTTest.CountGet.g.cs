using Microsoft.Pex.Framework.Generated;

// <copyright file="PriorityQueueTTest.CountGet.g.cs" company="SuprDewd">Copyright � SuprDewd 2010</copyright>
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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharpBag.Algorithms
{
    public partial class PriorityQueueTTest
    {
        [TestMethod]
        [PexGeneratedBy(typeof(PriorityQueueTTest))]
        public void CountGet1901()
        {
            PriorityQueue<int> priorityQueue;
            int i;
            priorityQueue = new PriorityQueue<int>();
            i = this.CountGet<int>(priorityQueue);
            Assert.AreEqual<int>(0, i);
            Assert.IsNotNull((object)priorityQueue);
        }
    }
}