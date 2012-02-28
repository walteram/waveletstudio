using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Reflection;
using DiagramNet.Elements.Controllers;

namespace DiagramNet.Elements
{
	[
	Serializable,
	TypeConverter(typeof(ExpandableObjectConverter))
	]
	public sealed class LabelElement: BaseElement, ISerializable, IControllable
	{
	    private Color _foreColor1 = Color.Black;
	    private Color _foreColor2 = Color.Empty;

	    private Color _backColor1 = Color.Empty;
	    private Color _backColor2 = Color.Empty;

		[NonSerialized]
		private RectangleController _controller;

	    private string _text = "";

	    private bool _autoSize;
		
		[NonSerialized] private Font _font = new Font(FontFamily.GenericSansSerif, 8);
		
		[NonSerialized]
		private readonly StringFormat _format = new StringFormat(StringFormatFlags.NoWrap);

		private StringAlignment _alignment;
		private StringAlignment _lineAlignment;
		private StringTrimming _trimming;
		private bool _wrap;
		private bool _vertical;
	    private bool _readOnly;

		public LabelElement(): this(0, 0, 100, 100)
		{}

		public LabelElement(Rectangle rec): this(rec.Location, rec.Size)
		{}

		public LabelElement(Point l, Size s): this(l.X, l.Y, s.Width, s.Height) 
		{}

		public LabelElement(int top, int left, int width, int height): base(top, left, width, height)
		{
			Alignment = StringAlignment.Center;
			LineAlignment = StringAlignment.Center;
			Trimming = StringTrimming.Character;
			Vertical = false;
			Wrap = true;
			BorderColorValue = Color.Transparent;
		}

		#region Properties
		public string Text
		{
			get
			{
				return _text;
			}
			set
			{
				_text = value;
				if (_autoSize) DoAutoSize();
				OnAppearanceChanged(new EventArgs());
			}
		}

		public Font Font
		{
			get
			{
				return _font;
			}
			set
			{
				_font = value;
				if (_autoSize) DoAutoSize();
				OnAppearanceChanged(new EventArgs());
			}
		}

		public StringAlignment Alignment
		{
			get
			{
				return _alignment;
			}
			set
			{
				_alignment = value;
				_format.Alignment = _alignment;
				if (_autoSize) DoAutoSize();
				OnAppearanceChanged(new EventArgs());
			}
		}

		public StringAlignment LineAlignment
		{
			get
			{
				return _lineAlignment;
			}
			set
			{
				_lineAlignment = value;
				_format.LineAlignment = _lineAlignment;
				if (_autoSize) DoAutoSize();
				OnAppearanceChanged(new EventArgs());
			}
		}

		public StringTrimming Trimming
		{
			get
			{
				return _trimming;
			}
			set
			{
				_trimming = value;
				_format.Trimming = _trimming;
				if (_autoSize) DoAutoSize();
				OnAppearanceChanged(new EventArgs());
			}
		}

		public bool Wrap
		{
			get
			{
				return _wrap;
			}
			set
			{
				_wrap = value;
				if (_wrap)
					_format.FormatFlags &= ~StringFormatFlags.NoWrap;
				else
					_format.FormatFlags |= StringFormatFlags.NoWrap;
				
				if (_autoSize) DoAutoSize();
				OnAppearanceChanged(new EventArgs());
			}
		}

		public bool Vertical
		{
			get
			{
				return _vertical;
			}
			set
			{
				_vertical = value;
				if (_vertical)
					_format.FormatFlags |= StringFormatFlags.DirectionVertical;
				else
					_format.FormatFlags &= ~StringFormatFlags.DirectionVertical;
				
				if (_autoSize) DoAutoSize();
				OnAppearanceChanged(new EventArgs());
			}

		}

		public bool ReadOnly
		{
			get
			{
				return _readOnly;
			}
			set
			{
				_readOnly = value;

				OnAppearanceChanged(new EventArgs());
			}
		}

		public Color ForeColor1
		{
			get
			{
				return _foreColor1;
			}
			set
			{
				_foreColor1 = value;
				OnAppearanceChanged(new EventArgs());
			}
		}

		public Color ForeColor2 
		{
			get
			{
				return _foreColor2;
			}
			set
			{
				_foreColor2 = value;
				OnAppearanceChanged(new EventArgs());
			}
		}

		public Color BackColor1
		{
			get
			{
				return _backColor1;
			}
			set
			{
				_backColor1 = value;
				OnAppearanceChanged(new EventArgs());
			}
		}

		public Color BackColor2 
		{
			get
			{
				return _backColor2;
			}
			set
			{
				_backColor2 = value;
				OnAppearanceChanged(new EventArgs());
			}
		}

		public bool AutoSize
		{
			get
			{
				return _autoSize;
			}
			set
			{
				_autoSize = value;
				if (_autoSize) DoAutoSize();
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
				SizeValue = value;
				if (_autoSize) DoAutoSize();
				base.Size = SizeValue;			
			}
		}

		internal StringFormat Format
		{
			get
			{
				return _format;
			}
		}

		#endregion

		public void DoAutoSize()
		{
			if (_text.Length == 0) return;

			var bmp = new Bitmap(1,1);
			var g = Graphics.FromImage(bmp);
			var sizeF = g.MeasureString(_text, _font, SizeValue.Width, _format);
			var sizeTmp = Size.Round(sizeF);

			if (SizeValue.Height < sizeTmp.Height)
				SizeValue.Height = sizeTmp.Height;
		}

	    private Brush GetBrushBackColor(Rectangle r)
		{
			//Fill rectangle
			Color fill1;
			Color fill2;
			Brush b;
			if (OpacityValue == 100)
			{
				fill1 = _backColor1;
				fill2 = _backColor2;
			}
			else
			{
				fill1 = Color.FromArgb((int) (255.0f * (OpacityValue / 100.0f)), _backColor1);
				fill2 = Color.FromArgb((int) (255.0f * (OpacityValue / 100.0f)), _backColor2);
			}
			
			if (_backColor2 == Color.Empty)
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

			return b;
		}

	    private Brush GetBrushForeColor(Rectangle r)
		{
			//Fill rectangle
			Color fill1;
			Color fill2;
			Brush b;
			if (OpacityValue == 100)
			{
				fill1 = _foreColor1;
				fill2 = _foreColor2;
			}
			else
			{
				fill1 = Color.FromArgb((int) (255.0f * (OpacityValue / 100.0f)), _foreColor1);
				fill2 = Color.FromArgb((int) (255.0f * (OpacityValue / 100.0f)), _foreColor2);
			}
			
			if (_foreColor2 == Color.Empty)
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

			return b;
		}

		internal override void Draw(Graphics g)
		{
			var r = GetUnsignedRectangle();			
			g.FillRectangle(GetBrushBackColor(r), r);
			var b = GetBrushForeColor(r);
			g.DrawString(_text, _font, b, r, _format);
			DrawBorder(g, r);
			b.Dispose();
		}

	    private void DrawBorder(Graphics g, Rectangle r)
		{
			//Border
			var p = new Pen(BorderColorValue, BorderWidthValue);
			g.DrawRectangle(p, r);
			p.Dispose();
		}

		#region ISerializable Members

	    private LabelElement(SerializationInfo info, StreamingContext context)
		{

			// Get the set of serializable members for our class and base classes
			var thisType = typeof(LabelElement);
			var mi = FormatterServices.GetSerializableMembers(thisType, context);

			// Deserialize the base class's fields from the info object
			foreach (var t in mi)
			{
                 // Don't deserialize fields for this class
			    if (t.DeclaringType == thisType) continue;

			    // To ease coding, treat the member as a FieldInfo object
			    var fi = (FieldInfo) t;

			    // Set the field to the deserialized value
			    fi.SetValue(this, info.GetValue(fi.Name, fi.FieldType));
			}

			// Deserialize the values that were serialized for this class
			ForeColor1 = (Color) info.GetValue("foreColor1", typeof(Color));
			ForeColor2 = (Color) info.GetValue("foreColor2", typeof(Color));
			BackColor1 = (Color) info.GetValue("backColor1", typeof(Color));
			BackColor2 = (Color) info.GetValue("backColor2", typeof(Color));
			Text = info.GetString("text");
			Alignment = (StringAlignment) info.GetValue("alignment", typeof(StringAlignment));
			LineAlignment = (StringAlignment) info.GetValue("lineAlignment", typeof(StringAlignment));
			Trimming = (StringTrimming) info.GetValue("trimming", typeof(StringTrimming));
			Wrap = info.GetBoolean("wrap");
			Vertical = info.GetBoolean("vertical");
			ReadOnly = info.GetBoolean("readOnly");
			AutoSize = info.GetBoolean("autoSize");
			
			var fc = new FontConverter();
			Font = (Font) fc.ConvertFromString(info.GetString("font"));
			
		}

		[SecurityPermissionAttribute(SecurityAction.Demand,SerializationFormatter=true)]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			// Serialize the desired values for this class
			info.AddValue("foreColor1", _foreColor1);
			info.AddValue("foreColor2", _foreColor2);
			info.AddValue("backColor1", _backColor1);
			info.AddValue("backColor2", _backColor2);
			info.AddValue("text", _text);
			info.AddValue("alignment", _alignment);
			info.AddValue("lineAlignment", _lineAlignment);
			info.AddValue("trimming", _trimming);
			info.AddValue("wrap", _wrap);
			info.AddValue("vertical", _vertical);
			info.AddValue("readOnly", _readOnly);
			info.AddValue("autoSize", _autoSize);

			var fc = new FontConverter();
			info.AddValue("font", fc.ConvertToString(_font));

			// Get the set of serializable members for our class and base classes
			var thisType = typeof(LabelElement);
			var mi = FormatterServices.GetSerializableMembers(thisType, context);

			// Serialize the base class's fields to the info object
			foreach (var t in mi)
			{
                // Don't serialize fields for this class
			    if (t.DeclaringType == thisType) continue;
			    info.AddValue(t.Name, ((FieldInfo) t).GetValue(this));
			}
		}
		#endregion

		IController IControllable.GetController()
		{
		    return _controller ?? (_controller = new RectangleController(this));
		}

	    internal void PositionBySite(BaseElement site)
		{
			var newLocation = Point.Empty;
			var siteLocation = site.Location;
			var siteSize = site.Size;
			var thisSize = Size;
			
			newLocation.X = (siteLocation.X + (siteSize.Width / 2)) - (thisSize.Width / 2);
			newLocation.Y = (siteLocation.Y + (siteSize.Height / 2)) - (thisSize.Height / 2);

			Location = newLocation;
		}
	}
}




