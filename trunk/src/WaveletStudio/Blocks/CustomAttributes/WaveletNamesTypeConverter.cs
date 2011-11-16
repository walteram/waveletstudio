using System.ComponentModel;
using System.Linq;
using WaveletStudio.Wavelet;

namespace WaveletStudio.Blocks.CustomAttributes
{
    /// <summary>
    /// Wavelet names type converter
    /// </summary>
    public class WaveletNamesTypeConverter : ListTypeConverter
    {
        /// <summary>
        /// Returns the available wavelets.
        /// </summary>
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(CommonMotherWavelets.Wavelets.Values.Select(it => ((string)it[0]).Split('|')[1] + " (" + ((string)it[0]).Split('|')[0] + ")").ToList());
        }
    }
}