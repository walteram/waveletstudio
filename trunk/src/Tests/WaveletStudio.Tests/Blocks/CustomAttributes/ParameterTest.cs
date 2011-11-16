using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Blocks.CustomAttributes;

namespace WaveletStudio.Tests.Blocks.CustomAttributes
{
    [TestClass]
    public class ParameterTest
    {
        [TestMethod]
        public void TestParameter()
        {
            var parameter = new Parameter();
            Assert.IsNull(parameter.NameResourceName);
            Assert.IsNull(parameter.DescriptionResourceName);

            parameter = new Parameter("test1", "test2");
            Assert.AreEqual("test1", parameter.NameResourceName);
            Assert.AreEqual("test2", parameter.DescriptionResourceName);
        }
    }
}
