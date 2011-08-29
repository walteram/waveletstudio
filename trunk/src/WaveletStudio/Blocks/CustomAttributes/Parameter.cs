using System;

namespace WaveletStudio.Blocks.CustomAttributes
{
    /// <summary>
    /// Parameter
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class Parameter : Attribute
    {
        /// <summary>
        /// Name of the parameter
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the paramater</param>
        public Parameter(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Parameter()
        {
            
        }
    }
}
