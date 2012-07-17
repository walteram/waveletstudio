/*  Wavelet Studio Signal Processing Library - www.waveletstudio.net
    Copyright (C) 2011, 2012 Walter V. S. de Amorim - The Wavelet Studio Initiative

    Wavelet Studio is free software: you can redistribute it and/or modify
    it under the terms of the GNU Lesser General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Wavelet Studio is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>. 
*/

using System;
using System.ComponentModel;
using System.Linq;
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
        public bool CausesRefresh { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public GlobalizedPropertyDescriptor(PropertyDescriptor basePropertyDescriptor) : base(basePropertyDescriptor)
        {
            _basePropertyDescriptor = basePropertyDescriptor;
            if (_basePropertyDescriptor!= null)
            {
                CausesRefresh = (from Attribute attribute in _basePropertyDescriptor.Attributes where (attribute.GetType() == typeof(Parameter)) select ((Parameter)attribute).CausesRefresh).FirstOrDefault();
            }
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
                var displayName = (from Attribute attribute in _basePropertyDescriptor.Attributes where (attribute.GetType() == typeof (Parameter)) select ((Parameter) attribute).NameResourceName).FirstOrDefault() ??
                                     _basePropertyDescriptor.DisplayName;
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
                foreach (var attribute in _basePropertyDescriptor.Attributes.Cast<Attribute>().Where(attribute => attribute.GetType() == typeof(Parameter)))
                {
                    description = ((Parameter)attribute).DescriptionResourceName;
                }
                if (description == null)
                {
                    description = _basePropertyDescriptor.DisplayName + "Description";
                }
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
