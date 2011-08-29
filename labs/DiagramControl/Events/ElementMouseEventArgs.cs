using DiagramNet.Elements;

namespace DiagramNet.Events
{
	public class ElementMouseEventArgs: ElementEventArgs 
	{
	    public ElementMouseEventArgs(BaseElement el, int x, int y): base (el)
		{
			X = x;
			Y = y;
		}

	    public int X { get; set; }

	    public int Y { get; set; }

	    public override string ToString()
		{
			return base.ToString() + " X:" + X + " Y:" + Y;
		}

	}
}
