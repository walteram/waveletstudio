using System;
using System.ComponentModel;
using System.Drawing;

namespace DiagramNet.Elements
{
    /// <summary>
    /// This is the base for all node element.
    /// </summary>
    [Serializable]
    public abstract class NodeElement: BaseElement
    {
        protected ConnectorElement[] Connects = new ConnectorElement[4];
        protected const int ConnectSize = 3;
        protected bool Overrided;
        
        protected NodeElement(int top, int left, int width, int height): base(top, left, width, height)
        {
            InitConnectors();
        }

        protected NodeElement()
        {
            
        }

        [Browsable(false)]
        public virtual ConnectorElement[] Connectors
        {
            get
            {
                return Connects;
            }
        }

        public override Point Location
        {
            get
            {
                return LocationValue;
            }
            set
            {
                LocationValue = value;
                if (Overrided) return;
                UpdateConnectorsPosition();
                OnAppearanceChanged(new EventArgs());
            }
        }

        public override Size Size
        {
            get
            {
                return SizeValue;
            }
            set
            {
                SizeValue = value;
                UpdateConnectorsPosition();
                OnAppearanceChanged(new EventArgs());
            }
        }

        public override bool Visible
        {
            get
            {
                return VisibleValue;
            }
            set
            {
                VisibleValue = value;
                foreach (var c in Connects)
                {
                    c.Visible = value;
                }
                OnAppearanceChanged(new EventArgs());
            }
        }

        public virtual bool IsConnected
        {
            get
            {
                foreach (var c in Connects)
                {
                    if (c.Links.Count > 0)
                        return true;
                }
                return false;
            }
        }

        protected void InitConnectors()
        {
            Connects[0] = new ConnectorElement(this);
            Connects[1] = new ConnectorElement(this);
            Connects[2] = new ConnectorElement(this);
            Connects[3] = new ConnectorElement(this);
            UpdateConnectorsPosition();
        }

        protected void UpdateConnectorsPosition()
        {
            //Top
            var loc = new Point(LocationValue.X + SizeValue.Width / 2,
                                  LocationValue.Y);
            var connect = Connects[0];
            connect.Location = new Point(loc.X - ConnectSize, loc.Y - ConnectSize);
            connect.Size = new Size(ConnectSize * 2, ConnectSize * 2);

            //Botton
            loc = new Point(LocationValue.X + SizeValue.Width / 2,
                LocationValue.Y + SizeValue.Height);
            connect = Connects[1];
            connect.Location = new Point(loc.X - ConnectSize, loc.Y - ConnectSize);
            connect.Size = new Size(ConnectSize * 2, ConnectSize * 2);

            //Left
            loc = new Point(LocationValue.X,
                LocationValue.Y + SizeValue.Height / 2);
            connect = Connects[2];
            connect.Location = new Point(loc.X - ConnectSize, loc.Y - ConnectSize);
            connect.Size = new Size(ConnectSize * 2, ConnectSize * 2);

            //Right
            loc = new Point(LocationValue.X + SizeValue.Width,
                LocationValue.Y + SizeValue.Height / 2);
            connect = Connects[3];
            connect.Location = new Point(loc.X - ConnectSize, loc.Y - ConnectSize);
            connect.Size = new Size(ConnectSize * 2, ConnectSize * 2);
        }

        public override void Invalidate()
        {
            base.Invalidate();
            for(var i = Connects.Length - 1; i >= 0; i--)
            {
                for(var ii = Connects[i].Links.Count - 1; ii >= 0; ii--)
                {
                    Connects[i].Links[ii].Invalidate();
                }				
            }
        }


        internal virtual void Draw(Graphics g, bool drawConnector)
        {
            Draw(g);
            if (drawConnector)
                DrawConnectors(g);
        }

        protected void DrawConnectors(Graphics g)
        {
            foreach (var ce in Connects)
            {
                ce.Draw(g);
            }
        }

        public virtual ElementCollection GetLinkedNodes()
        {
            var ec = new ElementCollection();
            foreach(var ce in Connects)
            {
                foreach(BaseLinkElement le in ce.Links)
                {
                    ec.Add(le.Connector1 == ce ? le.Connector2.ParentElement : le.Connector1.ParentElement);
                }
            }
            return ec;
        }
    }
}
