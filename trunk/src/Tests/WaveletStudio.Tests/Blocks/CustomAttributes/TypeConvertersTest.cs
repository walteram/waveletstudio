using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Blocks.CustomAttributes;
using WaveletStudio.Wavelet;

namespace WaveletStudio.Tests.Blocks.CustomAttributes
{
    [TestClass]
    public class TypeConvertersTest
    {
        [TestMethod]
        public void TestWaveletNamesTypeConverter()
        {
            var converter = new WaveletNamesTypeConverter();
            var standardValues = converter.GetStandardValues(new TypeDescriptorContextMock());
            Assert.IsNotNull(standardValues);
            Assert.AreEqual(CommonMotherWavelets.Wavelets.Values.Count, standardValues.Count);
            Assert.AreEqual("Coiflet 1 (coif1)", standardValues[0]);
        }

        [TestMethod]
        public void TestTemplateNamesTypeConverter()
        {
            var converter = new TemplateNamesTypeConverter();
            var standardValues = converter.GetStandardValues(new TypeDescriptorContextMock());
            Assert.IsNotNull(standardValues);
            Assert.AreEqual(Utils.GetTypes("WaveletStudio.SignalGeneration").Count(), standardValues.Count);
            Assert.IsNotNull(Utils.GetType("WaveletStudio.SignalGeneration." + standardValues[0]));
        }
        
        [TestMethod]
        public void TestListTypeConverterConverter()
        {
            var converter = new ListTypeConverter();
            Assert.IsTrue(converter.GetStandardValuesSupported(new TypeDescriptorContextMock()));
            Assert.IsTrue(converter.GetStandardValuesExclusive(new TypeDescriptorContextMock()));
            Assert.IsNotNull(converter.GetStandardValues(new TypeDescriptorContextMock()));
        }
    }
}
