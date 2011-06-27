using ILNumerics;

namespace WaveLib
{
    public class Signal
    {
        public ILArray<double> Points { get; set; }

        public int PointsPerSecond { get; set; }

        public Signal()
        {
            Points = new ILArray<double>();
            PointsPerSecond = 1;
        }

        public Signal(double[] points, int pointsPerSecond)
        {
            Points = new ILArray<double>(points);
            PointsPerSecond = pointsPerSecond;
        }

        public Signal(ILArray<double> points, int pointsPerSecond)
        {
            Points = points;
            PointsPerSecond = pointsPerSecond;
        }

        /// <summary>
        /// Informs that size of signal is a power of 2.
        /// </summary>
        /// <returns></returns>
        public bool LengthIsPowerOf2()
        {
            return IsPowerOf2(Points.Length);
        }

        /// <summary>
        /// Resizes the signal until its length be a power of 2.
        /// </summary>
        /// <returns></returns>
        public void MakeLengthPowerOfTwo()
        {
            if (LengthIsPowerOf2())
            {
                return;
            }
            var length = Points.Length;
            while (!(IsPowerOf2(length)) && length > 0)
            {
                length--;
            }
            Points[string.Format("{0}:1:{1}", length, Points.Length - 1)] = null;            
        }

        private bool IsPowerOf2(int x)
        {
            return (x != 0) && ((x & (x - 1)) == 0);
        }        
    }
}
