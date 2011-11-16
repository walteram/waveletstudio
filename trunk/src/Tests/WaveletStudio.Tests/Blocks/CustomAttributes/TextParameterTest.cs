using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Blocks.CustomAttributes;

namespace WaveletStudio.Tests.Blocks.CustomAttributes
{
    [TestClass]
    public class TextParameterTest
    {
        [TestMethod]
        public void TestTextParameter()
        {
            var parameter = new TextParameter();
            Assert.IsNull(parameter.NameResourceName);
            Assert.IsNull(parameter.DescriptionResourceName);

            parameter = new TextParameter("test1", "test2");
            Assert.AreEqual("test1", parameter.NameResourceName);
            Assert.AreEqual("test2", parameter.DescriptionResourceName);
        }
    }
}
