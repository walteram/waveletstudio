using System;
using System.ComponentModel;
using System.Resources;
using WaveletStudio.Properties;

namespace WaveletStudio.Blocks.CustomAttributes
{
    /// <summary>
    /// Gets the name and description of parameters from the resource file
    /// </summary>
    [Serializable]
    public class GlobalizedPropertyDescriptor : PropertyDescriptor
    {
        private readonly PropertyDescriptor _basePropertyDescriptor;
        private String _localizedName = "";
        private String _localizedDescription = "";

        /// <summary>
        /// Constructor
        /// </summary>
        public GlobalizedPropertyDescriptor(PropertyDescriptor basePropertyDescriptor) : base(basePropertyDescriptor)
        {
            _basePropertyDescriptor = basePropertyDescriptor;
        }

        /// <summary>
        /// Returns whether resetting an object changes its value.
        /// </summary>
        public override bool CanResetValue(object component)
        {
            return _basePropertyDescriptor.CanResetValue(component);
        }

        /// <summary>
        /// Gets the type of the component this property is bound to.
        /// </summary>
        public override Type ComponentType
        {
            get { return _basePropertyDescriptor.ComponentType; }
        }

        /// <summary>
        /// Gets the parameter display name
        /// </summary>
        public override string DisplayName
        {
            get
            {
                string displayName = null;
                foreach (Attribute attribute in _basePropertyDescriptor.Attributes)
                {
                    if (!attribute.GetType().Equals(typeof (Parameter))) continue;
                    displayName = ((Parameter)attribute).NameResourceName;
                    break;
                }
                if (displayName == null)
                    displayName = _basePropertyDescriptor.DisplayName;
                var resource = new ResourceManager(typeof(Resources));
                _localizedName = resource.GetString(displayName) ?? _basePropertyDescriptor.DisplayName;
                return _localizedName;
            }
        }

        /// <summary>
        /// Gets the parameter description
        /// </summary>
        public override string Description
        {
            get
            {
                string description = null;
                foreach (Attribute attribute in _basePropertyDescriptor.Attributes)
                {
                    if (attribute.GetType().Equals(typeof(Parameter)))
                    {
                        description = ((Parameter)attribute).DescriptionResourceName;
                    }
                }
                if (description == null)
                    description = _basePropertyDescriptor.DisplayName + "Description";
                var resource = new ResourceManager(typeof(Resources));
                _localizedDescription = resource.GetString(description) ?? _basePropertyDescriptor.Description;
                return _localizedDescription;
            }
        }

        /// <summary>
        /// Gets the current value of the property on a component.
        /// </summary>
        public override object GetValue(object component)
        {
            return _basePropertyDescriptor.GetValue(component);
        }

        /// <summary>
        /// Gets a value indicating whether this property is read-only.
        /// </summary>
        public override bool IsReadOnly
        {
            get { return _basePropertyDescriptor.IsReadOnly; }
        }

        /// <summary>
        /// Gets the name of the member.
        /// </summary>
        public override string Name
        {
            get { return _basePropertyDescriptor.Name; }
        }

        /// <summary>
        ///  Gets the type of the property.
        /// </summary>
        public override Type PropertyType
        {
            get { return _basePropertyDescriptor.PropertyType; }
        }

        /// <summary>
        /// Resets the value for this property of the component to the default value.
        /// </summary>
        public override void ResetValue(object component)
        {
            _basePropertyDescriptor.ResetValue(component);
        }

        /// <summary>
        /// Determines a value indicating whether the value of this property needs to be persisted.
        /// </summary>
        public override bool ShouldSerializeValue(object component)
        {
            return _basePropertyDescriptor.ShouldSerializeValue(component);
        }

        /// <summary>
        /// Sets the value of the component to a different value.
        /// </summary>
        public override void SetValue(object component, object value)
        {
            _basePropertyDescriptor.SetValue(component, value);
        }
    }
}
