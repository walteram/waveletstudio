using System.Drawing;

namespace DiagramNet.Elements.Controllers
{
	/// <summary>
	/// This class is the controller for ConnectorElement
	/// </summary>
	internal class ConnectorController: RectangleController
	{
		public ConnectorController(BaseElement element): base(element)
		{}

		public override void DrawSelection(Graphics g)
		{
			const int distance = 1;
			const int border = 2;

			var r = BaseElement.GetUnsignedRectangle(
				new Rectangle(
				El.Location.X - distance, El.Location.Y - distance,
				El.Size.Width + (distance * 2), El.Size.Height + (distance * 2)));

			//HatchBrush brush = new HatchBrush(HatchStyle.SmallCheckerBoard, Color.Red, Color.Transparent);
			var brush = new SolidBrush(Color.FromArgb(150, Color.Green));
			var p = new Pen(brush, border);
			g.DrawRectangle(p, r);
			
			p.Dispose();
			brush.Dispose();
		}
	}
}
