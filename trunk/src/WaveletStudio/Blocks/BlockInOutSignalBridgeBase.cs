using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace WaveletStudio.Blocks
{
    /// <summary>
    /// A shortcut to get a signal of a node (input or output of a signal)
    /// </summary>
    [Serializable]
    public abstract class BlockInOutSignalBridgeBase<T> : IBlockInOutSignal where T : BlockNodeBase
    {
        private readonly BlockBase _root;

        /// <summary>
        /// Returns the first signal of the specified input/output
        /// </summary>
        /// <param name="nodeIndex">The zero-based index of the input/output</param>
        public Signal this[int nodeIndex]
        {
            get
            {
                return this[nodeIndex, 0];
            }
        }

        /// <summary>
        /// Returns the first signal of the specified input/output
        /// </summary>
        /// <param name="nodeName">The short name of the input/output</param>
        public Signal this[string nodeName]
        {
            get { return this[nodeName, 0]; }
        }

        /// <summary>
        /// Returns a signal of the specified input/output
        /// </summary>
        /// <param name="nodeIndex">The zero-based index of the input/output</param>
        /// <param name="signalIndex">The zero-based index of the desired signal</param>
        public Signal this[int nodeIndex, int signalIndex]
        {
            get
            {
                var signals = AllSignals(nodeIndex);
                if (signals == null || signalIndex >= signals.Count)
                {
                    return null;
                }
                return signals[signalIndex];
            }
        }

        /// <summary>
        /// Returns the first signal of the specified input/output
        /// </summary>
        /// <param name="nodeName">The short name of the input/output</param>
        /// <param name="signalIndex">The zero-based index of the desired signal</param>
        public Signal this[string nodeName, int signalIndex]
        {
            get
            {
                var signals = AllSignals(nodeName);
                if (signals == null || signalIndex >= signals.Count)
                {
                    return null;
                }
                return signals[signalIndex];
            }
        }

        /// <summary>
        /// Get all signals of output
        /// </summary>
        /// <param name="nodeIndex">The zero-based index of the input/output</param>
        public List<Signal> AllSignals(int nodeIndex)
        {
            var nodeList = GetNodeList(_root);
            if (nodeList == null || nodeList.Count <= nodeIndex)
            {
                return null;
            }
            return nodeList[nodeIndex].SignalList();
        }

        /// <summary>
        /// Get all signals of output
        /// </summary>
        /// <param name="nodeName">The short name of the input/output</param>
        public List<Signal> AllSignals(string nodeName)
        {
            var nodeList = GetNodeList(_root);
            var node = nodeList.FirstOrDefault(o => o.ShortName.ToLower(CultureInfo.InvariantCulture) == nodeName.ToLower(CultureInfo.InvariantCulture)) ??
                       nodeList.FirstOrDefault(o => o.Name.ToLower(CultureInfo.InvariantCulture) == nodeName.ToLower(CultureInfo.InvariantCulture));
            if (node == null)
            {
                return null;
            }
            return node.SignalList();
        }

        /// <summary>
        /// Return the node list
        /// </summary>
        protected abstract List<T> GetNodeList(BlockBase root);

        /// <summary>
        /// Constructor
        /// </summary>        
        protected BlockInOutSignalBridgeBase(BlockBase root)
        {
            _root = root;
        }
    }
}