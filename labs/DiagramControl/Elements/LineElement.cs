using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using DiagramNet.Elements.Controllers;

namespace DiagramNet.Elements
{
	[Serializable]
	public class LineElement: BaseElement, IControllable 
	{
		private Point _point1;
		private Point _point2;
		private LineCap _startCap = LineCap.Round;
		private LineCap _endCap = LineCap.Round;
		private bool _needCalcLine;

		[NonSerialized]
		private LineController _controller;

		internal LineElement(int x1, int y1, int x2, int y2)
			: this(new Point(x1, y1), new Point(x2, y2)) {}

		internal LineElement(Point p1, Point p2)
		{
			_point1 = p1;			
			_point2 = p2;
			
			BorderWidthValue = 1;
			BorderColorValue = Color.Black;
		}

		public virtual Point Point1
		{
			get
			{
				CalcLine();
				return _point1;
			}
			set
			{
				_point1 = value;
				_needCalcLine = true;
				OnAppearanceChanged(new EventArgs());
			}
		}

		public virtual Point Point2
		{
			get
			{
				CalcLine();
				return _point2;
			}
			set
			{
				_point2 = value;
				_needCalcLine = true;
				OnAppearanceChanged(new EventArgs());
			}
		}

		public virtual LineCap StartCap
		{
			get
			{
				return _startCap;
			}
			set
			{
				_startCap = value;
				OnAppearanceChanged(new EventArgs());
			}
		}

		public virtual LineCap EndCap
		{
			get
			{
				return _endCap;
			}
			set
			{
				_endCap = value;
				OnAppearanceChanged(new EventArgs());
			}
		}

		internal override void Draw(Graphics g)
		{
			IsInvalidated = false;
			//Fill rectangle    
			var borderColor1 = OpacityValue == 100 ? BorderColorValue : Color.FromArgb((int)(255.0f * (OpacityValue / 100.0f)), BorderColorValue);
			var p = new Pen(borderColor1, BorderWidthValue) {StartCap = _startCap, EndCap = _endCap};
			g.DrawLine(p, _point1, _point2);
			p.Dispose();
		}

		internal void CalcLine()
		{
			if (_needCalcLine == false) return;

			//Find Location and Size
			if (_point1.X < _point2.X)
			{
				LocationValue.X = _point1.X;
				SizeValue.Width = _point2.X - _point1.X;
			}
			else
			{
				LocationValue.X = _point2.X;
				SizeValue.Width = _point1.X - _point2.X;
			}

			if (_point1.Y < _point2.Y)
			{
				LocationValue.Y = _point1.Y;
				SizeValue.Height = _point2.Y - _point1.Y;
			}
			else
			{
				LocationValue.Y = _point2.Y;
				SizeValue.Height = _point1.Y - _point2.Y;
			}

			_needCalcLine = false;
		}
		
		#region IControllable Members

		IController IControllable.GetController()
		{
			if (_controller == null)
				_controller = new LineController(this);
			return _controller;
		}

		#endregion
	}
}