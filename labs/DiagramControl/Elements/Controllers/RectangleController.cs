using System.Drawing;
using System.Drawing.Drawing2D;

namespace DiagramNet.Elements.Controllers
{
	/// <summary>
	/// This class is the controller for RectangleElement
	/// </summary>
	internal class RectangleController: IMoveController, IResizeController
	{
		//parent element
		protected BaseElement El;
		
		//Move vars.
		protected Point DragOffset = new Point(0);
		protected bool IsDragging;
		protected bool CanMove1 = true;
		
		//Resize vars.
		protected const int SelCornerSize = 3;
		protected RectangleElement[] SelectionCorner = new RectangleElement[9];
		protected CornerPosition SelCorner = CornerPosition.Nothing;
		protected bool CanResize1 = true;


		public RectangleController(BaseElement element)
		{
			El = element;

			//Create corners
			for(var i = 0; i < SelectionCorner.Length; i++)
			{
			    SelectionCorner[i] = new RectangleElement(0, 0, SelCornerSize*2, SelCornerSize*2)
			                             {BorderColor = Color.Black, FillColor1 = Color.White, FillColor2 = Color.Empty};
			}
		}
		
		#region IController Members

		public BaseElement OwnerElement
		{
			get
			{
				return El;
			}
		}

		public virtual bool HitTest(Point p)
		{
			var gp = new GraphicsPath();
			var mtx = new Matrix();

			var elLocation = El.Location;
			var elSize = El.Size;
			gp.AddRectangle(new Rectangle(elLocation.X,
				elLocation.Y,
				elSize.Width,
				elSize.Height));
			gp.Transform(mtx);

			return gp.IsVisible(p);
		}

		public virtual bool HitTest(Rectangle r)
		{
			var gp = new GraphicsPath();
			var mtx = new Matrix();

			var elLocation = El.Location;
			var elSize = El.Size;
			gp.AddRectangle(new Rectangle(elLocation.X,
				elLocation.Y,
				elSize.Width,
				elSize.Height));
			gp.Transform(mtx);
			var retGp = Rectangle.Round(gp.GetBounds());
			return r.Contains (retGp);
		}

		public virtual void DrawSelection(Graphics g)
		{
			const int border = 2;

			var elLocation = El.Location;
			var elSize = El.Size;

			var r = BaseElement.GetUnsignedRectangle(
				new Rectangle(
				elLocation.X - border, elLocation.Y - border,
				elSize.Width + (border * 2), elSize.Height + (border * 2)));

			var brush = new HatchBrush(HatchStyle.SmallCheckerBoard, Color.Gray, Color.Transparent);
			var p = new Pen(brush, border);
			g.DrawRectangle(p, r);
			
			p.Dispose();
			brush.Dispose();
		}

		#endregion

		#region IMoveController Members

		void IMoveController.Start(Point posStart)
		{
			var elLocation = El.Location;
			DragOffset.X = elLocation.X - posStart.X;
			DragOffset.Y = elLocation.Y - posStart.Y;
			IsDragging = true;
		}

		void IMoveController.Move(Point posCurrent)
		{
		    if (!IsDragging) return;
		    var dragPointEl = posCurrent;
		    dragPointEl.Offset(DragOffset.X, DragOffset.Y) ;
		    if (dragPointEl.X < 0) dragPointEl.X = 0;
		    if (dragPointEl.Y < 0) dragPointEl.Y = 0;

		    El.Location = dragPointEl;
		}

		void IMoveController.End()
		{
			IsDragging = false;
		}

		bool IMoveController.IsMoving
		{
			get
			{
				return IsDragging;
			}
		}

		bool IMoveController.CanMove
		{
			get
			{
				return CanMove1;
			}
		}

		#endregion
	
		#region IResizeController Members

		public RectangleElement[] Corners
		{
			get
			{
				return SelectionCorner;
			}			
		}

		void IResizeController.UpdateCornersPos()
		{
			// Update selection corner rectangle
			var rec = new Rectangle(El.Location, El.Size);
			SelectionCorner[(int) CornerPosition.TopLeft].Location = new Point(rec.Location.X - SelCornerSize, rec.Location.Y - SelCornerSize);
			SelectionCorner[(int) CornerPosition.TopRight].Location = new Point(rec.Location.X + rec.Size.Width - SelCornerSize, rec.Location.Y - SelCornerSize);
			SelectionCorner[(int) CornerPosition.TopCenter].Location = new Point(rec.Location.X + rec.Size.Width / 2 - SelCornerSize, rec.Location.Y - SelCornerSize);
			
			SelectionCorner[(int) CornerPosition.BottomLeft].Location = new Point(rec.Location.X - SelCornerSize, rec.Location.Y + rec.Size.Height - SelCornerSize);
			SelectionCorner[(int) CornerPosition.BottomRight].Location = new Point(rec.Location.X + rec.Size.Width - SelCornerSize, rec.Location.Y  + rec.Size.Height - SelCornerSize);
			SelectionCorner[(int) CornerPosition.BottomCenter].Location = new Point(rec.Location.X + rec.Size.Width / 2 - SelCornerSize, rec.Location.Y + rec.Size.Height - SelCornerSize);

			SelectionCorner[(int) CornerPosition.MiddleLeft].Location = new Point(rec.Location.X - SelCornerSize, rec.Location.Y  + rec.Size.Height / 2 - SelCornerSize);
			SelectionCorner[(int) CornerPosition.MiddleCenter].Location = new Point(rec.Location.X + rec.Size.Width / 2 - SelCornerSize, rec.Location.Y + rec.Size.Height / 2 - SelCornerSize);
			SelectionCorner[(int) CornerPosition.MiddleRight].Location = new Point(rec.Location.X + rec.Size.Width - SelCornerSize, rec.Location.Y + rec.Size.Height / 2 - SelCornerSize);

		}

		CornerPosition IResizeController.HitTestCorner(Point p)
		{
			for(var i = 0; i < SelectionCorner.Length; i++)
			{
				var ctrl = ((IControllable) SelectionCorner[i]).GetController();
				if (ctrl.HitTest(p))
					return (CornerPosition) i;
			}
			return CornerPosition.Nothing;
		}

		void IResizeController.Start(Point posStart, CornerPosition corner)
		{
			SelCorner = corner;
			DragOffset.X = SelectionCorner[(int) SelCorner].Location.X - posStart.X;
			DragOffset.Y = SelectionCorner[(int) SelCorner].Location.Y - posStart.Y;
		}

		void IResizeController.Resize(Point posCurrent)
		{
			var corner = SelectionCorner[(int) SelCorner];
			Point loc;

			var dragPointEl = posCurrent;
			dragPointEl.Offset(DragOffset.X, DragOffset.Y);
			if (dragPointEl.X < 0) dragPointEl.X = 0;
			if (dragPointEl.Y < 0) dragPointEl.Y = 0;

			switch (SelCorner)
			{
				case CornerPosition.TopLeft:
					corner.Location = dragPointEl;
					loc = new Point(corner.Location.X + corner.Size.Width / 2, corner.Location.Y + corner.Size.Height / 2);
					El.Size = new Size(El.Size.Width + (El.Location.X - loc.X),
						El.Size.Height + (El.Location.Y - loc.Y));
					El.Location = loc;
					break;

				case CornerPosition.TopCenter:
					corner.Location = new Point(corner.Location.X, dragPointEl.Y);
					loc = new Point(corner.Location.X + corner.Size.Width / 2, corner.Location.Y + corner.Size.Height / 2);
					El.Size = new Size(El.Size.Width,
						El.Size.Height + (El.Location.Y - loc.Y));
					El.Location = new Point(El.Location.X, loc.Y);
					break;

				case CornerPosition.TopRight:
					corner.Location = dragPointEl;
					loc = new Point(corner.Location.X + corner.Size.Width / 2, corner.Location.Y + corner.Size.Height / 2);
					El.Size = new Size(loc.X - El.Location.X,
						El.Size.Height - (loc.Y - El.Location.Y));
					El.Location = new Point(El.Location.X, loc.Y);
					break;

				case CornerPosition.MiddleLeft:
					corner.Location = new Point(dragPointEl.X, corner.Location.Y);
					loc = new Point(corner.Location.X + corner.Size.Width / 2, corner.Location.Y + corner.Size.Height / 2);
					El.Size = new Size(El.Size.Width + (El.Location.X - loc.X),
						El.Size.Height);
					El.Location = new Point(loc.X, El.Location.Y);
					break;

				case CornerPosition.MiddleRight:
					corner.Location = new Point(dragPointEl.X, corner.Location.Y);
					loc = new Point(corner.Location.X + corner.Size.Width / 2, corner.Location.Y + corner.Size.Height / 2);
					El.Size = new Size(loc.X - El.Location.X,
						El.Size.Height);
					break;
							
				case CornerPosition.BottomLeft:
					corner.Location = dragPointEl;
					loc = new Point(corner.Location.X + corner.Size.Width / 2, corner.Location.Y + corner.Size.Height / 2);
					El.Size = new Size(El.Size.Width - (loc.X - El.Location.X),
						loc.Y - El.Location.Y);
					El.Location = new Point(loc.X, El.Location.Y);
					break;

				case CornerPosition.BottomCenter:
					corner.Location = new Point(corner.Location.X, dragPointEl.Y);
					loc = new Point(corner.Location.X + corner.Size.Width / 2, corner.Location.Y + corner.Size.Height / 2);
					El.Size = new Size(El.Size.Width,
						loc.Y - El.Location.Y);
					break;
							
				case CornerPosition.BottomRight:
					corner.Location = dragPointEl;
					loc = new Point(corner.Location.X + corner.Size.Width / 2, corner.Location.Y + corner.Size.Height / 2);
					El.Size = new Size(loc.X - El.Location.X,
						loc.Y - El.Location.Y);
					break;
			}
		}

		void IResizeController.End(Point posEnd)
		{
			if ((El.Size.Height < 0) || (El.Size.Width < 0))
			{
				var urec = El.GetUnsignedRectangle();
				El.Location = urec.Location;
				El.Size = urec.Size;
			}
			SelCorner = CornerPosition.Nothing;
			DragOffset = Point.Empty;
		}
		
		bool IResizeController.IsResizing
		{
			get
			{
				return (SelCorner != CornerPosition.Nothing);
			}
		}

		bool IResizeController.CanResize
		{
			get
			{
				return CanResize1;
			}
		}

		#endregion
	}
}
