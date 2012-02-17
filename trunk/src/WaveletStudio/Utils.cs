/*  Wavelet Studio Signal Processing Library - www.waveletstudio.net
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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace WaveletStudio
{
    /// <summary>
    /// Utils methods
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// Gets the types in a namespace
        /// </summary>
        /// <param name="namespaceName"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetTypes(string namespaceName)
        {
            return Assembly.GetAssembly(typeof(Signal)).GetTypes().Where(it => it.Namespace == namespaceName && !it.IsAbstract && !it.IsInterface && !it.IsEnum && it.IsPublic).OrderBy(it => it.FullName);
        }

        /// <summary>
        /// Gets a type based on its fullname
        /// </summary>
        /// <param name="fullname"></param>
        /// <returns></returns>
        public static Type GetType(string fullname)
        {
            return Assembly.GetAssembly(typeof(Signal)).GetType(fullname);
        }

        /// <summary>
        /// Get current directory
        /// </summary>
        public static string AssemblyDirectory
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}
