using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaveletStudio.MainApplication.Attributes
{
    public class DisplayAttribute : Attribute
    {
        public string DisplayText { get; set; }

        public DisplayAttribute(string displayText)
        {
            DisplayText = displayText;
        }
    }
}
