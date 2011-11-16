using System.ComponentModel;
using System.Linq;

namespace WaveletStudio.Blocks.CustomAttributes
{
    public class TemplateNamesTypeConverter : ListTypeConverter
    {
        private StandardValuesCollection GetValues()
        {
            return new StandardValuesCollection(Utils.GetTypes("WaveletStudio.SignalGeneration").Select(it => it.Name).ToArray());
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return GetValues();
        }
    }
}