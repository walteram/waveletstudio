using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using DiagramNet.Elements.Controllers;

namespace DiagramNet.Elements
{
    [Serializable]
    public class RightAngleLinkElement: BaseLinkElement, IControllable, ILabelElement
    {
        public LineElement[] LineElements = {new LineElement(0,0,0,0)};
        public Orientation Orientation;
        private CardinalDirection _conn1Dir;
        private CardinalDirection _conn2Dir;
        private bool _needCalcLinkLocation = true;
        private bool _needCalcLinkSize = true;

        private LabelElement _label = new LabelElement();

        [NonSerialized]
        private RightAngleLinkController _controller;

        public RightAngleLinkElement(ConnectorElement conn1, ConnectorElement conn2): base(conn1, conn2)
        {
            NeedCalcLinkValue = true;
            InitConnectors(conn1, conn2);
            foreach(var l in LineElements)
            {
                l.StartCap = LineCap.Round;
                l.EndCap = LineCap.Round;
            }
            StartCapValue = LineCap.Round;
            EndCapValue = LineCap.Round;

            _label.PositionBySite(LineElements[1]);
        }

        #region Properties
        [Browsable(false)]
        public override Point Point1
        {
            get
            {
                return LineElements[0].Point1;
            }
        }

        [Browsable(false)]
        public override Point Point2
        {
            get
            {
                return LineElements[LineElements.Length - 1].Point2;
            }
        }

        public override Color BorderColor
        {
            get
            {
                return BorderColorValue;
            }
            set
            {
                BorderColorValue = value;
                foreach (var l in LineElements)
                    l.BorderColor = value;
                OnAppearanceChanged(new EventArgs());
            }
        }

        public override int BorderWidth
        {
            get
            {
                return BorderWidthValue;
            }
            set
            {
                BorderWidthValue = value;
                foreach (var l in LineElements)
                    l.BorderWidth = value;
                OnAppearanceChanged(new EventArgs());
            }
        }

        public override Point Location
        {
            get
            {
                CalcLinkLocation();
                return LocationValue;
            }
            set
            {
                var ctrl = (IMoveController) ((IControllable) this).GetController();
                if (!ctrl.IsMoving)
                    return;

                var locBefore = Location;
                var locAfter = value;

                var locDiff = new Point(locAfter.X - locBefore.X, locAfter.Y - locBefore.Y);

                foreach(var l in LineElements)
                {
                    var lPoint1 = l.Point1;
                    var lPoint2 = l.Point2;
                    l.Point1 = new Point(lPoint1.X + locDiff.X, lPoint1.Y + locDiff.Y);
                    l.Point2 = new Point(lPoint2.X + locDiff.X, lPoint2.Y + locDiff.Y);
                }
                NeedCalcLinkValue = true;
                OnAppearanceChanged(new EventArgs());
            }
        }

        public override Size Size
        {
            get
            {
                CalcLinkSize();
                return SizeValue;
            }
        }

        public override int Opacity
        {
            get
            {
                return OpacityValue;
            }
            set
            {
                OpacityValue = value;
                foreach (var l in LineElements)
                    l.Opacity = value;
                OnAppearanceChanged(new EventArgs());
            }
        }

        public override LineCap StartCap
        {
            get
            {
                return StartCapValue;
            }
            set
            {
                StartCapValue = value;
                LineElements[0].StartCap = value;
                OnAppearanceChanged(new EventArgs());
            }
        }

        public override LineCap EndCap
        {
            get
            {
                return EndCapValue;
            }
            set
            {
                EndCapValue = value;
                LineElements[LineElements.Length - 1].EndCap = value;
                OnAppearanceChanged(new EventArgs());
            }
        }

        public override LineElement[] Lines
        {
            get
            {
                return (LineElement[]) LineElements.Clone();
            }
        }
        #endregion

        internal override void Draw(Graphics g)
        {
            IsInvalidated = false;

            CalcLink();

            for (var i = 0; i < LineElements.Length; i++)
            {
                if (i == LineElements.Length-1)
                {
                    LineElements[i].EndCap = LineCap.ArrowAnchor;                    
                }
                LineElements[i].Draw(g);
            }
                
        }

        private void InitConnectors(ConnectorElement conn1, ConnectorElement conn2)
        {
            _conn1Dir = conn1.GetDirection();
            _conn2Dir = conn2.GetDirection();

            if ((_conn1Dir == CardinalDirection.North) || (_conn1Dir == CardinalDirection.South))
                Orientation = Orientation.Vertical;
            else
                Orientation = Orientation.Horizontal;

            if (
                (
                ((_conn1Dir == CardinalDirection.North) || (_conn1Dir == CardinalDirection.South))
                && ((_conn2Dir == CardinalDirection.East) || (_conn2Dir == CardinalDirection.West)))
                ||
                (
                ((_conn1Dir == CardinalDirection.East) || (_conn1Dir == CardinalDirection.West))
                && ((_conn2Dir == CardinalDirection.North) || (_conn2Dir == CardinalDirection.South)))
                )
            {
                LineElements = new LineElement[2];
                LineElements[0] = new LineElement(0, 0, 0, 0);
                LineElements[1] = new LineElement(0, 0, 0, 0);
            }
            else	
            {
                LineElements = new LineElement[3];
                LineElements[0] = new LineElement(0, 0, 0, 0);
                LineElements[1] = new LineElement(0, 0, 0, 0);
                LineElements[2] = new LineElement(0, 0, 0, 0);
            }
            
            CalcLinkFirtTime();
            CalcLink();
            RestartProps();
        }

        private void RestartProps()
        {
            foreach(var line in LineElements)
            {
                line.BorderColor = BorderColorValue;
                line.BorderWidth = BorderWidthValue;
                line.Opacity = OpacityValue;
                line.StartCap = StartCapValue;
                line.EndCap = EndCapValue;
            }
        }
        protected override void OnConnectorChanged(EventArgs e)
        {
            InitConnectors(Connector1Value, Connector2Value);
            base.OnConnectorChanged (e);
        }


        internal void CalcLinkFirtTime()
        {
            if (LineElements == null)
                return;

            var lastLine = LineElements[LineElements.Length - 1];
            var connector1Location = Connector1Value.Location;
            var connector2Location = Connector2Value.Location;
            var connector1Size = Connector1Value.Size;
            var connector2Size = Connector2Value.Size;
            LineElements[0].Point1 = new Point(connector1Location.X + connector1Size.Width / 2, connector1Location.Y + connector1Size.Height / 2);            
            lastLine.Point2 = Orientation == Orientation.Horizontal ? new Point(connector2Location.X, connector2Location.Y + connector1Size.Height / 2) : new Point(connector2Location.X + connector2Size.Width / 2, connector2Location.Y);

            if (LineElements.Length != 3) return;
            var lines0Point1 = LineElements[0].Point1;
            var lastLinePoint2 = lastLine.Point2;

            if (Orientation == Orientation.Horizontal)
            {
                LineElements[0].Point2 = new Point(lines0Point1.X + ((lastLinePoint2.X - lines0Point1.X) / 2), lines0Point1.Y);
                lastLine.Point1 = new Point(lines0Point1.X + ((lastLinePoint2.X - lines0Point1.X) / 2), lastLinePoint2.Y);
            }
            else if (Orientation == Orientation.Vertical)
            {
                LineElements[0].Point2 = new Point(lines0Point1.X, lines0Point1.Y + ((lastLinePoint2.Y - lines0Point1.Y) / 2));
                lastLine.Point1 = new Point(lastLinePoint2.X, lines0Point1.Y + ((lastLinePoint2.Y - lines0Point1.Y) / 2));
            }
        }

        internal override void CalcLink()
        {
            if (NeedCalcLinkValue == false) return;

            if (LineElements == null)
                return;

            var lastLine = LineElements[LineElements.Length - 1];

            //Otimization - Get prop. value only one time
            var connector1Location = Connector1Value.Location;
            var connector2Location = Connector2Value.Location;
            var connector1Size = Connector1Value.Size;
            var connector2Size = Connector2Value.Size;

            LineElements[0].Point1 = new Point(connector1Location.X + connector1Size.Width / 2, connector1Location.Y + connector1Size.Height / 2);
            lastLine.Point2 = Orientation == Orientation.Horizontal ? new Point(connector2Location.X, connector2Location.Y + connector2Size.Height/2) : new Point(connector2Location.X + connector2Size.Width / 2, connector2Location.Y);
            
            if (LineElements.Length == 3)
            {

                if (Orientation == Orientation.Horizontal)
                {
                    LineElements[0].Point2 = new Point(LineElements[0].Point2.X, LineElements[0].Point1.Y);
                    lastLine.Point1 = new Point(lastLine.Point1.X, lastLine.Point2.Y);
                    LineElements[1].Point1 = LineElements[0].Point2;
                    LineElements[1].Point2 = LineElements[2].Point1;
                }
                else if (Orientation == Orientation.Vertical)
                {
                    LineElements[0].Point2 = new Point(LineElements[0].Point1.X, LineElements[0].Point2.Y);
                    lastLine.Point1 = new Point(lastLine.Point2.X, lastLine.Point1.Y);
                    LineElements[1].Point1 = LineElements[0].Point2;
                    LineElements[1].Point2 = LineElements[2].Point1;
                }
            }
            else if (LineElements.Length == 2)
            {
                if ((_conn1Dir == CardinalDirection.North) || (_conn1Dir == CardinalDirection.South))
                    LineElements[0].Point2 = new Point(LineElements[0].Point1.X, lastLine.Point2.Y);
                else
                    LineElements[0].Point2 = new Point(lastLine.Point2.X, LineElements[0].Point1.Y);

                lastLine.Point1 = LineElements[0].Point2;
            }

            _needCalcLinkLocation = true;
            _needCalcLinkSize = true;

            NeedCalcLinkValue = false;
        }

        private void CalcLinkLocation()
        {
            //CalcLink();

            if (!_needCalcLinkLocation)
                return;

            var points = new Point[LineElements.Length * 2];
            var i = 0;
            foreach(var ln in LineElements)
            {
                points[i] = ln.Point1;
                points[i + 1] = ln.Point2;
                i+=2;
            }
            LocationValue = DiagramUtil.GetUpperPoint(points);
            _needCalcLinkLocation = false;
        }

        private void CalcLinkSize()
        {
            if (!_needCalcLinkSize)
                return;
            var sizeTmp = Size.Empty;
            if (LineElements.Length > 1)
            {
                var points = new Point[LineElements.Length * 2];
                var i = 0;
                foreach(var ln in LineElements)
                {
                    points[i] = ln.Point1;
                    points[i + 1] = ln.Point2;
                    i+=2;
                }
                var upper = DiagramUtil.GetUpperPoint(points);
                var lower = DiagramUtil.GetLowerPoint(points);
                sizeTmp = new Size(lower.X - upper.X, lower.Y - upper.Y);
            }
            SizeValue = sizeTmp;
            _needCalcLinkSize = false;
        }

    
        #region IControllable Members

        IController IControllable.GetController()
        {
            return _controller ?? (_controller = new RightAngleLinkController(this));
        }

        #endregion

        #region ILabelElement Members

        public virtual LabelElement Label 
        {
            get
            {
                return _label;
            }
            set
            {
                _label = value;
                OnAppearanceChanged(new EventArgs());
            }
        }

        #endregion
    }
}
