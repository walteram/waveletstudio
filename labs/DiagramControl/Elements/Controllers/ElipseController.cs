using System.Drawing;
using System.Drawing.Drawing2D;

namespace DiagramNet.Elements.Controllers
{
	/// <summary>
	/// This class is the controller for ElipseElement
	/// </summary>
	internal class ElipseController: RectangleController
	{
		public ElipseController(BaseElement element): base(element)
		{
		}
	
		#region IController Members

		public override bool HitTest(Point p)
		{
			var gp = new GraphicsPath();
			var mtx = new Matrix();

			gp.AddEllipse(new Rectangle(El.Location.X,
				El.Location.Y,
				El.Size.Width,
				El.Size.Height));
			gp.Transform(mtx);

			return gp.IsVisible(p);
		}

		public override void DrawSelection(Graphics g)
		{
		    const int border = 3;
			var r = BaseElement.GetUnsignedRectangle(
				new Rectangle(
					El.Location.X - border, El.Location.Y - border,
					El.Size.Width + (border * 2), El.Size.Height + (border * 2)));

			var brush = new HatchBrush(HatchStyle.SmallCheckerBoard, Color.LightGray, Color.Transparent);
			var p = new Pen(brush, border);
			g.DrawEllipse(p, r);

			p.Dispose();
			brush.Dispose();
		}

		#endregion
	}
}
