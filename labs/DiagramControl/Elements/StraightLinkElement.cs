using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using DiagramNet.Elements.Controllers;

namespace DiagramNet.Elements
{
	[Serializable]
	public class StraightLinkElement: BaseLinkElement, IControllable, ILabelElement
	{
		protected LineElement Line1 = new LineElement(0,0,0,0);
		private LabelElement _label = new LabelElement();

		[NonSerialized]
		private LineController _controller;

		internal StraightLinkElement(ConnectorElement conn1, ConnectorElement conn2): base(conn1, conn2)
		{
			_label.PositionBySite(Line1);
		}

		#region Properties
		[Browsable(false)]
		public override Point Point1
		{
			get
			{
				return Line1.Point1;
			}
		}

		[Browsable(false)]
		public override Point Point2
		{
			get
			{
				return Line1.Point2;
			}
		}

		public override Color BorderColor
		{
			get
			{
				return Line1.BorderColor;
			}
			set
			{
				Line1.BorderColor = value;
			}
		}

		public override int BorderWidth
		{
			get
			{
				return Line1.BorderWidth;
			}
			set
			{
				Line1.BorderWidth = value;
			}
		}

		public override Point Location
		{
			get
			{
				CalcLink();
				return Line1.Location;
			}
		}

		public override Size Size
		{
			get
			{
				CalcLink();
				return Line1.Size;
			}
		}

		public override int Opacity
		{
			get
			{
				return Line1.Opacity;
			}
			set
			{
				Line1.Opacity = value;
			}
		}

		public override LineCap StartCap
		{
			get
			{
				return Line1.StartCap;
			}
			set
			{
				Line1.StartCap = value;
			}
		}

		public override LineCap EndCap
		{
			get
			{
				return Line1.EndCap;
			}
			set
			{
				Line1.EndCap = value;
			}
		}

		public override LineElement[] Lines
		{
			get
			{
				return new[] {Line1};
			}
		}


		#endregion

		internal override void Draw(Graphics g)
		{
			IsInvalidated = false;

			Line1.Draw(g);
		}

		internal override void CalcLink()
		{
			if (NeedCalcLinkValue == false) return;

			if (Line1 != null)
			{
				var connector1Location = Connector1Value.Location;
				var connector2Location = Connector2Value.Location;
				var connector1Size = Connector1Value.Size;
				var connector2Size = Connector2Value.Size;

				Line1.Point1 = new Point(connector1Location.X + connector1Size.Width / 2, connector1Location.Y + connector1Size.Height / 2);
				Line1.Point2 = new Point(connector2Location.X + connector2Size.Width / 2, connector2Location.Y + connector2Size.Height / 2);
				Line1.CalcLine();
			}

			NeedCalcLinkValue = false;
		}

		#region IControllable Members

		IController IControllable.GetController()
		{
		    return _controller ?? (_controller = new LineController(Line1));
		}

	    #endregion
	
		#region ILabelElement Members

		public virtual LabelElement Label 
		{
			get
			{
				return _label;
			}
			set
			{
				_label = value;
				OnAppearanceChanged(new EventArgs());
			}
		}

		#endregion
	}
}


