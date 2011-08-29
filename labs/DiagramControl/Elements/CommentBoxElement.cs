using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using DiagramNet.Elements.Controllers;

namespace DiagramNet.Elements
{   
	[Serializable]
	public class CommentBoxElement: RectangleElement, IControllable
	{
		[NonSerialized]
		private RectangleController _controller;

		protected Size FoldSize = new Size(10, 15);

		public CommentBoxElement(): this(0, 0, 100, 100)
		{}

		public CommentBoxElement(Rectangle rec): this(rec.Location, rec.Size)
		{}

		public CommentBoxElement(Point l, Size s): this(l.X, l.Y, s.Width, s.Height) 
		{}

		public CommentBoxElement(int top, int left, int width, int height): base(top, left, width, height)
		{
			FillColor1Value = Color.LemonChiffon;
			FillColor2Value = Color.FromArgb(255, 255, 128);

			LabelValue.Opacity = 100;
		}


		internal override void Draw(Graphics g)
		{
			IsInvalidated = false;

			var r = GetUnsignedRectangle(new Rectangle(LocationValue, SizeValue));

			var points = new Point[5];
			points[0] = new Point(r.X + 0, r.Y + 0);
			points[1] = new Point(r.X + 0, r.Y + r.Height);
			points[2] = new Point(r.X + r.Width, r.Y + r.Height);

			//Fold
			points[3] = new Point(r.X + r.Width, r.Y + FoldSize.Height);
			points[4] = new Point(r.X + r.Width - FoldSize.Width, r.Y + 0);

			//foreach(Point p in points) p.Offset(location.X, location.Y);

			g.FillPolygon(GetBrush(r), points, FillMode.Alternate);
			g.DrawPolygon(new Pen(BorderColorValue, BorderWidthValue), points);

			g.DrawLine(new Pen(BorderColorValue, BorderWidthValue),
					   new Point(r.X + r.Width - FoldSize.Width, r.Y + FoldSize.Height),
					   new Point(r.X + r.Width, r.Y + FoldSize.Height));

			g.DrawLine(new Pen(BorderColorValue, BorderWidthValue),
					   new Point(r.X + r.Width - FoldSize.Width, r.Y + 0),
					   new Point(r.X + r.Width - FoldSize.Width, r.Y + 0 + FoldSize.Height));
		}

		IController IControllable.GetController()
		{
			return _controller ?? (_controller = new CommentBoxController(this));
		}
	}
}
