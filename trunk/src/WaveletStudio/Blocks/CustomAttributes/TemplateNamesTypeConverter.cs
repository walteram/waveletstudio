using System.ComponentModel;
using System.Linq;

namespace WaveletStudio.Blocks.CustomAttributes
{
    /// <summary>
    /// Template names type converter
    /// </summary>
    public class TemplateNamesTypeConverter : ListTypeConverter
    {
        /// <summary>
        /// Returns the available templates.
        /// </summary>
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(Utils.GetTypes("WaveletStudio.SignalGeneration").Select(it => it.Name).ToArray());
        }
    }
}