using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;
using System.Security.Permissions;
using DiagramNet.Elements;
using DiagramNet.Elements.Controllers;
using DiagramNet.Events;

namespace DiagramNet
{
    /// <summary>
    /// This class control the elements collection and visualization.
    /// </summary>
    [Serializable]
    public class Document: IDeserializationCallback 
    {
        //Draw properties
        private SmoothingMode _smoothingMode = SmoothingMode.HighQuality;
        private PixelOffsetMode _pixelOffsetMode = PixelOffsetMode.Default;
        private CompositingQuality _compositingQuality = CompositingQuality.AssumeLinear;

        //Action
        private DesignerAction _action = DesignerAction.Select;
        private ElementType _elementType = ElementType.RectangleNode;
        private LinkType _linkType = LinkType.RightAngle;

        // Element Collection

        // Selections Collections

        //Document Size
        private Point _location = new Point(100, 100);
        private Size _windowSize = new Size(0, 0);

        //Zoom
        private float _zoom = 1.0f;

        //Grig
        private Size _gridSize = new Size(50, 50);

        //Events
        private bool _canFireEvents = true;

        public Document()
        {
            SelectedNodes = new ElementCollection();
            SelectedElements = new ElementCollection();
            Elements = new ElementCollection();
        }

        #region Add Methods
        public void AddElement(BaseElement el)
        {
            Elements.Add(el);
            el.AppearanceChanged += ElementAppearanceChanged;
            OnAppearancePropertyChanged(new EventArgs());
        }

        public void AddElements(ElementCollection els)
        {
            AddElements(els.GetArray());
        }

        public void AddElements(BaseElement[] els)
        {
            Elements.EnabledCalc = false;
            foreach (var el in els)
            {
                AddElement(el);
            }
            Elements.EnabledCalc = true;
        }

        internal bool CanAddLink(ConnectorElement connStart, ConnectorElement connEnd)
        {
            return ((connStart != connEnd) && (connStart.ParentElement != connEnd.ParentElement));
        }

        public BaseLinkElement AddLink(ConnectorElement connStart, ConnectorElement connEnd)
        {
            if (CanAddLink(connStart, connEnd))
            {
                BaseLinkElement lnk;
                
                if (_linkType == LinkType.Straight)
                    lnk = new StraightLinkElement(connStart, connEnd);
                else // (linkType == LinkType.RightAngle)
                    lnk = new RightAngleLinkElement(connStart, connEnd);

                Elements.Add(lnk);
                lnk.AppearanceChanged += ElementAppearanceChanged;
                OnAppearancePropertyChanged(new EventArgs());
                return lnk;
            }
            return null;
        }

        #endregion

        #region Delete Methods

        public void DeleteElement(BaseElement el)
        {
            if ((el == null) || (el is ConnectorElement)) return;
            //Delete link
            if (el is BaseLinkElement)
            {
                var lnk = (BaseLinkElement) el;
                DeleteLink(lnk);
                return;
            }

            //Delete node
            if (el is NodeElement)
            {
                var conn = ((NodeElement) el);
                foreach (var elconn in conn.Connectors)
                {
                    for (var i = elconn.Links.Count - 1; i>=0; i--)
                    {
                        var lnk = (BaseLinkElement) elconn.Links[i];
                        DeleteLink(lnk);
                    }
                }
                    
                if (SelectedNodes.Contains(el))
                    SelectedNodes.Remove(el);
            }

            if (SelectedElements.Contains(el))
                SelectedElements.Remove(el);

            Elements.Remove(el);
                
            OnAppearancePropertyChanged(new EventArgs());
        }

        public void DeleteElement(Point p)
        {
            var selectedElement = FindElement(p);
            DeleteElement(selectedElement);
        }

        public void DeleteSelectedElements()
        {
            SelectedElements.EnabledCalc = false;
            SelectedNodes.EnabledCalc = false;

            for(var i = SelectedElements.Count - 1; i >= 0; i-- )
            {
                DeleteElement(SelectedElements[i]);
            }

            SelectedElements.EnabledCalc = true;
            SelectedNodes.EnabledCalc = true;
        }

        public void DeleteLink(BaseLinkElement lnk, bool callHandler = true)
        {
            if (lnk == null) return;
            lnk.Connector1.RemoveLink(lnk);
            lnk.Connector2.RemoveLink(lnk);
                            
            if (Elements.Contains(lnk))
                Elements.Remove(lnk);
            if (SelectedElements.Contains(lnk))
                SelectedElements.Remove(lnk);
            if (callHandler)
                OnLinkRemoved(new ElementEventArgs(lnk));
            OnAppearancePropertyChanged(new EventArgs());
        }
        #endregion

        #region Select Methods
        public void ClearSelection()
        {
            SelectedElements.Clear();
            SelectedNodes.Clear();
            OnElementSelection(this, new ElementSelectionEventArgs(SelectedElements));
        }

        public void SelectElement(BaseElement el)
        {
            SelectedElements.Add(el);
            if (el is NodeElement)
            {
                SelectedNodes.Add(el);
            }
            if (_canFireEvents)
                OnElementSelection(this, new ElementSelectionEventArgs(SelectedElements));
        }

        public void SelectElements(BaseElement[] els)
        {
            SelectedElements.EnabledCalc = false;
            SelectedNodes.EnabledCalc = false;

            _canFireEvents = false;
            
            try
            {
                foreach(var el in els)
                {
                    SelectElement(el);
                }
            }
            finally
            {
                _canFireEvents = true;
            }
            SelectedElements.EnabledCalc = true;
            SelectedNodes.EnabledCalc = true;
            
            OnElementSelection(this, new ElementSelectionEventArgs(SelectedElements));
        }

        public void SelectElements(Rectangle selectionRectangle)
        {
            SelectedElements.EnabledCalc = false;
            SelectedNodes.EnabledCalc = false;
            
            // Add all "hitable" elements
            foreach(BaseElement element in Elements)
            {
                if (!(element is IControllable)) continue;
                var ctrl = ((IControllable)element).GetController();
                if (!ctrl.HitTest(selectionRectangle)) continue;
                if (!(element is ConnectorElement))
                    SelectedElements.Add(element);
                        
                if (element is NodeElement)
                    SelectedNodes.Add(element);
            }

            //if the seleciont isn't a expecific link, remove links
            // without 2 elements in selection
            if (SelectedElements.Count > 1)
            {
                foreach(BaseElement el in Elements)
                {
                    var lnk = el as BaseLinkElement;
                    if (lnk == null) continue;
                    
                    if ((!SelectedElements.Contains(lnk.Connector1.ParentElement)) ||
                        (!SelectedElements.Contains(lnk.Connector2.ParentElement)))
                    {
                        SelectedElements.Remove(lnk);
                    }
                }
            }

            SelectedElements.EnabledCalc = true;
            SelectedNodes.EnabledCalc = true;
            
            OnElementSelection(this, new ElementSelectionEventArgs(SelectedElements));
        }

        public void SelectAllElements()
        {
            SelectedElements.EnabledCalc = false;
            SelectedNodes.EnabledCalc = false;

            foreach(BaseElement element in Elements)
            {
                if (!(element is ConnectorElement))
                    SelectedElements.Add(element);
                    
                if (element is NodeElement)
                    SelectedNodes.Add(element);
            }

            SelectedElements.EnabledCalc = true;
            SelectedNodes.EnabledCalc = true;
            
        }

        public BaseElement FindElement(Point point)
        {
            if ((Elements != null) && (Elements.Count > 0))
            {
                // First, find elements
                BaseElement el;
                for(var i = Elements.Count - 1; i >=0 ; i--)
                {
                    el = Elements[i];

                    if (el is BaseLinkElement)
                        continue;

                    //Find element in a Connector array
                    IController ctrl;
                    if (el is NodeElement)
                    {
                        var nel = (NodeElement) el;
                        foreach(var cel in nel.Connectors)
                        {
                            ctrl = ((IControllable) cel).GetController();
                            if (ctrl.HitTest(point))
                                return cel;
                        }
                    }

                    //Find element in a Container Element
                    if (el is IContainer)
                    {
                        var inner = FindInnerElement((IContainer) el, point);
                        if (inner != null)
                            return inner;
                    }

                    //Find element by hit test
                    if (!(el is IControllable)) continue;
                    ctrl = ((IControllable) el).GetController();
                    if (ctrl.HitTest(point))
                        return el;
                }

                // Then, find links
                for(var i = Elements.Count - 1; i >=0 ; i--)
                {
                    el = Elements[i];

                    if (!(el is BaseLinkElement))
                        continue;

                    if (!(el is IControllable)) continue;
                    var ctrl = ((IControllable) el).GetController();
                    if (ctrl.HitTest(point))
                        return el;
                } 
            }
            return null;
        }

        private BaseElement FindInnerElement(IContainer parent, Point hitPos)
        {
            foreach (BaseElement el in parent.Elements)
            {
                if (el is IContainer)
                {
                    var retEl = FindInnerElement((IContainer)el, hitPos);
                    if (retEl != null)
                        return retEl;
                }

                if (!(el is IControllable)) continue;
                var ctrl = ((IControllable) el).GetController();

                if (ctrl.HitTest(hitPos))
                    return el;
            }
            return null;
        }
        #endregion

        #region Position Methods
        public void MoveUpElement(BaseElement el)
        {
            var i = Elements.IndexOf(el);
            if (i == Elements.Count - 1) return;
            Elements.ChangeIndex(i, i + 1);
            OnAppearancePropertyChanged(new EventArgs());
        }

        public void MoveDownElement(BaseElement el)
        {
            var i = Elements.IndexOf(el);
            if (i == 0) return;
            Elements.ChangeIndex(i, i - 1);
            OnAppearancePropertyChanged(new EventArgs());
        }

        public void BringToFrontElement(BaseElement el)
        {
            var i = Elements.IndexOf(el);
            for (var x = i + 1; x <= Elements.Count - 1; x++)
            {
                Elements.ChangeIndex(i, x);
                i = x;
            }
            OnAppearancePropertyChanged(new EventArgs());
        }

        public void SendToBackElement(BaseElement el)
        {
            var i = Elements.IndexOf(el);
            for (var x = i - 1; x >= 0; x--)
            {
                Elements.ChangeIndex(i, x);
                i = x;
            }
            OnAppearancePropertyChanged(new EventArgs());
        }
        #endregion

        internal void CalcWindow(bool forceCalc)
        {
            Elements.CalcWindow(forceCalc);
            SelectedElements.CalcWindow(forceCalc);
            SelectedNodes.CalcWindow(forceCalc);
        }

        #region Properties

        public ElementCollection Elements { get; private set; }

        public ElementCollection SelectedElements { get; private set; }

        public ElementCollection SelectedNodes { get; private set; }

        public Point Location
        {
            get
            {
                return Elements.WindowLocation;
            }
        }

        public Size Size
        {
            get
            {
                return Elements.WindowSize;
            }
        }

        internal Size WindowSize
        {
            set
            {
                _windowSize = value;
            }
        }

        public SmoothingMode SmoothingMode
        {
            get
            {
                return _smoothingMode;
            }
            set
            {
                _smoothingMode = value;
                OnAppearancePropertyChanged(new EventArgs());
            }
        }

        public PixelOffsetMode PixelOffsetMode
        {
            get
            {
                return _pixelOffsetMode;
            }
            set
            {
                _pixelOffsetMode = value;
                OnAppearancePropertyChanged(new EventArgs());
            }
        }

        public CompositingQuality CompositingQuality
        {
            get
            {
                return _compositingQuality;
            }
            set
            {
                _compositingQuality = value;
                OnAppearancePropertyChanged(new EventArgs());
            }
        }

        public DesignerAction Action
        {
            get
            {
                return _action;
            }
            set
            {
                _action = value;
                OnPropertyChanged(new EventArgs());
            }
        }

        public float Zoom
        {
            get
            {
                return _zoom;
            }
            set
            {
                if (value < 0.1f)
                    value = 0.1f;
                var changed = Math.Abs(_zoom - value) > float.Epsilon;
                _zoom = value;
                GridSize = new Size(Convert.ToInt32(10*value), Convert.ToInt32(10*value));
                OnPropertyChanged(new EventArgs());
                if (changed && ZoomChanged != null)
                    ZoomChanged(this, new EventArgs());
            }
        }

        public ElementType ElementType
        {
            get
            {
                return _elementType;
            }
            set
            {
                _elementType = value;
                OnPropertyChanged(new EventArgs());
            }
        }

        public LinkType LinkType
        {
            get
            {
                return _linkType;
            }
            set
            {
                _linkType = value;
                OnPropertyChanged(new EventArgs());
            }
        }

        public Size GridSize
        {
            get
            {
                return _gridSize;
            }
            set
            {
                _gridSize = value;
                OnAppearancePropertyChanged(new EventArgs());
            }
        }

        #endregion

        #region Draw Methods

        public void DrawElementsToGraphics(Graphics g, Rectangle? clippingRegion)
        {
            //Draw Links first
            for (var i = 0; i <= Elements.Count - 1; i++)
            {
                var el = Elements[i];
                if ((el is BaseLinkElement) && (clippingRegion == null || NeedDrawElement(el, clippingRegion.Value)))
                    el.Draw(g);
                if (el is ILabelElement)
                    ((ILabelElement)el).Label.Draw(g);
            }

            //Draw the other elements
            for (var i = 0; i <= Elements.Count - 1; i++)
            {
                var el = Elements[i];
                if ((el is BaseLinkElement) || (clippingRegion != null && !NeedDrawElement(el, clippingRegion.Value))) continue;
                if (el is NodeElement)
                {
                    var n = (NodeElement)el;
                    n.Draw(g, (_action == DesignerAction.Connect));
                }
                else
                {
                    el.Draw(g);
                }

                if (el is ILabelElement)
                    ((ILabelElement)el).Label.Draw(g);
            }
        }

        public Rectangle DrawElements(Graphics g, Rectangle clippingRegion)
        {
            var point = new Rectangle();
            var pointWrited = false;

            //Draw Links first
            for (var i = 0; i <= Elements.Count - 1; i++)
            {
                var el = Elements[i];
                if ((el is BaseLinkElement) && (NeedDrawElement(el, clippingRegion)))
                    el.Draw(g);
                                            
                if (el is ILabelElement)
                    ((ILabelElement) el).Label.Draw(g);

                if (!pointWrited)
                {
                    point.X = el.Location.X;
                    point.Y = el.Location.Y;
                    point.Width = el.Location.X + el.Size.Width + 4;
                    point.Height = el.Location.Y + el.Size.Height + 1;
                    pointWrited = true;
                }
                if (el.Location.X < point.X)
                    point.X = el.Location.X;
                if (el.Location.Y < point.Y)
                    point.Y = el.Location.Y;
                if (el.Location.X + el.Size.Width > point.Width)
                    point.Width = el.Location.X + el.Size.Width + 4;
                if (el.Location.Y + el.Size.Height > point.Height)
                    point.Height = el.Location.Y + el.Size.Height + 1;
            }

            //Draw the other elements
            for (var i = 0; i <= Elements.Count - 1; i++)
            {
                var el = Elements[i];
                if ((el is BaseLinkElement) || (!NeedDrawElement(el, clippingRegion))) continue;
                if (el is NodeElement)
                {
                    var n = (NodeElement) el;
                    n.Draw(g, (_action == DesignerAction.Connect));
                }
                else
                {
                    el.Draw(g);
                }

                if (el is ILabelElement)
                    ((ILabelElement) el).Label.Draw(g);

                if (!pointWrited)
                {
                    point.X = el.Location.X;
                    point.Y = el.Location.Y;
                    pointWrited = true;
                }
                if (el.Location.X < point.X)
                    point.X = el.Location.X;
                if (el.Location.Y < point.Y)
                    point.Y = el.Location.Y;
            }
            return point;
        }

        public Rectangle GetArea()
        {
            var area = new Rectangle();
            var pointWrited = false;

            //Links first
            for (var i = 0; i <= Elements.Count - 1; i++)
            {
                var el = Elements[i];                
                if (!pointWrited)
                {
                    area.X = el.Location.X;
                    area.Y = el.Location.Y;
                    area.Width = el.Location.X + el.Size.Width + 4;
                    area.Height = el.Location.Y + el.Size.Height + 1;
                    pointWrited = true;
                }
                if (el.Location.X < area.X)
                    area.X = el.Location.X;
                if (el.Location.Y < area.Y)
                    area.Y = el.Location.Y;
                if (el.Location.X + el.Size.Width > area.Width)
                    area.Width = el.Location.X + el.Size.Width + 4;
                if (el.Location.Y + el.Size.Height > area.Height)
                    area.Height = el.Location.Y + el.Size.Height + 1;
            }
            return area;
        }

        private bool NeedDrawElement(BaseElement el, Rectangle clippingRegion)
        {
            if (!el.Visible) return false;

            var elRectangle = el.GetUnsignedRectangle();
            elRectangle.Inflate(5, 5);
            return clippingRegion.IntersectsWith(elRectangle);
        }

        internal void DrawSelections(Graphics g, Rectangle clippingRegion)
        {
            for(var i = SelectedElements.Count - 1; i >=0 ; i--)
            {
                if (!(SelectedElements[i] is IControllable)) continue;
                var ctrl = ((IControllable) SelectedElements[i]).GetController();
                ctrl.DrawSelection(g);

                if (!(SelectedElements[i] is BaseLinkElement)) continue;
                var link = (BaseLinkElement) SelectedElements[i];
                ctrl = ((IControllable) link.Connector1).GetController();
                ctrl.DrawSelection(g);

                ctrl = ((IControllable) link.Connector2).GetController();
                ctrl.DrawSelection(g);
            }
        }

        internal void DrawGrid(Graphics g, Rectangle clippingRegion)
        {
            DrawGrid(g, clippingRegion, _gridSize);
        }

        internal void DrawGrid(Graphics g, Rectangle clippingRegion, Size gridSize)
        {
            var p = new Pen(new HatchBrush(HatchStyle.DarkUpwardDiagonal, Color.LightGray, Color.Transparent), 1);
            var maxX = _location.X + Size.Width;
            var maxY = _location.Y + Size.Height;

            if (_windowSize.Width / _zoom > maxX)
                maxX = (int)(_windowSize.Width / _zoom);

            if (_windowSize.Height / _zoom > maxY)
                maxY = (int)(_windowSize.Height / _zoom);

            for (var i = 0; i < maxX; i += gridSize.Width)
            {
                g.DrawLine(p, i, 0, i, maxY);
            }

            for (var i = 0; i < maxY; i += gridSize.Height)
            {
                g.DrawLine(p, 0, i, maxX, i);
            }

            p.Dispose();
        }
        #endregion

        #region Events Raising
        
        // Property Changed
        [field: NonSerialized]
        public event EventHandler PropertyChanged;

        // Zoom Changed
        [field: NonSerialized]
        public event EventHandler ZoomChanged; 

        protected virtual void OnPropertyChanged(EventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        // Appearance Property Changed
        [field: NonSerialized]
        public event EventHandler AppearancePropertyChanged;

        protected virtual void OnAppearancePropertyChanged(EventArgs e)
        {
            OnPropertyChanged(e);

            if (AppearancePropertyChanged != null)
                AppearancePropertyChanged(this, e);
            
        }

        public delegate void ElementEventHandler(object sender, ElementEventArgs e);
        
        // Appearance Property Changed
        [field: NonSerialized]
        public event ElementEventHandler LinkRemoved;

        protected virtual void OnLinkRemoved(ElementEventArgs e)
        {
            if (LinkRemoved != null)
                LinkRemoved(this, e);
            
        }

        // Element Property Changed
        [field: NonSerialized]
        public event EventHandler ElementPropertyChanged;

        protected virtual void OnElementPropertyChanged(object sender, EventArgs e)
        {
            if (ElementPropertyChanged != null)
                ElementPropertyChanged(sender, e);
        }

        // Element Selection
        public delegate void ElementSelectionEventHandler(object sender, ElementSelectionEventArgs e);
        
        [field: NonSerialized]
        public event ElementSelectionEventHandler ElementSelection;

        protected virtual void OnElementSelection(object sender, ElementSelectionEventArgs e)
        {
            if (ElementSelection != null)
                ElementSelection(sender, e);
        }
        

        #endregion

        #region Events Handling
        private void RecreateEventsHandlers()
        {
            foreach(BaseElement el in Elements)
                el.AppearanceChanged += ElementAppearanceChanged;			
        }

        [SecurityPermissionAttribute(SecurityAction.Demand,SerializationFormatter=true)]
        private void ElementAppearanceChanged(object sender, EventArgs e)
        {
            OnElementPropertyChanged(sender, e);
        }
        #endregion
    
        #region IDeserializationCallback Members
        void IDeserializationCallback.OnDeserialization(object sender)
        {
            RecreateEventsHandlers();
        }
        #endregion

    }
}
