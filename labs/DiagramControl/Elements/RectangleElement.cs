using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using DiagramNet.Elements.Controllers;

namespace DiagramNet.Elements
{
    [Serializable]
    public class RectangleElement : BaseElement, IControllable, ILabelElement
    {
        protected Color FillColor1Value = Color.White;
        protected Color FillColor2Value = Color.DodgerBlue;
        protected LabelElement LabelValue = new LabelElement();
        
        [NonSerialized]
        private RectangleController _controller;

        public RectangleElement(): this(0, 0, 100, 100)
        {}

        public RectangleElement(Rectangle rec): this(rec.Location, rec.Size)
        {}
        
        public RectangleElement(Point l, Size s): this(l.X, l.Y, s.Width, s.Height) 
        {}

        public RectangleElement(int top, int left, int width, int height)
        {
            LocationValue  = new Point(top, left);
            SizeValue = new Size(width, height);
        }

// ReSharper disable RedundantOverridenMember
        public override Point Location
        {
            get
            {
                return base.Location;
            }
            set
            {
                base.Location = value;
            }
        }
// ReSharper restore RedundantOverridenMember
        
// ReSharper disable RedundantOverridenMember
        public override Size Size
        {
            get
            {
                return base.Size;
            }
            set
            {
                base.Size = value;
            }
        }
// ReSharper restore RedundantOverridenMember

        public virtual Color FillColor1
        {
            get
            {
                return FillColor1Value;
            }
            set
            {
                FillColor1Value = value;
                OnAppearanceChanged(new EventArgs());
            }
        }

        public virtual Color FillColor2 
        {
            get
            {
                return FillColor2Value;
            }
            set
            {
                FillColor2Value = value;
                OnAppearanceChanged(new EventArgs());
            }
        }

        public virtual LabelElement Label 
        {
            get
            {
                return LabelValue;
            }
            set
            {
                LabelValue = value;
                OnAppearanceChanged(new EventArgs());
            }
        }

        protected virtual Brush GetBrush(Rectangle r)
        {
            //Fill rectangle
            Color fill1;
            Color fill2;
            Brush b;
            if (OpacityValue == 100)
            {
                fill1 = FillColor1Value;
                fill2 = FillColor2Value;
            }
            else
            {
                fill1 = Color.FromArgb((int) (255.0f * (OpacityValue / 100.0f)), FillColor1Value);
                fill2 = Color.FromArgb((int) (255.0f * (OpacityValue / 100.0f)), FillColor2Value);
            }
            
            if (FillColor2Value == Color.Empty)
                b = new SolidBrush(fill1);
            else
            {
                var rb = new Rectangle(r.X, r.Y, r.Width + 1, r.Height + 1);
                b = new LinearGradientBrush(
                    rb,
                    fill1, 
                    fill2, 
                    LinearGradientMode.Horizontal);
            }

            return b;
        }

        protected virtual void DrawBorder(Graphics g, Rectangle r)
        {
            //Border
            var p = new Pen(BorderColorValue, BorderWidthValue);
            g.DrawRectangle(p, r);
            p.Dispose();
        }

        internal override void Draw(Graphics g)
        {
            IsInvalidated = false;

            var r = GetUnsignedRectangle();
            var b = GetBrush(r);			
            g.FillRectangle(b, r);

            DrawBorder(g, r);
            b.Dispose();
        }
        
        IController IControllable.GetController()
        {
            return _controller ?? (_controller = new RectangleController(this));
        }
    }
}
