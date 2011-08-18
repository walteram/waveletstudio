using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WaveletStudio
{
    public static class Utils
    {
        public static IEnumerable<Type> GetTypes(string namespaceName)
        {
            var assemblies = Assembly.GetAssembly(typeof(Signal)).GetTypes().Where(it => it.Namespace == namespaceName && !it.IsAbstract && !it.IsInterface && !it.IsEnum);
            if (assemblies.Count() == 0)
            {
                assemblies = Assembly.GetAssembly(typeof(Signal)).GetTypes().Where(it => it.Namespace == namespaceName && !it.IsAbstract && !it.IsInterface && !it.IsEnum);
            }
            return assemblies;
        }

        public static Type GetType(string fullname)
        {
            return Assembly.GetAssembly(typeof(Signal)).GetType(fullname) ??
                           Assembly.GetAssembly(typeof(Signal)).GetType(fullname);
        }
    }
}
