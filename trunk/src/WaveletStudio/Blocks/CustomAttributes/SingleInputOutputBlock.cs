using System;

namespace WaveletStudio.Blocks.CustomAttributes
{
    /// <summary>
    /// Defines a block that receives nothing or only one signal as input and returns only one signal as output
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class SingleInputOutputBlock : Attribute
    {
    }
}
