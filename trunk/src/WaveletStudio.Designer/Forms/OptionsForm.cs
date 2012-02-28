/*  Wavelet Studio Signal Processing Library - www.waveletstudio.net
    Copyright (C) 2011, 2012 Walter V. S. de Amorim - The Wavelet Studio Initiative

    Wavelet Studio is free software: you can redistribute it and/or modify
    it under the terms of the GNU Lesser General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Wavelet Studio is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>. 
*/

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Qios.DevSuite.Components.Ribbon;
using WaveletStudio.Designer.Properties;

namespace WaveletStudio.Designer.Forms
{
    public partial class OptionsForm : QRibbonForm
    {
        public OptionsForm()
        {
            InitializeComponent();

            LanguageList.Items.Clear();
            foreach(var lang in Resources.AvailableLanguages.Split(new[]{Environment.NewLine}, StringSplitOptions.None))
            {
                LanguageList.Items.Add(lang);
            }
            if (string.IsNullOrEmpty(Settings.Default.Language))
            {
                LanguageList.SelectedIndex = 0;
            }
            else if (Settings.Default.Language.StartsWith("pt-BR"))
            {
                LanguageList.SelectedIndex = 2;
            }
            else
            {
                LanguageList.SelectedIndex = 1;
            }

            ThemeList.SelectedItem = !string.IsNullOrEmpty(Settings.Default.Theme) ? Settings.Default.Theme : ThemeList.Items[0];
            if (ThemeList.SelectedIndex == -1)
            {
                ThemeList.SelectedIndex = 0;
            }
            AutoLoadLastFileField.Checked = Settings.Default.AutoLoadLastFile;
        }

        private void OptionsFormLoad(object sender, EventArgs e)
        {
            TabControl.TabStripLeft.Painter = new QTabStripPainter();
        }

        private void SaveButtonClick(object sender, EventArgs e)
        {
            string language;
            switch (LanguageList.SelectedIndex)
            {
                case 1:
                    language = "en-US";
                    break;
                case 2:
                    language = "pt-BR";
                    break;
                default:
                    language = "";
                    break;
            }

            Settings.Default.Language = language;
            Settings.Default.Theme = ThemeList.SelectedItem.ToString();
            Settings.Default.AutoLoadLastFile = AutoLoadLastFileField.Checked;
            Settings.Default.Save();

            MessageBox.Show(Resources.RestartNeeded, Resources.Attention, MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void CancelButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        public class QTabStripPainter : Qios.DevSuite.Components.QTabStripPainter
        {

            /// <summary>
            /// Overridden Draws the base background and the orange part.
            /// </summary>
            protected override void DrawTabButtonBackground(Qios.DevSuite.Components.QTabButton button, Qios.DevSuite.Components.QTabButtonConfiguration buttonConfiguration, Qios.DevSuite.Components.QTabStripConfiguration stripConfiguration, Qios.DevSuite.Components.QTabButtonAppearance buttonAppearance, Rectangle buttonBounds, Rectangle controlAndButtonBounds, DockStyle dockStyle, Color backColor1, Color backColor2, Color borderColor, Graphics graphics)
            {
                base.DrawTabButtonBackground(button, buttonConfiguration, stripConfiguration, buttonAppearance, buttonBounds, controlAndButtonBounds, dockStyle, backColor1, backColor2, borderColor, graphics);

                const int tmpIOrangePartWidth = 4;

                if (((!button.IsHot) && (!button.IsActivated)) || (button.LastDrawnGraphicsPath == null))
                {
                    return;
                }
                //Get the bounds
                var tmpOButtonBounds = button.BoundsToControl;

                //Create a gradient
                var tmpORect = new Rectangle(tmpOButtonBounds.Left, tmpOButtonBounds.Top, tmpIOrangePartWidth, tmpOButtonBounds.Height);
                var tmpOBrush = new LinearGradientBrush(tmpORect, Color.Orange, Color.White, 0, false);

                //Set the clip to the dran path.
                var tmpOCurrentClip = graphics.Clip;
                var tmpONewClip = tmpOCurrentClip.Clone();
                tmpONewClip.Intersect(new Region(button.LastDrawnGraphicsPath));
                graphics.Clip = tmpONewClip;

                //Fill the rectangle
                graphics.FillRectangle(tmpOBrush, tmpORect);

                //Reset the clip
                graphics.Clip = tmpOCurrentClip;

                //Dispose resources.
                tmpONewClip.Dispose();
                tmpOBrush.Dispose();
            }

        }
    }
}

