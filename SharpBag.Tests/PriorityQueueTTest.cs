// <copyright file="PriorityQueueTTest.cs" company="SuprDewd">Copyright © SuprDewd 2010</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpBag.Algorithms;
using System.Collections.Generic;

namespace SharpBag.Algorithms
{
    [TestClass]
    [PexClass(typeof(PriorityQueue<>))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class PriorityQueueTTest
    {
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public T Peek<T>([PexAssumeUnderTest]PriorityQueue<T> target)
        {
            T result = target.Peek();
            return result;
            // TODO: add assertions to method PriorityQueueTTest.Peek(PriorityQueue`1<!!0>)
        }
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public void ItemSet<T>(
            [PexAssumeUnderTest]PriorityQueue<T> target,
            int index,
            T value
        )
        {
            target[index] = value;
            // TODO: add assertions to method PriorityQueueTTest.ItemSet(PriorityQueue`1<!!0>, Int32, !!0)
        }
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public T ItemGet<T>([PexAssumeUnderTest]PriorityQueue<T> target, int index)
        {
            T result = target[index];
            return result;
            // TODO: add assertions to method PriorityQueueTTest.ItemGet(PriorityQueue`1<!!0>, Int32)
        }
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public IEnumerable<T> GetPriorityEnumerator<T>([PexAssumeUnderTest]PriorityQueue<T> target)
        {
            IEnumerable<T> result = target.GetPriorityEnumerator();
            return result;
            // TODO: add assertions to method PriorityQueueTTest.GetPriorityEnumerator(PriorityQueue`1<!!0>)
        }
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public IEnumerator<T> GetEnumerator01<T>([PexAssumeUnderTest]PriorityQueue<T> target, bool remove)
        {
            IEnumerator<T> result = target.GetEnumerator(remove);
            return result;
            // TODO: add assertions to method PriorityQueueTTest.GetEnumerator01(PriorityQueue`1<!!0>, Boolean)
        }
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public IEnumerator<T> GetEnumerator<T>([PexAssumeUnderTest]PriorityQueue<T> target)
        {
            IEnumerator<T> result = target.GetEnumerator();
            return result;
            // TODO: add assertions to method PriorityQueueTTest.GetEnumerator(PriorityQueue`1<!!0>)
        }
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public void Enqueue<T>(
            [PexAssumeUnderTest]PriorityQueue<T> target,
            T item,
            int priority
        )
        {
            target.Enqueue(item, priority);
            // TODO: add assertions to method PriorityQueueTTest.Enqueue(PriorityQueue`1<!!0>, !!0, Int32)
        }
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public T Dequeue<T>([PexAssumeUnderTest]PriorityQueue<T> target)
        {
            T result = target.Dequeue();
            return result;
            // TODO: add assertions to method PriorityQueueTTest.Dequeue(PriorityQueue`1<!!0>)
        }
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public int CountGet<T>([PexAssumeUnderTest]PriorityQueue<T> target)
        {
            int result = target.Count;
            return result;
            // TODO: add assertions to method PriorityQueueTTest.CountGet(PriorityQueue`1<!!0>)
        }
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public PriorityQueue<T> Constructor<T>()
        {
            PriorityQueue<T> target = new PriorityQueue<T>();
            return target;
            // TODO: add assertions to method PriorityQueueTTest.Constructor()
        }
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public void Add<T>(
            [PexAssumeUnderTest]PriorityQueue<T> target,
            T item,
            int priority
        )
        {
            target.Add(item, priority);
            // TODO: add assertions to method PriorityQueueTTest.Add(PriorityQueue`1<!!0>, !!0, Int32)
        }
    }
}
