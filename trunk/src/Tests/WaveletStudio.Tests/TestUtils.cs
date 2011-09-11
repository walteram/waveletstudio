using System;
using System.Linq;

namespace WaveletStudio.Tests
{
    public static class TestUtils
    {
        public static bool SequenceEquals(double[] double1, double[] double2)
        {
            if (double1.Length != double2.Length)
                return false;
            for (var i = 0; i < double1.Count(); i++)
            {
                if (!AlmostEquals(double1[i], double2[i], 0.0000001))
                    return false;
            }
            return true;
        }

        private static bool AlmostEquals(double double1, double double2, double precision)
        {
            return (Math.Abs(double1 - double2) <= precision);
        }
    }
}
