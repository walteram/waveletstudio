using System;
using System.Drawing;
using System.Windows.Forms;

namespace WaveletStudio.Designer.Utils
{
    public sealed class FlatButton : Button
    {
        private bool _pressed;

        private bool _hasFocus;

        public Color FocusBackColor { get; set; }

        public Color PressedBackColor { get; set; }

        public Color NormalBackColor { get; set; }

        public Color NormalBorderColor { get; set; }

        public Color FocusBorderColor { get; set; }

        public Color PressedBorderColor { get; set; }

        public Color PressedForeColor { get; set; }

        public Color FocusForeColor { get; set; }        

        public Color NormalForeColor { get; set; }

        public int NormalBorderSize { get; set; }

        public int FocusBorderSize { get; set; }

        public int PressedBorderSize { get; set; }
        
        public bool HasPressedState { get; set; }

        protected override bool ShowFocusCues
        {
            get
            {
                return false;
            }
        }

        public bool Pressed
        {
            get { return _pressed; }
            set
            {
                if (!HasPressedState)
                {
                    return;
                }
                _pressed = value;
                if (_pressed)
                {
                    BackColor = PressedBackColor;
                }
                else
                {
                    if (_hasFocus)
                    {
                        BackColor = FocusBackColor;
                    }
                    else
                    {
                        BackColor = NormalBackColor;
                    }
                }
                Refresh();
            }
        }

        public FlatButton()
        {
            UseVisualStyleBackColor = false;
            PressedBackColor = Color.Thistle;
            FocusBackColor = Color.Thistle;
            NormalBackColor = Color.White;
            PressedBorderColor = FlatAppearance.BorderColor;
            FocusBorderColor = FlatAppearance.BorderColor;
            NormalBorderColor = FlatAppearance.BorderColor;
            NormalBorderSize = FlatAppearance.BorderSize;
            PressedBorderSize = FlatAppearance.BorderSize;
            FocusBorderSize = FlatAppearance.BorderSize;
            PressedForeColor = ForeColor;
            FocusForeColor = ForeColor;
            NormalForeColor = ForeColor;
            HasPressedState = true;            
            SetStyle(ControlStyles.Selectable, false);            
        }        

        protected override void OnMouseEnter(EventArgs e)
        {
            _hasFocus = true;
            BackColor = FocusBackColor;
            FlatAppearance.BorderColor = FocusBorderColor;
            FlatAppearance.BorderSize = FocusBorderSize;
            ForeColor = FocusForeColor;            
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _hasFocus = false;
            if (HasPressedState && Pressed)
            {
                BackColor = PressedBackColor;
                FlatAppearance.BorderColor = PressedBorderColor;
                FlatAppearance.BorderSize = PressedBorderSize;
                ForeColor = PressedForeColor;
            }
            else
            {
                BackColor = NormalBackColor;
                FlatAppearance.BorderColor = NormalBorderColor;
                FlatAppearance.BorderSize = NormalBorderSize;
                ForeColor = NormalForeColor;
            }
        }        
    }
}
