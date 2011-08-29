using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using DiagramNet.Elements.Controllers;

namespace DiagramNet.Elements
{
	[Serializable]
	public class ElipseElement: RectangleElement, IControllable
	{
		[NonSerialized]
		private ElipseController _controller;

		public ElipseElement()
		{}

		public ElipseElement(Rectangle rec): base(rec) {}

		public ElipseElement(Point l, Size s): base(l, s) {}

		public ElipseElement(int top, int left, int width, int height): base(top, left, width, height) {}

		internal override void Draw(Graphics g)
		{
			IsInvalidated = false;

			var r = GetUnsignedRectangle(
				new Rectangle(
				LocationValue.X, LocationValue.Y,
				SizeValue.Width, SizeValue.Height));

			//Fill elipse
			Color fill1;
			Color fill2;
			Brush b;
			if (OpacityValue == 100)
			{
				fill1 = FillColor1Value;
				fill2 = FillColor2Value;
			}
			else
			{
				fill1 = Color.FromArgb((int) (255.0f * (OpacityValue / 100.0f)), FillColor1Value);
				fill2 = Color.FromArgb((int) (255.0f * (OpacityValue / 100.0f)), FillColor2Value);
			}
			
			if (FillColor2Value == Color.Empty)
				b = new SolidBrush(fill1);
			else
			{
				var rb = new Rectangle(r.X, r.Y, r.Width + 1, r.Height + 1);
				b = new LinearGradientBrush(
					rb,
					fill1, 
					fill2, 
					LinearGradientMode.Horizontal);
			}

			g.FillEllipse(b, r);

			//Border
			var p = new Pen(BorderColorValue, BorderWidthValue);
			g.DrawEllipse(p, r);
			
			p.Dispose();
			b.Dispose();

		}
		
		IController IControllable.GetController()
		{
			return _controller ?? (_controller = new ElipseController(this));
		}
	}
}
