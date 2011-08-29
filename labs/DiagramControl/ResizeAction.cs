using System.Drawing;
using System.Windows.Forms;
using DiagramNet.Elements;
using DiagramNet.Elements.Controllers;
using DiagramNet.Events;


namespace DiagramNet
{
	/// <summary>
	/// This class control the size of elements
	/// </summary>
	internal class ResizeAction
	{
		public delegate void OnElementResizingDelegate(ElementEventArgs e);
		private OnElementResizingDelegate _onElementResizingDelegate;

	    private IResizeController _resizeCtrl;
		private Document _document;

	    public bool IsResizing { get; private set; }

	    public bool IsResizingLink
		{
			get
			{
				return ((_resizeCtrl != null) && (_resizeCtrl.OwnerElement is BaseLinkElement));
			}
		}

		public void Select(Document document)
		{
			_document = document;

			// Get Resize Controller
			if ((document.SelectedElements.Count == 1) && (document.SelectedElements[0] is IControllable))
			{	
				var ctrl = ((IControllable) document.SelectedElements[0]).GetController();
				if (ctrl is IResizeController)
				{
					ctrl.OwnerElement.Invalidate();

					_resizeCtrl = (IResizeController) ctrl;
					ShowResizeCorner(true);
				}
			}
			else
				_resizeCtrl = null;
		}

		public void Start(Point mousePoint, OnElementResizingDelegate onElementResizingDelegate)
		{
			IsResizing = false;

			if (_resizeCtrl == null) return;

			_onElementResizingDelegate = onElementResizingDelegate;

			_resizeCtrl.OwnerElement.Invalidate();

			var corPos = _resizeCtrl.HitTestCorner(mousePoint);

		    if (corPos == CornerPosition.Nothing) return;
		    //Events
		    var eventResizeArg = new ElementEventArgs(_resizeCtrl.OwnerElement);
		    onElementResizingDelegate(eventResizeArg);

		    _resizeCtrl.Start(mousePoint, corPos);

		    UpdateResizeCorner();

		    IsResizing = true;
		}

		public void Resize(Point dragPoint)
		{
		    if ((_resizeCtrl == null) || (!_resizeCtrl.CanResize)) return;
		    //Events
		    var eventResizeArg = new ElementEventArgs(_resizeCtrl.OwnerElement);
		    _onElementResizingDelegate(eventResizeArg);

		    _resizeCtrl.OwnerElement.Invalidate();

		    _resizeCtrl.Resize(dragPoint);

		    var lblCtrl = ControllerHelper.GetLabelController(_resizeCtrl.OwnerElement);
		    if (lblCtrl != null)
		        lblCtrl.SetLabelPosition();
		    else
		    {
		        if (_resizeCtrl.OwnerElement is ILabelElement)
		        {
		            var label = ((ILabelElement) _resizeCtrl.OwnerElement).Label;
		            label.PositionBySite(_resizeCtrl.OwnerElement);
		        }
		    }

		    UpdateResizeCorner();
		}

		public void End(Point posEnd)
		{
		    if (_resizeCtrl == null) return;
		    _resizeCtrl.OwnerElement.Invalidate();

		    _resizeCtrl.End(posEnd);

		    //Events
		    var eventResizeArg = new ElementEventArgs(_resizeCtrl.OwnerElement);
		    _onElementResizingDelegate(eventResizeArg);

		    IsResizing = false;
		}

		public void DrawResizeCorner(Graphics g)
		{
		    if (_resizeCtrl == null) return;
		    foreach(var r in _resizeCtrl.Corners)
		    {
		        if (_document.Action == DesignerAction.Select)
		        {
		            if (r.Visible) r.Draw(g);
		        }
		        else if (_document.Action == DesignerAction.Connect)
		        {
		            // if is Connect Mode, then resize only Links.
		            if (_resizeCtrl.OwnerElement is BaseLinkElement)
		                if (r.Visible) r.Draw(g);
		        }
		    }
		}

	    public void UpdateResizeCorner()
		{
			if (_resizeCtrl != null)
				_resizeCtrl.UpdateCornersPos();
		}

		public Cursor UpdateResizeCornerCursor(Point mousePoint)
		{
			if ((_resizeCtrl == null) || (!_resizeCtrl.CanResize)) return Cursors.Default;

			var corPos = _resizeCtrl.HitTestCorner(mousePoint);

			switch(corPos)
			{
				case CornerPosition.TopLeft:
					return Cursors.SizeNWSE;
				
				case CornerPosition.TopCenter:
					return Cursors.SizeNS;								

				case CornerPosition.TopRight:
					return Cursors.SizeNESW;								
					
				case CornerPosition.MiddleLeft:
				case CornerPosition.MiddleRight:
					return Cursors.SizeWE;

				case CornerPosition.BottomLeft:
					return Cursors.SizeNESW;
				
				case CornerPosition.BottomCenter:
					return Cursors.SizeNS;								

				case CornerPosition.BottomRight:
					return Cursors.SizeNWSE;
				default:
					return Cursors.Default;
			}			
			
		}

		public void ShowResizeCorner(bool show)
		{
		    if (_resizeCtrl == null) return;
		    var canResize = _resizeCtrl.CanResize;
		    foreach (var t in _resizeCtrl.Corners)
		    {
		        t.Visible = canResize && show;
		    }

		    if (_resizeCtrl.Corners.Length >= (int) CornerPosition.MiddleCenter)
		        _resizeCtrl.Corners[(int) CornerPosition.MiddleCenter].Visible = false;
		}
	}
}
