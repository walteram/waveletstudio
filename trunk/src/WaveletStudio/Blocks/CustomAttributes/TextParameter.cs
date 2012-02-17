﻿/*  Wavelet Studio Signal Processing Library - www.waveletstudio.net
    Copyright (C) 2011, 2012 Walter V. S. de Amorim - The Wavelet Studio Initiative

    Wavelet Studio is free software: you can redistribute it and/or modify
    it under the terms of the GNU Lesser General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Wavelet Studio is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>. 
*/

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
