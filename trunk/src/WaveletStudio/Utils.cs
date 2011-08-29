using System;
using System.Collections.Generic;
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
            var assemblies = Assembly.GetAssembly(typeof(Signal)).GetTypes().Where(it => it.Namespace == namespaceName && !it.IsAbstract && !it.IsInterface && !it.IsEnum && it.IsPublic);
            if (assemblies.Count() == 0)
            {
                assemblies = Assembly.GetAssembly(typeof(Signal)).GetTypes().Where(it => it.Namespace == namespaceName && !it.IsAbstract && !it.IsInterface && !it.IsEnum && it.IsPublic);
            }
            return assemblies;
        }

        /// <summary>
        /// Gets a type based on its fullname
        /// </summary>
        /// <param name="fullname"></param>
        /// <returns></returns>
        public static Type GetType(string fullname)
        {
            return Assembly.GetAssembly(typeof(Signal)).GetType(fullname) ??
                           Assembly.GetAssembly(typeof(Signal)).GetType(fullname);
        }
    }
}
