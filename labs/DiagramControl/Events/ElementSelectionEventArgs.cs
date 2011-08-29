using System;

namespace DiagramNet.Events
{
	public class ElementSelectionEventArgs: EventArgs 
	{
	    readonly ElementCollection _elements;

		public ElementSelectionEventArgs(ElementCollection elements)
		{
			_elements = elements;
		}

		public ElementCollection Elements
		{
			get
			{
				return _elements;
			}
		}

		public override string ToString()
		{
			return "ElementCollection: " + _elements.Count.ToString();
		}

	}
}
