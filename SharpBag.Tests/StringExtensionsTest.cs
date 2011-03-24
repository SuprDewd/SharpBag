using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpBag.Strings;

namespace SharpBag.Tests
{
    [TestClass]
    public class StringExtensionsTest
    {
        [TestMethod]
        public void SetCharAt()
        {
            string s = "ABC";
            s = s.SetCharAt(0, 'C');
            Assert.AreEqual("CBC", s);
            s = s.SetCharAt(1, 'G');
            Assert.AreEqual("CGC", s);
            s = s.SetCharAt(2, 'K');
            Assert.AreEqual("CGK", s);
        }
    }
}