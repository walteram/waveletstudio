using System;
using DiagramNet.Elements;

namespace DiagramNet.Events
{
	public class ElementConnectEventArgs: EventArgs
	{
		private readonly NodeElement _node1;
		private readonly NodeElement _node2;
		private readonly BaseLinkElement _link;

		public ElementConnectEventArgs(NodeElement node1, NodeElement node2, BaseLinkElement link)
		{
			_node1 = node1;
			_node2 = node2;
			_link = link;
		}

		public NodeElement Node1
		{
			get
			{
				return _node1;
			}
		}

		public NodeElement Node2
		{
			get
			{
				return _node2;
			}
		}

		public BaseLinkElement Link
		{
			get
			{
				return _link;
			}
		}

		public override string ToString()
		{
			var toString = "";

			if (_node1 != null)
				toString += "Node1:" + _node1;

			if (_node2 != null)
				toString += "Node2:" + _node2;

			if (_link != null)
				toString += "Link:" + _link;

			return toString;
		}

	}
}
