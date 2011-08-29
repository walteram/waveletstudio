using System.Drawing;
using System.Drawing.Drawing2D;

namespace DiagramNet.Elements.Controllers
{
	/// <summary>
	/// This class is the controller for LineElement
	/// </summary>
	internal class LineController: IController
	{
		//parent element
		protected LineElement El;

		public LineController(LineElement element)
		{
			El = element;
		}

		#region IController Members

		public BaseElement OwnerElement
		{
			get
			{
				return El;
			}
		}

		public bool HitTest(Point p)
		{
			var gp = new GraphicsPath();
			var mtx = new Matrix();
		    var pen = new Pen(El.BorderColor, El.BorderWidth + 4) {StartCap = El.StartCap, EndCap = El.EndCap};
		    gp.AddLine(El.Point1, El.Point2);
			gp.Transform(mtx);
			//Rectangle retGp = Rectangle.Round(gp.GetBounds());
			return gp.IsOutlineVisible (p, pen);
		}

		public bool HitTest(Rectangle r)
		{
			var gp = new GraphicsPath();
			var mtx = new Matrix();

			gp.AddRectangle(new Rectangle(El.Location.X,
				El.Location.Y,
				El.Size.Width,
				El.Size.Height));
			gp.Transform(mtx);
			var retGp = Rectangle.Round(gp.GetBounds());
			return r.Contains (retGp);
		}

		public void DrawSelection(Graphics g)
		{
			/*
			Pen p = new Pen(Color.FromArgb(80, Color.Yellow), el.BorderWidth + 2);
			p.StartCap = el.StartCap;
			p.EndCap = el.EndCap;
			g.DrawLine(p, el.Point1, el.Point2);
			*/
		}

		#endregion

	}
}
