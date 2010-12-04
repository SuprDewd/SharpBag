// <copyright file="UtilsTest.cs" company="SuprDewd">Copyright © SuprDewd 2010</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpBag;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace SharpBag
{
    [TestClass]
    [PexClass(typeof(Utils))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class UtilsTest
    {
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public IEnumerable<T> Single<T>(T value)
        {
            IEnumerable<T> result = Utils.Single<T>(value);
            return result;
            // TODO: add assertions to method UtilsTest.Single(!!0)
        }
        [PexMethod]
        public IEnumerable<string> ReadLinesFromFile(string path)
        {
            IEnumerable<string> result = Utils.ReadLinesFromFile(path);
            return result;
            // TODO: add assertions to method UtilsTest.ReadLinesFromFile(String)
        }
        [PexMethod]
        public IEnumerable<string> ReadLinesFromConsole()
        {
            IEnumerable<string> result = Utils.ReadLinesFromConsole();
            return result;
            // TODO: add assertions to method UtilsTest.ReadLinesFromConsole()
        }
        [PexMethod]
        public IEnumerable<string> ReadLinesFrom(TextReader reader)
        {
            IEnumerable<string> result = Utils.ReadLinesFrom(reader);
            return result;
            // TODO: add assertions to method UtilsTest.ReadLinesFrom(TextReader)
        }
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public IEnumerable<T> GenerateEndless<T>(Func<T> generator)
        {
            IEnumerable<T> result = Utils.GenerateEndless<T>(generator);
            return result;
            // TODO: add assertions to method UtilsTest.GenerateEndless(Func`1<!!0>)
        }
        [PexGenericArguments(typeof(object))]
        [PexMethod]
        public IEnumerable<T> Generate<T>(Func<T> generator)
            where T : class
        {
            IEnumerable<T> result = Utils.Generate<T>(generator);
            return result;
            // TODO: add assertions to method UtilsTest.Generate(Func`1<!!0>)
        }
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public IEnumerable<T> FromEnumerator<T>(IEnumerator<T> enumerator)
        {
            IEnumerable<T> result = Utils.FromEnumerator<T>(enumerator);
            return result;
            // TODO: add assertions to method UtilsTest.FromEnumerator(IEnumerator`1<!!0>)
        }
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public TResult ExecutionTime01<TResult>(
            Action a,
            Func<Stopwatch, TResult> result,
            bool handleGC
        )
        {
            TResult result01 = Utils.ExecutionTime<TResult>(a, result, handleGC);
            return result01;
            // TODO: add assertions to method UtilsTest.ExecutionTime01(Action, Func`2<Stopwatch,!!0>, Boolean)
        }
        [PexMethod]
        public long ExecutionTime(Action a, bool handleGC)
        {
            long result = Utils.ExecutionTime(a, handleGC);
            return result;
            // TODO: add assertions to method UtilsTest.ExecutionTime(Action, Boolean)
        }
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public IEnumerable<T> CreateIEnumerable<T>(T[] objects)
        {
            IEnumerable<T> result = Utils.CreateIEnumerable<T>(objects);
            return result;
            // TODO: add assertions to method UtilsTest.CreateIEnumerable(!!0[])
        }
    }
}
