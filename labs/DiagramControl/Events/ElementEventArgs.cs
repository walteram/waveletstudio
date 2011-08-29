using System;
using DiagramNet.Elements;

namespace DiagramNet.Events
{
	public class ElementEventArgs: EventArgs
	{
		private readonly BaseElement _element;

		public ElementEventArgs(BaseElement el)
		{
			_element = el;
		}

		public BaseElement Element
		{
			get
			{
				return _element;
			}
		}

		public override string ToString()
		{
			return "el: " + _element.GetHashCode();
		}


	}
}
