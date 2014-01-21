namespace WaveletStudio.Blocks
{
    /// <summary>
    /// Represents a shortcut to the signal stored in the Input or Output node
    /// </summary>
    public interface IBlockInOutSignal
    {
        /// <summary>
        /// Returns the first signal of the node with the index specified
        /// </summary>
        Signal this[int nodeIndex] { get; }

        /// <summary>
        /// Returns the signal of the node with the index specified
        /// </summary>
        Signal this[int nodeIndex, int signalIndex] { get; }

        /// <summary>
        /// Returns the first signal of the node with the name specified
        /// </summary>
        Signal this[string nodeName] { get; }

        /// <summary>
        /// Returns the signal of the node with the name specified
        /// </summary>
        Signal this[string nodeName, int signalIndex] { get; }
    }
}