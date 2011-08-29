using System;
using System.Drawing;

namespace DiagramNet.Elements
{
	/// <summary>
	/// This is the base for all element the will be draw on the
	/// document.
	/// </summary>
	[Serializable]
	public abstract class BaseElement
	{
		protected Point LocationValue;
		protected Size SizeValue;
		protected bool VisibleValue = true;
		protected Color BorderColorValue = Color.Black;
		protected int BorderWidthValue = 1;
		protected int OpacityValue = 100;
		internal protected Rectangle InvalidateRec = Rectangle.Empty;
		internal protected bool IsInvalidated = true;

		protected BaseElement()
		{
		}

		protected BaseElement(int top, int left, int width, int height)
		{
			LocationValue  = new Point(top, left);
			SizeValue = new Size(width, height);
		}

		public virtual Point Location
		{
			get
			{
				return LocationValue;
			}
			set
			{
				LocationValue = value;
				OnAppearanceChanged(new EventArgs());
			}
		}

		public virtual Size Size
		{
			get
			{
				return SizeValue;
			}
			set
			{
				SizeValue = value;
				OnAppearanceChanged(new EventArgs());
			}
		}

		public virtual bool Visible
		{
			get
			{
				return VisibleValue;
			}
			set
			{
				VisibleValue = value;
				OnAppearanceChanged(new EventArgs());
			}
		}

		public virtual Color BorderColor
		{
			get
			{
				return BorderColorValue;
			}
			set
			{
				BorderColorValue = value;
				OnAppearanceChanged(new EventArgs());
			}
		}

		public virtual int BorderWidth
		{
			get
			{
				return BorderWidthValue;
			}
			set
			{
				BorderWidthValue = value;
				OnAppearanceChanged(new EventArgs());
			}
		}
		
		public virtual int Opacity 
		{
			get
			{
				return OpacityValue;
			}
			set
			{
				if ((value >= 0) || (value <=100))
					OpacityValue = value;
				else
					throw new Exception("'" + value + "' is not a valid value for 'Opacity'. 'Opacity' should be between 0 and 100.");

				OnAppearanceChanged(new EventArgs());
			}
		}
		internal virtual void Draw(Graphics g)
		{
			IsInvalidated = false;
		}


		public virtual void Invalidate()
		{
			InvalidateRec = IsInvalidated ? Rectangle.Union(InvalidateRec, GetUnsignedRectangle()) : GetUnsignedRectangle();
			IsInvalidated = true;
		}

		public virtual Rectangle GetRectangle()
		{
			return new Rectangle(Location, Size);
		}

		public virtual Rectangle GetUnsignedRectangle()
		{
			
			return GetUnsignedRectangle(GetRectangle());
		}

		internal static Rectangle GetUnsignedRectangle(Rectangle rec)
		{
			var retRectangle = rec;
			if (rec.Width < 0)
			{
				retRectangle.X = rec.X + rec.Width;
				retRectangle.Width = - rec.Width;
			}
			
			if (rec.Height < 0)
			{
				retRectangle.Y = rec.Y + rec.Height;
				retRectangle.Height = - rec.Height;
			}

			return retRectangle;
		}

		#region Events
		[field: NonSerialized]
		public event EventHandler AppearanceChanged;

		protected virtual void OnAppearanceChanged(EventArgs e)
		{
			if (AppearanceChanged != null)
				AppearanceChanged(this, e);
		}
		#endregion

	}
}
