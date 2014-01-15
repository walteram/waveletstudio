using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace DiagramNet.Elements.Controllers
{
	/// <summary>
	/// This class is the controller for RightAngleLinkElement
	/// </summary>
	internal class RightAngleLinkController: IMoveController, IResizeController, ILabelController
	{
		//parent element
		protected RightAngleLinkElement El;

		//Move vars.
		protected Point DragOffset = new Point(0);
		protected bool IsDragging;
		protected bool CanMove1 = true;

		//Resize vars.
		protected const int SelCornerSize = 3;
		protected RectangleElement[] SelectionCorner;
		protected CornerPosition SelCorner = CornerPosition.Nothing;
		protected bool CanResize1 = true;

		public RightAngleLinkController(RightAngleLinkElement element)
		{
			El = element;
			
			//Create corners
			if (El.LineElements.Length == 3)
			{
				SelectionCorner = new RectangleElement[1];
			    SelectionCorner[0] = new RectangleElement(0, 0, SelCornerSize*2, SelCornerSize*2)
			                             {BorderColor = Color.Black, FillColor1 = Color.White, FillColor2 = Color.Empty};
			}
			else
			{
				SelectionCorner = new RectangleElement[0];
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

		public bool HitTest(Point p)
		{
		    return El.LineElements.Select(l => ((IControllable) l).GetController()).Any(ctrl => ctrl.HitTest(p));
		}

	    bool IController.HitTest(Rectangle r)
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

		public void DrawSelection(Graphics g)
		{
			foreach(var l in El.LineElements)
			{
				var ctrl = ((IControllable) l).GetController();
				ctrl.DrawSelection(g);
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
		    if (SelectionCorner.Length != 1) return;
		    var elLinePoint1 = El.LineElements[1].Point1;
		    var elLinePoint2 = El.LineElements[1].Point2;
		    SelectionCorner[0].Location = new Point(elLinePoint1.X + ((elLinePoint2.X - elLinePoint1.X) / 2) - SelCornerSize,
		                                            elLinePoint1.Y + ((elLinePoint2.Y - elLinePoint1.Y) / 2) - SelCornerSize);
		}

		CornerPosition IResizeController.HitTestCorner(Point p)
		{
			if (SelectionCorner.Length == 1)
			{
				var ctrl = ((IControllable) SelectionCorner[0]).GetController();
				if (ctrl.HitTest(p))
				{
				    if (El.Orientation == Orientation.Horizontal)
						return CornerPosition.MiddleLeft;
				    return El.Orientation == Orientation.Vertical ? CornerPosition.TopCenter : CornerPosition.Undefined;
				}
			}
			return CornerPosition.Nothing;
		}

		void IResizeController.Start(Point posStart, CornerPosition corner)
		{
			SelCorner = corner;
			DragOffset.X = SelectionCorner[0].Location.X - posStart.X;
			DragOffset.Y = SelectionCorner[0].Location.Y - posStart.Y;
		}

		void IResizeController.Resize(Point posCurrent)
		{
			var corner = SelectionCorner[0];
			Point loc;

			var dragPointEl = posCurrent;
			dragPointEl.Offset(DragOffset.X, DragOffset.Y);
			if (dragPointEl.X < 0) dragPointEl.X = 0;
			if (dragPointEl.Y < 0) dragPointEl.Y = 0;

			if (El.Orientation == Orientation.Horizontal)
			{
				corner.Location = new Point(dragPointEl.X, corner.Location.Y);
				loc = new Point(corner.Location.X + corner.Size.Width / 2, corner.Location.Y + corner.Size.Height / 2);
				El.LineElements[1].Point1 = new Point(loc.X, El.LineElements[1].Point1.Y);
				El.LineElements[1].Point2 = new Point(loc.X, El.LineElements[1].Point2.Y);
			}
			else
			{
				corner.Location = new Point(corner.Location.X, dragPointEl.Y);
				loc = new Point(corner.Location.X + corner.Size.Width / 2, corner.Location.Y + corner.Size.Height / 2);
				El.LineElements[1].Point1 = new Point(El.LineElements[1].Point1.X, loc.Y);
				El.LineElements[1].Point2 = new Point(El.LineElements[1].Point2.X, loc.Y);
			}

			El.LineElements[0].Point2 = El.LineElements[1].Point1;
			El.LineElements[2].Point1 = El.LineElements[1].Point2;

			El.NeedCalcLink = true;
		}

		void IResizeController.End(Point posEnd)
		{
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

		#region IMoveController Members

		void IMoveController.Start(Point posStart)
		{
			DragOffset.X = El.Location.X - posStart.X;
			DragOffset.Y = El.Location.Y - posStart.Y;
			IsDragging = true;
		}

        bool IMoveController.WillMove(Point posCurrent)
        {
            if (!IsDragging) return false;
            var dragPointEl = posCurrent;
            dragPointEl.Offset(DragOffset.X, DragOffset.Y);
            if (dragPointEl.X < 0) dragPointEl.X = 0;
            if (dragPointEl.Y < 0) dragPointEl.Y = 0;

            return El.Location.X != dragPointEl.X || El.Location.Y != dragPointEl.Y;
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
	
		#region ILabelController Members

		public void SetLabelPosition()
		{
			var label = ((ILabelElement) El).Label;
            
			if (El.Lines.Length == 2)
			{
				label.Location = El.Lines[0].Point2;
			}
			else
				label.PositionBySite(El.Lines[1]);
		}

		#endregion
	}
}
