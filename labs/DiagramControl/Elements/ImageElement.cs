using System;
using System.Drawing;
using DiagramNet.Elements.Controllers;

namespace DiagramNet.Elements
{
	[Serializable]
	public class ImageElement : BaseElement, IControllable
	{
		[NonSerialized]
		private RectangleController _controller;

		private readonly Image _image;
		public int Top;
		public int Left;
		public int Width;
		public int Height;

		public ImageElement(Image image, int top, int left, int width, int height)
		{
			_image = image;
			Top = top;
			Left = left;
			Width = width;
			Height = height;
		}

		public ImageElement(Image image, BaseElement rectangle)
		{
			_image = image;
			Left = rectangle.Location.X + rectangle.Size.Width/2 - image.Width/2;
			Top = rectangle.Location.Y + rectangle.Size.Height / 2 - image.Height / 2;
		    Width = image.Width;
			Height = image.Height;
		}

		internal override void Draw(Graphics g)
		{
			IsInvalidated = false;
			g.DrawImage(_image, Left, Top, Width, Height);
		}
		
		IController IControllable.GetController()
		{
			return _controller ?? (_controller = new RectangleController(this));
		}
	}
}
