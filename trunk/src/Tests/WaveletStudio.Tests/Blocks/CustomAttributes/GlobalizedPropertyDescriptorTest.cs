using System;
using System.ComponentModel;
using System.Linq;
using System.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveletStudio.Blocks;
using WaveletStudio.Blocks.CustomAttributes;
using WaveletStudio.Properties;

namespace WaveletStudio.Tests.Blocks.CustomAttributes
{
    [TestClass]
    public class GlobalizedPropertyDescriptorTest
    {
        [TestMethod]
        public void CheckAllParameters()
        {
            var resource = new ResourceManager(typeof(Resources));                
            var types = Utils.GetTypes("WaveletStudio.Blocks");
            foreach (var type in types)
            {
                if(type.BaseType != typeof(BlockBase))
                    continue;
                var block = (BlockBase)Activator.CreateInstance(type);
                var properties = TypeDescriptor.GetProperties(block, new Attribute[] { }, true);
                foreach (PropertyDescriptor property in properties)
                {
                    var descriptor = new GlobalizedPropertyDescriptor(property);
                    var parameterAttribute = property.Attributes.Cast<Attribute>().FirstOrDefault(attribute => attribute.GetType() == typeof (Parameter)) as Parameter;
                    if(parameterAttribute == null)
                        continue;
                    if(!string.IsNullOrEmpty(parameterAttribute.NameResourceName))
                    {
                        Assert.IsTrue(!string.IsNullOrWhiteSpace(resource.GetString(parameterAttribute.NameResourceName)), type.FullName + "." + descriptor.Name + " - DisplayName resource not found: " + parameterAttribute.NameResourceName);
                        Assert.IsTrue(!string.IsNullOrWhiteSpace(resource.GetString(parameterAttribute.DescriptionResourceName)), type.FullName + "." + descriptor.Name + " - Description resource not found: " + parameterAttribute.DescriptionResourceName);
                    }
                    Assert.IsTrue(!string.IsNullOrEmpty(descriptor.DisplayName), type.FullName + "." + descriptor.Name + " - DisplayName resource not found");
                    Assert.IsTrue(!string.IsNullOrEmpty(descriptor.Description), type.FullName + "." + descriptor.Name + " - Description resource not found");
                    Assert.IsTrue(descriptor.Description.EndsWith("."), type.FullName + "." + descriptor.Name + " - Description resource not ends with '.'");
                }
            }
        }

        [TestMethod]
        public void TestGlobalizedPropertyDescriptor()
        {
            var block = new WaveletBlock{WaveletName = "sym2"};
            var baseProps = TypeDescriptor.GetProperties(block, new Attribute[] { }, true);
            var waveletNameProperty = baseProps.Cast<PropertyDescriptor>().FirstOrDefault(baseProp => baseProp.Name == "WaveletName");            
            var descriptor = new GlobalizedPropertyDescriptor(waveletNameProperty);
            Assert.AreEqual(false, descriptor.CanResetValue(block));
            Assert.AreEqual(block.GetType(), descriptor.ComponentType);
            Assert.AreEqual("sym2", descriptor.GetValue(block));
            Assert.AreEqual(Resources.WaveletName, descriptor.DisplayName);
            Assert.AreEqual(Resources.WaveletNameDescription, descriptor.Description);
            Assert.AreEqual(false, descriptor.IsReadOnly);
            Assert.AreEqual("WaveletName", descriptor.Name);
            Assert.AreEqual(typeof(string), descriptor.PropertyType);
            Assert.AreEqual(true, descriptor.ShouldSerializeValue(block));
            descriptor.SetValue(block, "db4");
            Assert.AreEqual("db4", descriptor.GetValue(block));
            descriptor.ResetValue(block);
            Assert.AreEqual("db4", descriptor.GetValue(block));
        }

        [TestMethod]
        public void TestGlobalizedPropertyDescriptorWithInvalidResource()
        {
            var obj = new GlobalizedObjectMock();
            var baseProps = TypeDescriptor.GetProperties(obj, new Attribute[] { }, true);
            var property = baseProps[0];
            var descriptor = new GlobalizedPropertyDescriptor(property);
            Assert.AreEqual("TestProperty", descriptor.DisplayName);
            Assert.AreEqual("", descriptor.Description);            
        }
    }
}
