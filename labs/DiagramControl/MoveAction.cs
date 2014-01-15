using System.Drawing;
using DiagramNet.Elements;
using DiagramNet.Elements.Controllers;
using DiagramNet.Events;

namespace DiagramNet
{
    /// <summary>
    /// This class control the elements motion.
    /// </summary>	
    internal class MoveAction
    {
        public delegate void OnElementMovingDelegate(ElementEventArgs e);
        private OnElementMovingDelegate _onElementMovingDelegate;

        private IMoveController[] _moveCtrl;
        private Point _upperSelPoint = Point.Empty;
        private Point _upperSelPointDragOffset = Point.Empty;
        private Document _document;

        public bool IsMoving { get; private set; }

        public void Start(Point mousePoint, Document document, OnElementMovingDelegate onElementMovingDelegate)
        {
            _document = document;
            _onElementMovingDelegate = onElementMovingDelegate;

            // Get Controllers
            _moveCtrl = new IMoveController[document.SelectedElements.Count];
            var moveLabelCtrl = new IMoveController[document.SelectedElements.Count];
            for(var i = document.SelectedElements.Count - 1; i >= 0; i--)
            {
                _moveCtrl[i] = ControllerHelper.GetMoveController(document.SelectedElements[i]);
                
                if ((_moveCtrl[i] != null) && (_moveCtrl[i].CanMove))
                {
                    onElementMovingDelegate(new ElementEventArgs(document.SelectedElements[i]));
                    _moveCtrl[i].Start(mousePoint);
                    
                    //ILabelElement - Move Label inside the element
                    if ((document.SelectedElements[i] is ILabelElement) &&
                        (ControllerHelper.GetLabelController(document.SelectedElements[i]) == null))
                    {
                        var label = ((ILabelElement) document.SelectedElements[i]).Label;
                        moveLabelCtrl[i] = ControllerHelper.GetMoveController(label);
                        if ((moveLabelCtrl[i] != null) && (moveLabelCtrl[i].CanMove))
                            moveLabelCtrl[i].Start(mousePoint);
                        else
                            moveLabelCtrl[i] = null;
                    }
                }
                else
                    _moveCtrl[i] = null;
            }

            _moveCtrl = (IMoveController[]) DiagramUtil.ArrayHelper.Append(_moveCtrl, moveLabelCtrl);
            _moveCtrl = (IMoveController[]) DiagramUtil.ArrayHelper.Shrink(_moveCtrl, null);

            // Can't move only links
            var isOnlyLink = true;
            foreach (var ctrl in _moveCtrl)
            {
                // Verify
                if (ctrl == null) continue;
                ctrl.OwnerElement.Invalidate();

                if ((ctrl.OwnerElement is BaseLinkElement) || (ctrl.OwnerElement is LabelElement)) continue;
                isOnlyLink = false;
                break;
            }
            if (isOnlyLink)
            {
                //End Move the Links
                foreach (var ctrl in _moveCtrl)
                {
                    if (ctrl !=null)
                        ctrl.End();
                }
                _moveCtrl = new IMoveController[] {null};
            }

            //Upper selecion point controller
            UpdateUpperSelectionPoint();
            _upperSelPointDragOffset.X = _upperSelPoint.X - mousePoint.X;
            _upperSelPointDragOffset.Y = _upperSelPoint.Y - mousePoint.Y;

            IsMoving = true;
        }

        public void Move(Point dragPoint)
        {
            //Upper selecion point controller
            var dragPointEl = dragPoint;
            dragPointEl.Offset(_upperSelPointDragOffset.X, _upperSelPointDragOffset.Y);
                    
            _upperSelPoint = dragPointEl;
                    
            if (dragPointEl.X < 0) dragPointEl.X = 0;
            if (dragPointEl.Y < 0) dragPointEl.Y = 0;

            //Move Controller
            if (dragPointEl.X == 0) dragPoint.X = dragPoint.X - _upperSelPoint.X;					
            if (dragPointEl.Y == 0) dragPoint.Y = dragPoint.Y - _upperSelPoint.Y;

            foreach (var ctrl in _moveCtrl)
            {
                if (ctrl == null || !ctrl.WillMove(dragPoint))
                    continue;

                ctrl.OwnerElement.Invalidate();
                _onElementMovingDelegate(new ElementEventArgs(ctrl.OwnerElement));
                ctrl.Move(dragPoint);
                if (ctrl.OwnerElement is NodeElement)
                {
                    UpdateLinkPosition((NodeElement)ctrl.OwnerElement);
                }
                var lblCtrl = ControllerHelper.GetLabelController(ctrl.OwnerElement);
                if (lblCtrl != null)
                    lblCtrl.SetLabelPosition();                
            }
        }

        public void End()
        {
            _upperSelPoint = Point.Empty;
            _upperSelPointDragOffset = Point.Empty;
                
//			ElementEventArgs eventClickArg = new ElementEventArgs(selectedElement);
//			OnElementClick(eventClickArg);

            foreach(var ctrl in _moveCtrl)
            {
                if (ctrl == null) 
                    continue;
                
                if (ctrl.OwnerElement is NodeElement)
                {
                    UpdateLinkPosition((NodeElement) ctrl.OwnerElement);
                }

                ctrl.End();

                _onElementMovingDelegate(new ElementEventArgs(ctrl.OwnerElement));
            }
            

            IsMoving = false;

//			ElementMouseEventArgs eventMouseUpArg = new ElementMouseEventArgs(selectedElement, e.X, e.Y);
//			OnElementMouseUp(eventMouseUpArg);
        }

        private void UpdateUpperSelectionPoint()
        {
            //Get upper selecion point
            var points = new Point[_document.SelectedElements.Count];
            var p = 0;
            foreach(BaseElement el in _document.SelectedElements)
            {
                points[p] = el.Location;
                p++;
            }
            _upperSelPoint = DiagramUtil.GetUpperPoint(points);
        }

        private void UpdateLinkPosition(NodeElement node)
        {
            foreach(var conn in node.Connectors)
            {
                foreach (BaseElement el in conn.Links)
                {
                    var lnk = (BaseLinkElement) el;
                    var ctrl = ((IControllable) lnk).GetController();
                    if (ctrl is IMoveController)
                    {
                        var mctrl = (IMoveController) ctrl;
                        if (!mctrl.IsMoving) lnk.NeedCalcLink = true;
                    }
                    else lnk.NeedCalcLink = true;

                    if (!(lnk is ILabelElement)) continue;
                    var label = ((ILabelElement) lnk).Label;

                    var lblCtrl = ControllerHelper.GetLabelController(lnk);
                    if (lblCtrl != null)
                        lblCtrl.SetLabelPosition();
                    else
                    {
                        label.PositionBySite(lnk);
                    }		
                    label.Invalidate();
                }
            }
        }
    }
}
