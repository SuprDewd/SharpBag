// <copyright file="ExtensionsTest.cs" company="SuprDewd">Copyright © SuprDewd 2010</copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Windows.Threading;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpBag;

namespace SharpBag
{
    [TestClass]
    [PexClass(typeof(Extensions))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class ExtensionsTest
    {
        [PexGenericArguments(typeof(int), typeof(int))]
        [PexMethod]
        public void Add<TKey, TValue>(
            Dictionary<TKey, TValue> D,
            TKey key,
            TValue value,
            bool overwrite
        )
        {
            Extensions.Add<TKey, TValue>(D, key, value, overwrite);
            // TODO: add assertions to method ExtensionsTest.Add(Dictionary`2<!!0,!!1>, !!0, !!1, Boolean)
        }

        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public void With<T>(T obj, Action<T> act)
        {
            Extensions.With<T>(obj, act);
            // TODO: add assertions to method ExtensionsTest.With(!!0, Action`1<!!0>)
        }

        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public IEnumerable<T> UnionAll02<T>(IEnumerable<T> source, T[] newItems)
        {
            IEnumerable<T> result = Extensions.UnionAll<T>(source, newItems);
            return result;
            // TODO: add assertions to method ExtensionsTest.UnionAll02(IEnumerable`1<!!0>, !!0[])
        }

        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public IEnumerable<T> UnionAll01<T>(IEnumerable<T> source, T newItem)
        {
            IEnumerable<T> result = Extensions.UnionAll<T>(source, newItem);
            return result;
            // TODO: add assertions to method ExtensionsTest.UnionAll01(IEnumerable`1<!!0>, !!0)
        }

        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public IEnumerable<T> UnionAll<T>(IEnumerable<T> source, IEnumerable<T> other)
        {
            IEnumerable<T> result = Extensions.UnionAll<T>(source, other);
            return result;
            // TODO: add assertions to method ExtensionsTest.UnionAll(IEnumerable`1<!!0>, IEnumerable`1<!!0>)
        }

        [PexMethod]
        public Dictionary<string, object> ToDictionary(object o)
        {
            Dictionary<string, object> result = Extensions.ToDictionary(o);
            return result;
            // TODO: add assertions to method ExtensionsTest.ToDictionary(Object)
        }

        [PexMethod]
        public IEnumerable<char> To04(
            char start,
            char end,
            int step
        )
        {
            IEnumerable<char> result = Extensions.To(start, end, step);
            return result;
            // TODO: add assertions to method ExtensionsTest.To04(Char, Char, Int32)
        }

#if DOTNET4
        [PexMethod]
        public IEnumerable<BigInteger> To03(
            BigInteger start,
            BigInteger end,
            BigInteger step
        )
        {
            IEnumerable<BigInteger> result = Extensions.To(start, end, step);
            return result;
            // TODO: add assertions to method ExtensionsTest.To03(BigInteger, BigInteger, BigInteger)
        }

        [PexMethod]
        public IEnumerable<BigInteger> To02(BigInteger start, BigInteger end)
        {
            IEnumerable<BigInteger> result = Extensions.To(start, end);
            return result;
            // TODO: add assertions to method ExtensionsTest.To02(BigInteger, BigInteger)
        }
#endif

        [PexMethod]
        public IEnumerable<long> To01(
            long start,
            long end,
            long step
        )
        {
            IEnumerable<long> result = Extensions.To(start, end, step);
            return result;
            // TODO: add assertions to method ExtensionsTest.To01(Int64, Int64, Int64)
        }

        [PexMethod]
        public IEnumerable<int> To(
            int start,
            int end,
            int step
        )
        {
            IEnumerable<int> result = Extensions.To(start, end, step);
            return result;
            // TODO: add assertions to method ExtensionsTest.To(Int32, Int32, Int32)
        }

        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public IEnumerable<T> Times<T>(int i, Func<T> f)
        {
            IEnumerable<T> result = Extensions.Times<T>(i, f);
            return result;
            // TODO: add assertions to method ExtensionsTest.Times(Int32, Func`1<!!0>)
        }

        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public IEnumerable<T> TakeEvery<T>(
            IEnumerable<T> enumeration,
            int step,
            int start
        )
        {
            IEnumerable<T> result = Extensions.TakeEvery<T>(enumeration, step, start);
            return result;
            // TODO: add assertions to method ExtensionsTest.TakeEvery(IEnumerable`1<!!0>, Int32, Int32)
        }

        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public IEnumerable<T> Shuffle<T>(IEnumerable<T> source, Random rand)
        {
            IEnumerable<T> result = Extensions.Shuffle<T>(source, rand);
            return result;
            // TODO: add assertions to method ExtensionsTest.Shuffle(IEnumerable`1<!!0>, Random)
        }

        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public IEnumerable<T> Range<T>(
            IEnumerable<T> array,
            int start,
            int end
        )
        {
            IEnumerable<T> result = Extensions.Range<T>(array, start, end);
            return result;
            // TODO: add assertions to method ExtensionsTest.Range(IEnumerable`1<!!0>, Int32, Int32)
        }

        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public T Random01<T>(IEnumerable<T> collection, Random rand)
        {
            T result = Extensions.Random<T>(collection, rand);
            return result;
            // TODO: add assertions to method ExtensionsTest.Random01(IEnumerable`1<!!0>, Random)
        }

        [PexGenericArguments(typeof(int))]
        [PexMethod, PexAllowedException("SharpBag", "System.Diagnostics.Contracts.__ContractsRuntime+ContractException")]
        public T Random<T>(IEnumerable<T> collection)
        {
            T result = Extensions.Random<T>(collection);
            return result;
            // TODO: add assertions to method ExtensionsTest.Random(IEnumerable`1<!!0>)
        }

        [PexGenericArguments(typeof(object), typeof(int))]
        [PexMethod]
        public TReturn NullOr<TIn, TReturn>(
            TIn obj,
            Func<TIn, TReturn> selector,
            TReturn elseValue
        )
            where TIn : class
        {
            TReturn result = Extensions.NullOr<TIn, TReturn>(obj, selector, elseValue);
            return result;
            // TODO: add assertions to method ExtensionsTest.NullOr(!!0, Func`2<!!0,!!1>, !!1)
        }

        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public IEnumerable<T> Iter<T>(IEnumerable<T> source, Action<T> action)
        {
            IEnumerable<T> result = Extensions.Iter<T>(source, action);
            return result;
            // TODO: add assertions to method ExtensionsTest.Iter(IEnumerable`1<!!0>, Action`1<!!0>)
        }

        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public bool IsNullOrEmpty<T>(IEnumerable<T> collection)
        {
            bool result = Extensions.IsNullOrEmpty<T>(collection);
            return result;
            // TODO: add assertions to method ExtensionsTest.IsNullOrEmpty(IEnumerable`1<!!0>)
        }

        [PexMethod]
        public bool IsNullableType(Type type)
        {
            bool result = Extensions.IsNullableType(type);
            return result;
            // TODO: add assertions to method ExtensionsTest.IsNullableType(Type)
        }

        [PexGenericArguments(typeof(object))]
        [PexMethod]
        public bool IsNot<T>(object item)
            where T : class
        {
            bool result = Extensions.IsNot<T>(item);
            return result;
            // TODO: add assertions to method ExtensionsTest.IsNot(Object)
        }

        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public bool IsIn01<T>(T item, T[] collection)
        {
            bool result = Extensions.IsIn<T>(item, collection);
            return result;
            // TODO: add assertions to method ExtensionsTest.IsIn01(!!0, !!0[])
        }

        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public bool IsIn<T>(T item, IEnumerable<T> collection)
        {
            bool result = Extensions.IsIn<T>(item, collection);
            return result;
            // TODO: add assertions to method ExtensionsTest.IsIn(!!0, IEnumerable`1<!!0>)
        }

        [PexGenericArguments(typeof(object))]
        [PexMethod]
        public bool Is<T>(object item)
            where T : class
        {
            bool result = Extensions.Is<T>(item);
            return result;
            // TODO: add assertions to method ExtensionsTest.Is(Object)
        }

        [PexMethod]
        public void InvokeIfRequired01(
            DispatcherObject control,
            Action methodcall,
            DispatcherPriority priorityForCall
        )
        {
            Extensions.InvokeIfRequired(control, methodcall, priorityForCall);
            // TODO: add assertions to method ExtensionsTest.InvokeIfRequired01(DispatcherObject, Action, DispatcherPriority)
        }

        [PexMethod]
        public void InvokeIfRequired(DispatcherObject control, Action methodcall)
        {
            Extensions.InvokeIfRequired(control, methodcall);
            // TODO: add assertions to method ExtensionsTest.InvokeIfRequired(DispatcherObject, Action)
        }

        [PexGenericArguments(typeof(object))]
        [PexMethod]
        public void IfNotNull01<T>(T obj, Action action)
            where T : class
        {
            Extensions.IfNotNull<T>(obj, action);
            // TODO: add assertions to method ExtensionsTest.IfNotNull01(!!0, Action)
        }

        [PexGenericArguments(typeof(object))]
        [PexMethod]
        public void IfNotNull<T>(T obj, Action<T> action)
            where T : class
        {
            Extensions.IfNotNull<T>(obj, action);
            // TODO: add assertions to method ExtensionsTest.IfNotNull(!!0, Action`1<!!0>)
        }

        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public T If<T>(
            T obj,
            bool expression,
            T def
        )
        {
            T result = Extensions.If<T>(obj, expression, def);
            return result;
            // TODO: add assertions to method ExtensionsTest.If(!!0, Boolean, !!0)
        }

        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public T GetByPercent<T>(T[] array, double percent)
        {
            T result = Extensions.GetByPercent<T>(array, percent);
            return result;
            // TODO: add assertions to method ExtensionsTest.GetByPercent(!!0[], Double)
        }

        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public void ForEach<T>(IEnumerable<T> source, Action<T> action)
        {
            Extensions.ForEach<T>(source, action);
            // TODO: add assertions to method ExtensionsTest.ForEach(IEnumerable`1<!!0>, Action`1<!!0>)
        }

        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public void Fill01<T>(List<T> array, T value)
        {
            Extensions.Fill<T>(array, value);
            // TODO: add assertions to method ExtensionsTest.Fill01(List`1<!!0>, !!0)
        }

        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public void Fill<T>(T[] array, T value)
        {
            Extensions.Fill<T>(array, value);
            // TODO: add assertions to method ExtensionsTest.Fill(!!0[], !!0)
        }

        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public void Execute<T>(IEnumerable<T> sequence)
        {
            Extensions.Execute<T>(sequence);
            // TODO: add assertions to method ExtensionsTest.Execute(IEnumerable`1<!!0>)
        }

        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public IEnumerable<T> EmptyIfNull<T>(IEnumerable<T> pSeq)
        {
            IEnumerable<T> result = Extensions.EmptyIfNull<T>(pSeq);
            return result;
            // TODO: add assertions to method ExtensionsTest.EmptyIfNull(IEnumerable`1<!!0>)
        }

        [PexMethod]
        public bool ContainsArray(Array a, object o)
        {
            bool result = Extensions.ContainsArray(a, o);
            return result;
            // TODO: add assertions to method ExtensionsTest.ContainsArray(Array, Object)
        }

        [PexGenericArguments(typeof(object))]
        [PexMethod]
        public T CastAs<T>(object item)
            where T : class
        {
            T result = Extensions.CastAs<T>(item);
            return result;
            // TODO: add assertions to method ExtensionsTest.CastAs(Object)
        }

        [PexMethod]
        public IEnumerable<object> AsEnumerable03(IEnumerator e)
        {
            IEnumerable<object> result = Extensions.AsEnumerable(e);
            return result;
            // TODO: add assertions to method ExtensionsTest.AsEnumerable03(IEnumerator)
        }

        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public IEnumerable<T> AsEnumerable02<T>(IEnumerator<T> e)
        {
            IEnumerable<T> result = Extensions.AsEnumerable<T>(e);
            return result;
            // TODO: add assertions to method ExtensionsTest.AsEnumerable02(IEnumerator`1<!!0>)
        }

        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public IEnumerable<T> AsEnumerable01<T>(T[, ,] multiDArray)
        {
            IEnumerable<T> result = Extensions.AsEnumerable<T>(multiDArray);
            return result;
            // TODO: add assertions to method ExtensionsTest.AsEnumerable01(!!0[,,])
        }

        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public IEnumerable<T> AsEnumerable<T>(T[,] multiDArray)
        {
            IEnumerable<T> result = Extensions.AsEnumerable<T>(multiDArray);
            return result;
            // TODO: add assertions to method ExtensionsTest.AsEnumerable(!!0[,])
        }

        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public TOut As01<TOut>(
            object original,
            IFormatProvider provider,
            TOut defaultValue
        )
        {
            TOut result = Extensions.As<TOut>(original, provider, defaultValue);
            return result;
            // TODO: add assertions to method ExtensionsTest.As01(Object, IFormatProvider, !!0)
        }

        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public TOut As<TOut>(object original, TOut defaultValue)
        {
            TOut result = Extensions.As<TOut>(original, defaultValue);
            return result;
            // TODO: add assertions to method ExtensionsTest.As(Object, !!0)
        }

        [PexMethod]
        public int[,] Subarray(
            int[,] a,
            int x1,
            int y1,
            int x2,
            int y2
        )
        {
            int[,] result = Extensions.Subarray(a, x1, y1, x2, y2);
            return result;
            // TODO: add assertions to method ExtensionsTest.Subarray(Int32[,], Int32, Int32, Int32, Int32)
        }

        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public IEnumerable<T> Take01<T>(IEnumerable<T> collection, int[] indexes)
        {
            IEnumerable<T> result = Extensions.Take<T>(collection, indexes);
            return result;
            // TODO: add assertions to method ExtensionsTest.Take01(IEnumerable`1<!!0>, Int32[])
        }
    }
}