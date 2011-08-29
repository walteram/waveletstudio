using System;
using System.Drawing;
using DiagramNet.Elements.Controllers;

namespace DiagramNet.Elements
{
	[Serializable]
	public class ElipseNode: NodeElement, IControllable, ILabelElement
	{
		private readonly ElipseElement _elipse;
		private LabelElement _label = new LabelElement();

		[NonSerialized]
		private ElipseController _controller;

		public ElipseNode(): this(0, 0, 100, 100)
		{}

		public ElipseNode(Rectangle rec): this(rec.Location, rec.Size)
		{}

		public ElipseNode(Point l, Size s): this(l.X, l.Y, s.Width, s.Height) 
		{}

		public ElipseNode(int top, int left, int width, int height): base(top, left, width, height)
		{
			_elipse = new ElipseElement(top, left, width, height);
			SyncContructors();
		}
		
		public override Color BorderColor
		{
			get
			{
				return base.BorderColor;
			}
			set
			{
				_elipse.BorderColor = value;
				base.BorderColor = value;
			}
		}

		public Color FillColor1
		{
			get
			{
				return _elipse.FillColor1;
			}
			set
			{
				_elipse.FillColor1 = value;
			}
		}

		public Color FillColor2
		{
			get
			{
				return _elipse.FillColor2;
			}
			set
			{
				_elipse.FillColor2 = value;
			}
		}

		public override int Opacity
		{
			get
			{
				return base.Opacity;
			}
			set
			{
				_elipse.Opacity = value;
				base.Opacity = value;
			}
		}

		public override bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				_elipse.Visible = value;
				base.Visible = value;
			}
		}

		public override Point Location
		{
			get
			{	
				return base.Location;
			}
			set
			{
				_elipse.Location = value;
				base.Location = value;
			}
		}

		public override Size Size
		{
			get
			{
				return base.Size;
			}
			set
			{
				_elipse.Size = value;
				base.Size = value;
			}
		}

		public override int BorderWidth
		{
			get
			{
				return base.BorderWidth;
			}
			set
			{
				_elipse.BorderWidth = value;
				base.BorderWidth = value;
			}
		}

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
		
		private void SyncContructors()
		{
			LocationValue = _elipse.Location;
			SizeValue = _elipse.Size;
			BorderColorValue = _elipse.BorderColor;
			BorderWidthValue = _elipse.BorderWidth;
			OpacityValue = _elipse.Opacity;
			VisibleValue = _elipse.Visible;
		}

		internal override void Draw(Graphics g)
		{
			IsInvalidated = false;
			_elipse.Draw(g);
		}

		IController IControllable.GetController()
		{
			return _controller ?? (_controller = new ElipseController(this));
		}
	}
}
