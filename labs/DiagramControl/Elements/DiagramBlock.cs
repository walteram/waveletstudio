using System;
using System.Drawing;
using System.Reflection;
using DiagramNet.Elements.Controllers;

namespace DiagramNet.Elements
{
	[Serializable]
	public class DiagramBlock : NodeElement, IControllable
	{
		[NonSerialized]
		private RectangleController _controller;

		protected RectangleElement Rectangle;
		private Image _image;
		private string _labelText;
		private object[] _inputStates;
		private object[] _outputStates;
		private PropertyInfo _connectionTextProperty;
		public object State { get; set; }

		public DiagramBlock(Image image, string labelText, object blockState, object[] inputStates, object[] outputStates, PropertyInfo connectionTextProperty) : base(300, 300, 80, 80)
		{
			Overrided = true;
			Rectangle = new RectangleElement(300, 300, 80, 80);
			FillColor1 = Color.White;
			FillColor2 = Color.White;
			Refresh(image, labelText, blockState, inputStates, outputStates,connectionTextProperty);
		}

		public void Refresh(Image image, string labelText, object blockState, object[] inputStates, object[] outputStates, PropertyInfo connectionTextProperty)
		{
			_image = image;
			_labelText = labelText;
			_connectionTextProperty = connectionTextProperty;
			State = blockState;
			SyncContructors();
            _inputStates = inputStates;
            _outputStates = outputStates;

            if (Connects == null || Connects.Length != inputStates.Length + outputStates.Length)
		    {
		        Connects = new ConnectorElement[inputStates.Length + outputStates.Length];
                for (var i = 0; i < inputStates.Length; i++)
                {
                    Connects[i] = new ConnectorElement(this) { State = inputStates[i] };
                }
                for (var i = 0; i < outputStates.Length; i++)
                {
                    Connects[inputStates.Length + i] = new ConnectorElement(this) { State = outputStates[i] };
                }
		    }
            else
            {
                for (var i = 0; i < inputStates.Length; i++)
                {
                    Connects[i].State = inputStates[i];
                }
                for (var i = 0; i < outputStates.Length; i++)
                {
                    Connects[inputStates.Length + i].State = outputStates[i];
                }
            }
			UpdateConnectorsPosition();
			SyncContructors(); 
		}

		protected new void UpdateConnectorsPosition()
		{
			for (var i = 0; i < _inputStates.Length; i++)
			{
				var top = (SizeValue.Height / (_inputStates.Length + 1)) * (i + 1);
				var loc = new Point(LocationValue.X, LocationValue.Y + top);
				var connect = Connects[i];
				connect.Location = new Point(loc.X - ConnectSize, loc.Y - ConnectSize);
				connect.Size = new Size(ConnectSize * 2, ConnectSize * 2);
				connect.IsStart = true;
				connect.State = _inputStates[i];
			}

			for (var i = 0; i < _outputStates.Length; i++)
			{
				var top = (SizeValue.Height / (_outputStates.Length + 1)) * (i + 1);
				var loc = new Point(LocationValue.X + SizeValue.Width, LocationValue.Y + top);
				var connect = Connects[_inputStates.Length+i];
				connect.Location = new Point(loc.X - ConnectSize, loc.Y - ConnectSize);
				connect.Size = new Size(ConnectSize * 2, ConnectSize * 2);
				connect.IsStart = false;
				connect.State = _outputStates[i];
			}
		}

		private void SyncContructors()
		{
			LocationValue = Rectangle.Location;
			SizeValue = Rectangle.Size;
			BorderColorValue = Rectangle.BorderColor;
			BorderWidthValue = Rectangle.BorderWidth;
			OpacityValue = Rectangle.Opacity;
			VisibleValue = Rectangle.Visible;
		}

		internal override void Draw(Graphics g)
		{
			IsInvalidated = false;			

			var image = new ImageElement(_image, Rectangle);
			var label = new LabelElement(Rectangle.Location.X, image.Top + image.Height + 2,Rectangle.Size.Width, 12) {Text = _labelText, Font = new Font(FontFamily.GenericSansSerif, 8)};
			Rectangle.Draw(g);
			image.Draw(g);
			label.Draw(g);

			for (var i = 0; i < Connects.Length; i++)
			{
				var conn = Connects[i];
				int posX;
				StringAlignment alignment;
				var labelText = _connectionTextProperty.GetValue(conn.State, null).ToString();
				if (conn.IsStart)
				{
					posX = conn.Location.X + conn.Size.Width + 2;
					alignment = StringAlignment.Near;
				}
				else
				{
					posX = conn.Location.X - 42;
					alignment = StringAlignment.Far;
				}
				var connectorLabel = new LabelElement(posX, conn.Location.Y - conn.Size.Height / 2, 40, 12) { Text = labelText, Alignment = alignment, Font = new Font(FontFamily.GenericSansSerif, 8) };
				connectorLabel.Draw(g);
			}		    
		}

		IController IControllable.GetController()
		{
			return _controller ?? (_controller = new RectangleController(this));
		}

#region Overrided

		public override Color BorderColor
		{
			get
			{
				return base.BorderColor;
			}
			set
			{
				Rectangle.BorderColor = value;
				base.BorderColor = value;
			}
		}

		public Color FillColor1
		{
			get
			{
				return Rectangle.FillColor1;
			}
			set
			{
				Rectangle.FillColor1 = value;
			}
		}

		public Color FillColor2
		{
			get
			{
				return Rectangle.FillColor2;
			}
			set
			{
				Rectangle.FillColor2 = value;
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
				Rectangle.Opacity = value;
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
				Rectangle.Visible = value;
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
				Rectangle.Location = value;
				base.Location = value;

				UpdateConnectorsPosition();
				OnAppearanceChanged(new EventArgs());
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
				Rectangle.Size = value;
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
				Rectangle.BorderWidth = value;
				base.BorderWidth = value;
			}
		}
#endregion

	}
}
