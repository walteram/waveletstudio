using System;
using System.ComponentModel;
using WaveletStudio.Blocks.CustomAttributes;

namespace WaveletStudio.Tests.Blocks.CustomAttributes
{
    public class PropertyDescriptorMock : PropertyDescriptor
    {
        public PropertyDescriptorMock(string name, Attribute[] attrs)
            : base(name, attrs)
        {
        }

        public PropertyDescriptorMock(MemberDescriptor descr)
            : base(descr)
        {
        }

        public PropertyDescriptorMock(MemberDescriptor descr, Attribute[] attrs)
            : base(descr, attrs)
        {
        }

        public override bool CanResetValue(object component)
        {
            return true;
        }

        public override object GetValue(object component)
        {
            return true;
        }

        public override void ResetValue(object component)
        {

        }

        public override void SetValue(object component, object value)
        {

        }

        public override bool ShouldSerializeValue(object component)
        {
            return true;
        }

        public override Type ComponentType
        {
            get { return null; }
        }

        public override bool IsReadOnly
        {
            get { return true; }
        }

        public override Type PropertyType
        {
            get { return null; }
        }
    }

    public class TypeDescriptorContextMock : ITypeDescriptorContext
    {
        public object GetService(Type serviceType)
        {
            return null;
        }

        public bool OnComponentChanging()
        {
            return false;
        }

        public void OnComponentChanged()
        {
        }

        public IContainer Container
        {
            get { return null; }
        }

        public object Instance
        {
            get { return null; }
        }

        public PropertyDescriptor PropertyDescriptor
        {
            get { return null; }
        }
    }

    public class GlobalizedObjectMock : GlobalizedObject
    {
        [Parameter("Nononono", "Nononono")]
        public string TestProperty { get; set; }
    }
}
