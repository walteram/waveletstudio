using System;
using System.Linq;
using ILNumerics;

namespace WaveletStudio.Tests
{
    public class TestUtils
    {
        public static bool SequenceEquals(ILArray<double> double1, ILArray<double> double2)
        {
            if (double1.Length != double2.Length)
                return false;
            for (var i = 0; i < double1.Count(); i++)
            {
                if (!AlmostEquals(double1.GetValue(i), double2.GetValue(i), 0.0000001))
                    return false;
            }
            return true;
        }

        public static bool AlmostEquals(double double1, double double2, double precision)
        {
            return (Math.Abs(double1 - double2) <= precision);
        }
    }
}
