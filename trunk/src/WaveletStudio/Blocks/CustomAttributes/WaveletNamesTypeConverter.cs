using System.ComponentModel;
using System.Linq;
using WaveletStudio.Wavelet;

namespace WaveletStudio.Blocks.CustomAttributes
{
    public class WaveletNamesTypeConverter : ListTypeConverter
    {
        private StandardValuesCollection GetValues()
        {
            return new StandardValuesCollection(CommonMotherWavelets.Wavelets.Values.Select(it => ((string)it[0]).Split('|')[1] + " (" + ((string)it[0]).Split('|')[0] + ")").ToList());
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return GetValues();
        }
    }
}