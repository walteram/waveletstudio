using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;
using DiagramNet.Elements;
using DiagramNet.Elements.Controllers;
using DiagramNet.Events;

namespace DiagramNet
{
	public class Designer : UserControl
	{
		private readonly System.ComponentModel.IContainer components;

		#region Designer Control Initialization
		//Document
		private Document _document = new Document();

		// Drag and Drop
        private MoveAction _moveAction;

		// Selection
		BaseElement _selectedElement;
		private bool _isMultiSelection;
		private readonly RectangleElement _selectionArea = new RectangleElement(0,0,0,0);
		private IController[] _controllers;
		private BaseElement _mousePointerElement;
	
		// Resize
		private ResizeAction _resizeAction;

		// Add Element
		private bool _isAddSelection;
		
		// Link
		private bool _isAddLink;
		private ConnectorElement _connStart;
		private ConnectorElement _connEnd;
		private BaseLinkElement _linkLine;

		// Label
		private bool _isEditLabel;
	    private readonly TextBox _labelTextBox = new TextBox();
		private EditLabelAction _editLabelAction;

		//Undo
		[NonSerialized]
		private readonly UndoManager _undo = new UndoManager(5);
		private bool _changed;

		public Designer(System.ComponentModel.IContainer components)
		{
			this.components = components;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// This change control to not flick
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.ResizeRedraw, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.DoubleBuffer, true);
			
			// Selection Area Properties
			_selectionArea.Opacity = 40;
			_selectionArea.FillColor1 = SystemColors.Control;
			_selectionArea.FillColor2 = Color.Empty;
			_selectionArea.BorderColor = SystemColors.Control;

			// Link Line Properties
			//linkLine.BorderColor = Color.FromArgb(127, Color.DarkGray);
			//linkLine.BorderWidth = 4;

			// Label Edit
			_labelTextBox.BorderStyle = BorderStyle.FixedSingle;
			_labelTextBox.Multiline = true;
			_labelTextBox.Hide();
			Controls.Add(_labelTextBox);

			//EventsHandlers
			RecreateEventsHandlers();
		}
		#endregion

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// Designer
			// 
			this.AutoScroll = true;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.Name = "Designer";

		}
		#endregion

		public new void Invalidate()
		{
			if (_document.Elements.Count > 0)
			{
				for (var i = 0; i <= _document.Elements.Count - 1; i++)
				{
					var el = _document.Elements[i];

					Invalidate(el);

					if (el is ILabelElement)
						Invalidate(((ILabelElement) el).Label);
				}
			}
			else
				base.Invalidate();

			if ((_moveAction != null) && (_moveAction.IsMoving))
				AutoScrollMinSize = new Size((int) ((_document.Location.X + _document.Size.Width) * _document.Zoom), (int) ((_document.Location.Y + _document.Size.Height) * _document.Zoom));

		}

	    private void Invalidate(BaseElement el, bool force = false)
		{
			if (el == null) return;

		    if ((!force) && (!el.IsInvalidated)) return;
		    var invalidateRec = Goc2Gsc(el.InvalidateRec);
		    invalidateRec.Inflate(10, 10);
		    Invalidate(invalidateRec);
		}

		#region Events Overrides
		protected override void OnPaint(PaintEventArgs e)
		{
			var g = e.Graphics;
		    g.PageUnit = GraphicsUnit.Pixel;

			var scrollPoint = AutoScrollPosition;
			g.TranslateTransform(scrollPoint.X, scrollPoint.Y);

			//Zoom
			var mtx = g.Transform;
			var gc = g.BeginContainer();
			
			g.SmoothingMode = _document.SmoothingMode;
			g.PixelOffsetMode = _document.PixelOffsetMode;
			g.CompositingQuality = _document.CompositingQuality;
			
			g.ScaleTransform(_document.Zoom, _document.Zoom);

			var clipRectangle = Gsc2Goc(e.ClipRectangle);

			_document.DrawElements(g, clipRectangle);

			if (!((_resizeAction != null) && (_resizeAction.IsResizing)))
				_document.DrawSelections(g, e.ClipRectangle);

			if ((_isMultiSelection) || (_isAddSelection))
				DrawSelectionRectangle(g);
 
			if (_isAddLink)
			{
				_linkLine.CalcLink();
				_linkLine.Draw(g);
			}
			if ((_resizeAction != null) && ( !((_moveAction != null) && (_moveAction.IsMoving))))
				_resizeAction.DrawResizeCorner(g);

			if (_mousePointerElement != null)
			{
				if (_mousePointerElement is IControllable)
				{
					var ctrl = ((IControllable) _mousePointerElement).GetController();
					ctrl.DrawSelection(g);
				}
			}

			g.EndContainer(gc);
			g.Transform = mtx;

			base.OnPaint(e);

		}

		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground (e);

			var g = e.Graphics;
		    g.PageUnit = GraphicsUnit.Pixel;
			var mtx = g.Transform;
			var gc = g.BeginContainer();
			
			var clipRectangle = Gsc2Goc(e.ClipRectangle);
			
			_document.DrawGrid(g, clipRectangle);

			g.EndContainer(gc);
			g.Transform = mtx;

		}


		protected override void OnKeyDown(KeyEventArgs e)
		{
			//Delete element
			if (e.KeyCode == Keys.Delete)
			{
				DeleteSelectedElements();
				EndGeneralAction();
				base.Invalidate();
			}

			//Undo
			if (e.Control && e.KeyCode == Keys.Z)
			{
				if (_undo.CanUndo)
					Undo();
			}

			//Copy
			if ((e.Control) && (e.KeyCode == Keys.C))
			{
				Copy();
			}

			//Paste
			if ((e.Control) && (e.KeyCode == Keys.V))
			{
				Paste();
			}

			//Cut
			if ((e.Control) && (e.KeyCode == Keys.X))
			{
				Cut();
			}

			base.OnKeyDown (e);
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize (e);
			_document.WindowSize = Size;
		}

		#region Mouse Events
		protected override void OnMouseDown(MouseEventArgs e)
		{
			Point mousePoint;

			//ShowSelectionCorner((document.Action==DesignerAction.Select));

			switch (_document.Action)
			{
				// SELECT
				case DesignerAction.Connect:
				case DesignerAction.Select:
					if (e.Button == MouseButtons.Left)
					{
						mousePoint = Gsc2Goc(new Point(e.X, e.Y));
						
						//Verify resize action
						StartResizeElement(mousePoint);
						if ((_resizeAction != null) && (_resizeAction.IsResizing)) break;

						//Verify label editing
						if (_isEditLabel)
						{
							EndEditLabel();
						}

						// Search element by click
						_selectedElement = _document.FindElement(mousePoint);	
						
						if (_selectedElement != null)
						{
							//Events
							var eventMouseDownArg = new ElementMouseEventArgs(_selectedElement, e.X, e.Y);
							OnElementMouseDown(eventMouseDownArg);

							// Double-click to edit Label
							if ((e.Clicks == 2) && (_selectedElement is ILabelElement))
							{
							    StartEditLabel();
								break;
							}

							// Element selected
							if (_selectedElement is ConnectorElement)
							{
								StartAddLink((ConnectorElement) _selectedElement, mousePoint);
								_selectedElement = null;
							}
							else
								StartSelectElements(_selectedElement, mousePoint);
						}
						else
						{
							// If click is on neutral area, clear selection
							_document.ClearSelection();
							var p = Gsc2Goc(new Point(e.X, e.Y));
						    _isMultiSelection = true;
							_selectionArea.Visible = true;
							_selectionArea.Location = p;
							_selectionArea.Size = new Size(0, 0);
							
							if (_resizeAction != null)
								_resizeAction.ShowResizeCorner(false);
						}
						base.Invalidate();
					}
					break;

				// ADD
				case DesignerAction.Add:

					if (e.Button == MouseButtons.Left)
					{
						mousePoint = Gsc2Goc(new Point(e.X, e.Y));
						StartAddElement(mousePoint);
					}
					break;

				// DELETE
				case DesignerAction.Delete:
					if (e.Button == MouseButtons.Left)
					{
						mousePoint = Gsc2Goc(new Point(e.X, e.Y));
						DeleteElement(mousePoint);
					}					
					break;
			}
			
			base.OnMouseDown (e);
		
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{

			if (e.Button == MouseButtons.None)
			{
				Cursor = Cursors.Arrow;
				var mousePoint = Gsc2Goc(new Point(e.X, e.Y));

				if ((_resizeAction != null)
					&& ((_document.Action == DesignerAction.Select)				
						|| ((_document.Action == DesignerAction.Connect)
							&& (_resizeAction.IsResizingLink))))
				{
					Cursor = _resizeAction.UpdateResizeCornerCursor(mousePoint);
				}
				
				if (_document.Action == DesignerAction.Connect)
				{
					var mousePointerElementTmp = _document.FindElement(mousePoint);
					if (_mousePointerElement != mousePointerElementTmp)
					{
						if (mousePointerElementTmp is ConnectorElement)
						{
							_mousePointerElement = mousePointerElementTmp;
							_mousePointerElement.Invalidate();
							Invalidate(_mousePointerElement, true);
						}
						else if (_mousePointerElement != null)
						{
							_mousePointerElement.Invalidate();
							Invalidate(_mousePointerElement, true);
							_mousePointerElement = null;
						}
						
					}
				}
				else
				{
					Invalidate(_mousePointerElement, true);
					_mousePointerElement = null;
				}
			}			

			if (e.Button == MouseButtons.Left)
			{
				var dragPoint = Gsc2Goc(new Point(e.X, e.Y));

				if ((_resizeAction != null) && (_resizeAction.IsResizing))
				{
					_resizeAction.Resize(dragPoint);
					Invalidate();					
				}

				if ((_moveAction != null) && (_moveAction.IsMoving))
				{
					_moveAction.Move(dragPoint);
					Invalidate();
				}
				
				if ((_isMultiSelection) || (_isAddSelection))
				{
					var p = Gsc2Goc(new Point(e.X, e.Y));
					_selectionArea.Size = new Size (p.X - _selectionArea.Location.X, p.Y - _selectionArea.Location.Y);
					_selectionArea.Invalidate();
					Invalidate(_selectionArea, true);
				}
				
				if (_isAddLink)
				{
					_selectedElement = _document.FindElement(dragPoint);
					if ((_selectedElement is ConnectorElement) 
						&& (_document.CanAddLink(_connStart, (ConnectorElement) _selectedElement)))
						_linkLine.Connector2 = (ConnectorElement) _selectedElement;
					else
						_linkLine.Connector2 = _connEnd;

					var ctrl = (IMoveController) ((IControllable) _connEnd).GetController();
					ctrl.Move(dragPoint);
					
					//this.Invalidate(linkLine, true); //TODO
					base.Invalidate();
				}
			}

			base.OnMouseMove (e);
		}

		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
            if (_selectedElement == null)
            {
                return;                
            }
            var eventClickArg = new ElementEventArgs(_selectedElement);
            OnElementDoubleClick(eventClickArg);

            _moveAction.End();
            _moveAction = null;

            var eventMouseUpArg = new ElementMouseEventArgs(_selectedElement, e.X, e.Y);
            OnElementMouseUp(eventMouseUpArg);

            if (_changed)
                AddUndo();

            RestartInitValues();
            base.Invalidate();
            base.OnMouseUp(e);
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			var selectionRectangle = _selectionArea.GetUnsignedRectangle();
			
			if ((_moveAction != null) && (_moveAction.IsMoving))
			{
				var eventClickArg = new ElementEventArgs(_selectedElement);
				OnElementClick(eventClickArg);

				_moveAction.End();
				_moveAction = null;

				var eventMouseUpArg = new ElementMouseEventArgs(_selectedElement, e.X, e.Y);
				OnElementMouseUp(eventMouseUpArg);
				
				if (_changed)
					AddUndo();
			}

			// Select
			if (_isMultiSelection)
			{
				EndSelectElements(selectionRectangle);
			}
			// Add element
			else if (_isAddSelection)
			{
				EndAddElement(selectionRectangle);
			}
			
			// Add link
			else if (_isAddLink)
			{
				EndAddLink();
				AddUndo();
			}
			
			// Resize
			if (_resizeAction != null)
			{
				if (_resizeAction.IsResizing)
				{
					var mousePoint = Gsc2Goc(new Point(e.X, e.Y));
					_resizeAction.End(mousePoint);
				
					AddUndo();
				}
				_resizeAction.UpdateResizeCorner();
			}

			RestartInitValues();

			base.Invalidate();

			base.OnMouseUp (e);
		}
		#endregion

		#endregion
		
		#region Events Raising
		
		// element handler
		public delegate void ElementEventHandler(object sender, ElementEventArgs e);

		#region Element Mouse Events
		
		// CLICK
		[Category("Element")]
		public event ElementEventHandler ElementClick;
		
		protected virtual void OnElementClick(ElementEventArgs e)
		{
			if (ElementClick != null)
			{
				ElementClick(this, e);
			}
		}

        [Category("Element")]
        public event ElementEventHandler ElementDoubleClick;

		protected virtual void OnElementDoubleClick(ElementEventArgs e)
		{
            if (ElementDoubleClick != null)
			{
				ElementDoubleClick(this, e);
			}
		}

		// mouse handler
		public delegate void ElementMouseEventHandler(object sender, ElementMouseEventArgs e);

		// MOUSE DOWN
		[Category("Element")]
		public event ElementMouseEventHandler ElementMouseDown;
		
		protected virtual void OnElementMouseDown(ElementMouseEventArgs e)
		{
			if (ElementMouseDown != null)
			{
				ElementMouseDown(this, e);
			}
		}

		// MOUSE UP
		[Category("Element")]
		public event ElementMouseEventHandler ElementMouseUp;
		
		protected virtual void OnElementMouseUp(ElementMouseEventArgs e)
		{
			if (ElementMouseUp != null)
			{
				ElementMouseUp(this, e);
			}
		}

		#endregion
		 
		#region Element Move Events
		// Before Move
		[Category("Element")]
		public event ElementEventHandler ElementMoving;
		
		protected virtual void OnElementMoving(ElementEventArgs e)
		{
			if (ElementMoving != null)
			{
				ElementMoving(this, e);
			}
		}

		// After Move
		[Category("Element")]
		public event ElementEventHandler ElementMoved;
		
		protected virtual void OnElementMoved(ElementEventArgs e)
		{
			if (ElementMoved != null)
			{
				ElementMoved(this, e);
			}
		}
		#endregion

		#region Element Resize Events
		// Before Resize
		[Category("Element")]
		public event ElementEventHandler ElementResizing;
		
		protected virtual void OnElementResizing(ElementEventArgs e)
		{
			if (ElementResizing != null)
			{
				ElementResizing(this, e);
			}
		}

		// After Resize
		[Category("Element")]
		public event ElementEventHandler ElementResized;
		
		protected virtual void OnElementResized(ElementEventArgs e)
		{
			if (ElementResized != null)
			{
				ElementResized(this, e);
			}
		}
		#endregion

		#region Element Connect Events
		// connect handler
		public delegate void ElementConnectEventHandler(object sender, ElementConnectEventArgs e);

		// Before Connect
		[Category("Element")]
		public event ElementConnectEventHandler ElementConnecting;
		
		protected virtual void OnElementConnecting(ElementConnectEventArgs e)
		{
			if (ElementConnecting != null)
			{
				ElementConnecting(this, e);
			}
		}

		// After Connect
		[Category("Element")]
		public event ElementConnectEventHandler ElementConnected;
		
		protected virtual void OnElementConnected(ElementConnectEventArgs e)
		{
			if (ElementConnected != null)
			{
				ElementConnected(this, e);
			}
		}
		#endregion

		#region Element Selection Events
		// connect handler
		public delegate void ElementSelectionEventHandler(object sender, ElementSelectionEventArgs e);

		// Selection
		[Category("Element")]
		public event ElementSelectionEventHandler ElementSelection;
		
		protected virtual void OnElementSelection(ElementSelectionEventArgs e)
		{
			if (ElementSelection != null)
			{
				ElementSelection(this, e);
			}
		}

		#endregion

		#endregion

		#region Events Handling
		private void DocumentPropertyChanged(object sender, EventArgs e)
		{
			if (!IsChanging())
			{
				base.Invalidate();
			}
		}

		private void DocumentAppearancePropertyChanged(object sender, EventArgs e)
		{
		    if (IsChanging()) return;
		    AddUndo();
		    base.Invalidate();
		}

		private void DocumentElementPropertyChanged(object sender, EventArgs e)
		{
			_changed = true;

		    if (IsChanging()) return;
		    AddUndo();
		    base.Invalidate();
		}

		private void DocumentElementSelection(object sender, ElementSelectionEventArgs e)
		{
			OnElementSelection(e);
		}
		#endregion

		#region Properties

		public Document Document
		{
			get
			{
				return _document;
			}
		}

		public bool CanUndo
		{
			get
			{
				return _undo.CanUndo;
			}
		}

		public bool CanRedo
		{
			get
			{
				return _undo.CanRedo;
			}
		}


		private bool IsChanging()
		{
			return (
					((_moveAction != null) && (_moveAction.IsMoving)) //isDragging
					|| _isAddLink || _isMultiSelection || 
					((_resizeAction != null) && (_resizeAction.IsResizing)) //isResizing
					);
		}
		#endregion
		
		#region Draw Methods

		/// <summary>
		/// Graphic surface coordinates to graphic object coordinates.
		/// </summary>
        /// <param name="gsp">Graphic surface point.</param>
		/// <returns></returns>
		public Point Gsc2Goc(Point gsp)
		{
			var zoom = _document.Zoom;
			gsp.X = (int) ((gsp.X - AutoScrollPosition.X) / zoom);
			gsp.Y = (int) ((gsp.Y - AutoScrollPosition.Y) / zoom);
			return gsp;
		}

		public Rectangle Gsc2Goc(Rectangle gsr)
		{
			var zoom = _document.Zoom;
			gsr.X = (int) ((gsr.X - AutoScrollPosition.X) / zoom);
			gsr.Y = (int) ((gsr.Y - AutoScrollPosition.Y) / zoom);
			gsr.Width = (int) (gsr.Width / zoom);
			gsr.Height = (int) (gsr.Height / zoom);
			return gsr;
		}

		public Rectangle Goc2Gsc(Rectangle gsr)
		{
			var zoom = _document.Zoom;
			gsr.X = (int) ((gsr.X + AutoScrollPosition.X) * zoom);
			gsr.Y = (int) ((gsr.Y + AutoScrollPosition.Y) * zoom);
			gsr.Width = (int) (gsr.Width * zoom);
			gsr.Height = (int) (gsr.Height * zoom);
			return gsr;
		}

		internal void DrawSelectionRectangle(Graphics g)
		{
			_selectionArea.Draw(g);
		}
		#endregion

		#region Open/Save File
		public void Save(string fileName)
		{
			IFormatter formatter = new BinaryFormatter();
			Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
			formatter.Serialize(stream, _document);
			stream.Close();
		}

		public void Open(string fileName)
		{
			IFormatter formatter = new BinaryFormatter();
			Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
			_document = (Document) formatter.Deserialize(stream);
			stream.Close();
			RecreateEventsHandlers();
		}
		#endregion

		#region Copy/Paste
		public void Copy()
		{
			if (_document.SelectedElements.Count == 0) return;

			IFormatter formatter = new BinaryFormatter();
			Stream stream = new MemoryStream();
			formatter.Serialize(stream, _document.SelectedElements.GetArray());
			var data = new DataObject(DataFormats.GetFormat("Diagram.NET Element Collection").Name,
				stream);
			Clipboard.SetDataObject(data);
		}

		public void Paste()
		{
			const int pasteStep = 20;

			_undo.Enabled = false;
			var iData = Clipboard.GetDataObject();
			var format = DataFormats.GetFormat("Diagram.NET Element Collection");
			if (iData != null && iData.GetDataPresent(format.Name))
			{
				IFormatter formatter = new BinaryFormatter();
				Stream stream = (MemoryStream) iData.GetData(format.Name);
				var elCol = (BaseElement[]) formatter.Deserialize(stream);
				stream.Close();

				foreach(var el in elCol)
				{
					el.Location = new Point(el.Location.X + pasteStep, el.Location.Y + pasteStep);
				}

				_document.AddElements(elCol);
				_document.ClearSelection();
				_document.SelectElements(elCol);
			}
			_undo.Enabled = true;
				
			AddUndo();
			EndGeneralAction();
		}

		public void Cut()
		{
			Copy();
			DeleteSelectedElements();
			EndGeneralAction();
		}
		#endregion

		#region Start/End Actions and General Functions
		
		#region General
		private void EndGeneralAction()
		{
			RestartInitValues();
			
			if (_resizeAction != null) _resizeAction.ShowResizeCorner(false);
		}
		
		private void RestartInitValues()
		{
			
			// Reinitialize status
			_moveAction = null;

			_isMultiSelection = false;
			_isAddSelection = false;
			_isAddLink = false;

			_changed = false;

			_connStart = null;
			
			_selectionArea.FillColor1 = SystemColors.Control;
			_selectionArea.BorderColor = SystemColors.Control;
			_selectionArea.Visible = false;

			_document.CalcWindow(true);
		}

		#endregion

		#region Selection
		private void StartSelectElements(BaseElement selectedElem, Point mousePoint)
		{
			// Vefiry if element is in selection
			if (!_document.SelectedElements.Contains(selectedElem))
			{
				//Clear selection and add new element to selection
				_document.ClearSelection();
				_document.SelectElement(selectedElem);
			}

			_changed = false;
			

			_moveAction = new MoveAction();
			MoveAction.OnElementMovingDelegate onElementMovingDelegate = OnElementMoving;
			_moveAction.Start(mousePoint, _document, onElementMovingDelegate);


			// Get Controllers
			_controllers = new IController[_document.SelectedElements.Count];
			for(var i = _document.SelectedElements.Count - 1; i >= 0; i--)
			{
				if (_document.SelectedElements[i] is IControllable)
				{
					// Get General Controller
					_controllers[i] = ((IControllable) _document.SelectedElements[i]).GetController();
				}
				else
				{
					_controllers[i] = null;
				}
			}

			_resizeAction = new ResizeAction();
			_resizeAction.Select(_document);
		}

		private void EndSelectElements(Rectangle selectionRectangle)
		{
			_document.SelectElements(selectionRectangle);
		}
		#endregion		

		#region Resize
		private void StartResizeElement(Point mousePoint)
		{
		    if ((_resizeAction == null) ||
		        ((_document.Action != DesignerAction.Select) &&
		         ((_document.Action != DesignerAction.Connect) || (!_resizeAction.IsResizingLink)))) return;
		    var onElementResizingDelegate = new ResizeAction.OnElementResizingDelegate(OnElementResizing);
		    _resizeAction.Start(mousePoint, onElementResizingDelegate);
		    if (!_resizeAction.IsResizing)
		        _resizeAction = null;
		}
		#endregion

		#region Link
		private void StartAddLink(ConnectorElement connectorStart, Point mousePoint)
		{
		    if (_document.Action != DesignerAction.Connect) return;
		    _connStart = connectorStart;
		    _connEnd = new ConnectorElement(connectorStart.ParentElement) {Location = connectorStart.Location};

		    var ctrl = (IMoveController) ((IControllable) _connEnd).GetController();
		    ctrl.Start(mousePoint);

		    _isAddLink = true;
				
		    switch(_document.LinkType)
		    {
		        case (LinkType.Straight):
		            _linkLine = new StraightLinkElement(connectorStart, _connEnd);
		            break;
		        case (LinkType.RightAngle):
		            _linkLine = new RightAngleLinkElement(connectorStart, _connEnd);
		            break;
		    }
		    _linkLine.Visible = true;
		    _linkLine.BorderColor = Color.FromArgb(150, Color.Black);
		    _linkLine.BorderWidth = 1;
				
		    Invalidate(_linkLine, true);
				
		    OnElementConnecting(new ElementConnectEventArgs(connectorStart.ParentElement, null, _linkLine));
		}

		private void EndAddLink()
		{
			if (_connEnd != _linkLine.Connector2)
			{
				_linkLine.Connector1.RemoveLink(_linkLine);
				_linkLine = _document.AddLink(_linkLine.Connector1, _linkLine.Connector2);
				OnElementConnected(new ElementConnectEventArgs(_linkLine.Connector1.ParentElement, _linkLine.Connector2.ParentElement, _linkLine));
			}

			_connStart = null;
			_connEnd = null;
			_linkLine = null;
		}
		#endregion

		#region Add Element
		private void StartAddElement(Point mousePoint)
		{
			_document.ClearSelection();

			//Change Selection Area Color
			_selectionArea.FillColor1 = Color.LightSteelBlue;
			_selectionArea.BorderColor = Color.WhiteSmoke;

			_isAddSelection = true;
			_selectionArea.Visible = true;
			_selectionArea.Location = mousePoint;
			_selectionArea.Size = new Size(0, 0);		
		}

		private void EndAddElement(Rectangle selectionRectangle)
		{
			BaseElement el;
			switch (_document.ElementType)
			{
				case ElementType.Rectangle:
					el = new RectangleElement(selectionRectangle);
					break;
				case ElementType.RectangleNode:
					el = new RectangleNode(selectionRectangle);
					break;
				case ElementType.Elipse:
					el = new ElipseElement(selectionRectangle);
					break;
				case ElementType.ElipseNode:
					el = new ElipseNode(selectionRectangle);
					break;
				case ElementType.CommentBox:
					el = new CommentBoxElement(selectionRectangle);
					break;
				default:
					el = new RectangleNode(selectionRectangle);
					break;
			}
			
			_document.AddElement(el);
			
			_document.Action = DesignerAction.Select;	
		}
		#endregion

		#region Edit Label
		private void StartEditLabel()
		{
			_isEditLabel = true;

			// Disable resize
			if (_resizeAction != null)
			{	
				_resizeAction.ShowResizeCorner(false);
				_resizeAction = null;
			}
			
			_editLabelAction = new EditLabelAction();
			_editLabelAction.StartEdit(_selectedElement, _labelTextBox);
		}

		private void EndEditLabel()
		{
			if (_editLabelAction != null)
			{
				_editLabelAction.EndEdit();
				_editLabelAction = null;
			}
			_isEditLabel = false;
		}
		#endregion

		#region Delete
		private void DeleteElement(Point mousePoint)
		{
			_document.DeleteElement(mousePoint);
			_selectedElement = null;
			_document.Action = DesignerAction.Select;		
		}

		private void DeleteSelectedElements()
		{
			_document.DeleteSelectedElements();
		}
		#endregion

		#endregion

		#region Undo/Redo
		public void Undo()
		{
			_document = (Document) _undo.Undo();
			RecreateEventsHandlers();
			if (_resizeAction != null) _resizeAction.UpdateResizeCorner();
			base.Invalidate();
		}

		public void Redo()
		{
			_document = (Document) _undo.Redo();
			RecreateEventsHandlers();
			if (_resizeAction != null) _resizeAction.UpdateResizeCorner();
			base.Invalidate();
		}

		private void AddUndo()
		{
			_undo.AddUndo(_document);
		}
		#endregion

		private void RecreateEventsHandlers()
		{
			_document.PropertyChanged += DocumentPropertyChanged;
			_document.AppearancePropertyChanged+=DocumentAppearancePropertyChanged;
			_document.ElementPropertyChanged += DocumentElementPropertyChanged;
			_document.ElementSelection += DocumentElementSelection;
		}
	}
}
