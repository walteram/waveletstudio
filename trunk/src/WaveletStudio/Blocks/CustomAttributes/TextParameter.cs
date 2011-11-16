using System;

namespace WaveletStudio.Blocks.CustomAttributes
{
    /// <summary>
    /// Parameter
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class TextParameter : Parameter
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public TextParameter()
        {
        }
        
        /// <summary>
        /// Constructor passing the resource names of the name and description of the parameter.
        /// </summary>
        public TextParameter(string nameResourceName, string descriptionResourceName) : base(nameResourceName, descriptionResourceName)
        {            
        }
    }
}
