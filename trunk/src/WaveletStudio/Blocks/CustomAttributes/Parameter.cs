using System;

namespace WaveletStudio.Blocks.CustomAttributes
{
    /// <summary>
    /// Block Parameter
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class Parameter : Attribute
    {
        /// <summary>
        /// Resource name of the name of the parameter.
        /// </summary>
        public string NameResourceName { get; set; }

        /// <summary>
        /// Resource name of the description of the parameter.
        /// </summary>
        public string DescriptionResourceName { get; set; }        

        /// <summary>
        /// Default constructor
        /// </summary>
        public Parameter()
        {
        }
        
        /// <summary>
        /// Constructor passing the resource names of the name and description of the parameter.
        /// </summary>
        public Parameter(string nameResourceName, string descriptionResourceName)
        {
            NameResourceName = nameResourceName;
            DescriptionResourceName = descriptionResourceName;
        }        
    }
}
