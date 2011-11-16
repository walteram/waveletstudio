using System;
using System.ComponentModel;

namespace WaveletStudio.Blocks.CustomAttributes
{    
    /// <summary>
    /// Instantiates a property descriptor of type GlobalizedPropertyDescriptor.  
    /// </summary>
    [Serializable]
    public class GlobalizedObject : ICustomTypeDescriptor
    {
        [NonSerialized]
        private PropertyDescriptorCollection _globalizedProperties;

        /// <summary>
        /// Returns the name of the class
        /// </summary>
        public String GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        /// <summary>
        /// Returns a collection of attributes of this class
        /// </summary>
        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        /// <summary>
        /// Returns the component name of this class
        /// </summary>
        public String GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        /// <summary>
        /// Returns a type converter for the class
        /// </summary>
        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        /// <summary>
        /// Returns the default event for a component with a custom type descriptor.
        /// </summary>
        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        /// <summary>
        /// Returns the default property for the specified component with a custom type descriptor.
        /// </summary>
        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        /// <summary>
        /// Returns an editor with the specified base type and with a custom type descriptor for the specified component.
        /// </summary>
        /// <param name="editorBaseType"></param>
        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        /// <summary>
        /// Returns the collection of events for a specified component using a specified array of attributes as a filter and using a custom type descriptor.
        /// </summary>
        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        /// <summary>
        /// Returns the collection of events for a specified component with a custom type descriptor.
        /// </summary>
        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        /// <summary>
        /// Returns the properties of a type.
        /// </summary>
        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            if (_globalizedProperties == null)
            {
                var baseProps = TypeDescriptor.GetProperties(this, attributes, true);
                _globalizedProperties = new PropertyDescriptorCollection(null);
                foreach (PropertyDescriptor property in baseProps)
                {
                    foreach (var attribute in property.Attributes)
                    {
                        if (!(attribute is Parameter)) 
                            continue;
                        _globalizedProperties.Add(new GlobalizedPropertyDescriptor(property));
                        break;
                    }                    
                }
            }
            return _globalizedProperties;
        }

        /// <summary>
        /// Returns the properties of a type.
        /// </summary>
        public PropertyDescriptorCollection GetProperties()
        {
            if (_globalizedProperties == null)
            {
                var baseProps = TypeDescriptor.GetProperties(this, true);
                _globalizedProperties = new PropertyDescriptorCollection(null);
                foreach (PropertyDescriptor property in baseProps)
                {
                    foreach (var attribute in property.Attributes)
                    {
                        if (!(attribute is Parameter)) 
                            continue;
                        _globalizedProperties.Add(new GlobalizedPropertyDescriptor(property));
                        break;
                    }
                }
            }
            return _globalizedProperties;
        }

        /// <summary>
        /// Return the owner (this)
        /// </summary>
        /// <param name="pd"></param>
        /// <returns></returns>
        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }
    }
}
