// <copyright file="PriorityQueueItemTTest.cs" company="SuprDewd">Copyright © SuprDewd 2010</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharpBag.Algorithms
{
    [TestClass]
    [PexClass(typeof(PriorityQueueItem<>))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class PriorityQueueItemTTest
    {
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public string ToString01<T>([PexAssumeUnderTest]PriorityQueueItem<T> target)
        {
            string result = target.ToString();
            return result;
            // TODO: add assertions to method PriorityQueueItemTTest.ToString01(PriorityQueueItem`1<!!0>)
        }

        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public PriorityQueueItem<T> Constructor<T>(T item, int priority)
        {
            PriorityQueueItem<T> target = new PriorityQueueItem<T>(item, priority);
            return target;
            // TODO: add assertions to method PriorityQueueItemTTest.Constructor(!!0, Int32)
        }
    }
}