using System;
using System.Drawing;
using DiagramNet.Elements.Controllers;

namespace DiagramNet.Elements
{
    [Serializable]
    public class ConnectorElement: RectangleElement, IControllable 
    {
        private readonly NodeElement _parentElement;
        public bool IsStart;
        public object State;
        public string ShortName;

        [NonSerialized]
        private ConnectorController _controller;

        public ConnectorElement()
        {
            
        }

        public ConnectorElement(NodeElement parent): base(new Rectangle(0, 0, 0, 0))
        {
            _parentElement = parent;
            BorderColorValue = Color.Black;
            FillColor1Value = Color.LightGray;
            FillColor2Value = Color.Empty;
        }

        public NodeElement ParentElement
        {
            get
            {
                return _parentElement;
            }
            
        }

        internal void AddLink(BaseLinkElement lnk)
        {
            Links.Add(lnk);
        }

        public void RemoveLink(BaseLinkElement lnk)
        {
            Links.Remove(lnk);
        }

        private ElementCollection _links = new ElementCollection();
        public ElementCollection Links
        {
            get { return _links; }
            set { _links = value; }
        }

        internal CardinalDirection GetDirection()
        {
            var rec = new Rectangle(_parentElement.Location, _parentElement.Size);
            var refPoint = new Point(LocationValue.X - _parentElement.Location.X + (SizeValue.Width / 2),
                                       LocationValue.Y - _parentElement.Location.Y + (SizeValue.Height / 2));

            return DiagramUtil.GetDirection(rec, refPoint);
        }

        IController IControllable.GetController()
        {
            return _controller ?? (_controller = new ConnectorController(this));
        }


        public override Point Location
        {
            get
            {
                return base.Location;
            }
            set
            {
                if (value == base.Location) return;
                
                foreach(BaseLinkElement lnk in Links)
                {
                    lnk.NeedCalcLink = true;					
                }
                base.Location = value;
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
                if (value == base.Size) return;

                foreach(BaseLinkElement lnk in Links)
                {
                    lnk.NeedCalcLink = true;
                }
                base.Size = value;
            }
        }
    }
}
