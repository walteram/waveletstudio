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
using Qios.DevSuite.Components;
using WaveletStudio.Designer.Properties;

namespace WaveletStudio.Designer.Controls
{
    public static class QControlUtils
    {
        public static QCompositeItem CreateCompositeListItem(string itemName, string imageResourceName, string title, string text, int borderWidth, QPartDirection direction, QPartAlignment textAlignment, Color? color, int imageWidth = 64, int imageHeight = 48)
        {
            var item = new QCompositeItem();
            var image = GetImageFromResource(imageResourceName);
            var itemGroup = GetSolidColorCompositeGroup(direction, color, 1);
            var textsGroup = GetSolidColorCompositeGroup(QPartDirection.Vertical, color, 0, true, false);
            var fontDefinition = new QFontDefinition { Bold = true, Size = -1 };
            image.Configuration.MaximumSize = new Size(imageWidth, imageHeight);
            textsGroup.Items.Add(new QCompositeText { Title = title, Configuration = { AlignmentHorizontal = textAlignment, FontDefinition = fontDefinition, FontDefinitionHot = fontDefinition, FontDefinitionPressed = fontDefinition } });
            if (!string.IsNullOrEmpty(text))
            {
                textsGroup.Items.Add(new QCompositeText {  Title = text, Configuration = { AlignmentHorizontal = textAlignment } });
            }
            image.Configuration.AlignmentHorizontal = QPartAlignment.Centered;
            itemGroup.Items.Add(image);
            itemGroup.Items.Add(textsGroup);
            item.Items.Add(itemGroup);

            if (color != null)
            {
                item.ColorScheme.CompositeItemPressedBackground1.SetColor(QColorScheme.Global.CurrentTheme, color.Value);
                item.ColorScheme.CompositeItemPressedBackground2.SetColor(QColorScheme.Global.CurrentTheme, color.Value);
                item.ColorScheme.CompositeItemBackground1.SetColor(QColorScheme.Global.CurrentTheme, color.Value);
                item.ColorScheme.CompositeItemBackground2.SetColor(QColorScheme.Global.CurrentTheme, color.Value);
            }
            item.ItemName = itemName;
            item.Configuration.Margin = new QMargin(3, 3, 3, 3);
            item.Configuration.Padding = new QPadding(4, 4, 4, 4);
            if (direction == QPartDirection.Horizontal)
            {
                item.Configuration.StretchHorizontal = true;
            }
            else
            {
                item.Configuration.StretchVertical = true;
            }
            item.Configuration.Appearance.BorderWidth = borderWidth;
            return item;
        }

        private static QCompositeGroup GetSolidColorCompositeGroup(QPartDirection direction, Color? color, int borderWidth, bool stretchHorizontal = true, bool stretchVertical = true)
        {
            var group = new QCompositeGroup { Configuration = { Direction = direction } };
            if (color != null)
            {
                group.ColorScheme.CompositeItemHotBackground1.SetColor(QColorScheme.Global.CurrentTheme, color.Value);
                group.ColorScheme.CompositeItemHotBackground2.SetColor(QColorScheme.Global.CurrentTheme, color.Value);
                group.ColorScheme.CompositeItemExpandedBackground1.SetColor(QColorScheme.Global.CurrentTheme, color.Value);
                group.ColorScheme.CompositeItemExpandedBackground2.SetColor(QColorScheme.Global.CurrentTheme, color.Value);
                group.ColorScheme.CompositeItemPressedBackground1.SetColor(QColorScheme.Global.CurrentTheme, color.Value);
                group.ColorScheme.CompositeItemPressedBackground2.SetColor(QColorScheme.Global.CurrentTheme, color.Value);                
                group.ColorScheme.CompositeItemBackground1.SetColor(QColorScheme.Global.CurrentTheme, color.Value);
                group.ColorScheme.CompositeItemBackground2.SetColor(QColorScheme.Global.CurrentTheme, color.Value);
                group.ColorScheme.CompositeItemDisabledBackground1.SetColor(QColorScheme.Global.CurrentTheme, color.Value);
                group.ColorScheme.CompositeItemDisabledBackground2.SetColor(QColorScheme.Global.CurrentTheme, color.Value);
                group.ColorScheme.CompositeItemPressedBackground1.SetColor(QColorScheme.Global.CurrentTheme, color.Value);
                group.ColorScheme.CompositeItemPressedBackground2.SetColor(QColorScheme.Global.CurrentTheme, color.Value);
                group.Configuration.Appearance.BorderWidth = borderWidth;
            }
            if (stretchHorizontal)
            {
                group.Configuration.StretchHorizontal = true;
            }
            if (stretchVertical)
            {
                group.Configuration.StretchVertical = true;
            }
            return group;
        }

        private static QCompositeImage GetImageFromResource(string name)
        {
            var image =  Resources.ResourceManager.GetObject(name.Replace(" ", "")) ?? new Bitmap(64,48);
            try
            {
                return new QCompositeImage { Image = (Image)image };
            }
            catch (Exception)
            {
                return new QCompositeImage { Image = new Bitmap(64, 48) };
            }            
        }
    }
}
