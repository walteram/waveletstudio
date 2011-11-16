using System.Collections.Generic;
using System.ComponentModel;

namespace WaveletStudio.Blocks.CustomAttributes
{
    /// <summary>
    /// Base type converter
    /// </summary>
    public class ListTypeConverter : TypeConverter
    {
        /// <summary>
        /// Returns wether standard values are supported
        /// </summary>
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// Returns wether standard values are exclusive
        /// </summary>
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// Returns the standard values
        /// </summary>
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new List<string>());
        }
    }
}