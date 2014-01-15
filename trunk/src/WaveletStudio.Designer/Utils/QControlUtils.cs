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
using Qios.DevSuite.Components.Ribbon;
using WaveletStudio.Designer.Resources;

namespace WaveletStudio.Designer.Utils
{
    public static class QControlUtils
    {
        internal static void CreatePanel(out QRibbonPanel panel, out QCompositeGroup composite, string title)
        {
            var currentStyle = QColorScheme.Global.CurrentTheme;
            panel = new QRibbonPanel {Title = title};
            panel.ColorScheme.RibbonPanelActiveBackground1.SetColor(currentStyle, Color.Empty, false);
            panel.ColorScheme.RibbonPanelActiveBackground2.SetColor(currentStyle, Color.Empty, false);
            panel.ColorScheme.RibbonPanelActiveBorder.SetColor(currentStyle, Color.Empty, false);
            panel.ColorScheme.RibbonPanelBackground1.SetColor(currentStyle, Color.Empty, false);
            panel.ColorScheme.RibbonPanelBackground2.SetColor(currentStyle, Color.Empty, false);
            panel.ColorScheme.RibbonPanelBorder.SetColor(currentStyle, Color.Empty, false);
            panel.ColorScheme.RibbonPanelCaptionArea1.SetColor(currentStyle, Color.Empty, false);
            panel.ColorScheme.RibbonPanelCaptionArea2.SetColor(currentStyle, Color.Empty, false);
            panel.ColorScheme.RibbonPanelCaptionShowDialog.SetColor(currentStyle, Color.Empty, false);
            panel.ColorScheme.RibbonPanelCaptionShowDialogDisabled.SetColor(currentStyle, Color.Empty, false);
            panel.ColorScheme.RibbonPanelCaptionShowDialogHot.SetColor(currentStyle, Color.Empty, false);
            panel.ColorScheme.RibbonPanelHotBackground1.SetColor(currentStyle, Color.Empty, false);
            panel.ColorScheme.RibbonPanelHotBackground2.SetColor(currentStyle, Color.Empty, false);
            panel.ColorScheme.RibbonPanelHotBorder.SetColor(currentStyle, Color.Empty, false);
            panel.ColorScheme.RibbonPanelHotCaptionArea1.SetColor(currentStyle, Color.Empty, false);
            panel.ColorScheme.RibbonPanelHotCaptionArea2.SetColor(currentStyle, Color.Empty, false);
            panel.ColorScheme.RibbonPanelText.SetColor(currentStyle, Color.Black, false);
            panel.ColorScheme.RibbonPanelTextActive.SetColor(currentStyle, Color.Black, false);
            panel.ColorScheme.RibbonPanelTextHot.SetColor(currentStyle, Color.Black, false);
            panel.Configuration.CaptionConfiguration.ShowDialogConfiguration.Visible = QTristateBool.False;

            composite = new QCompositeGroup();
            composite.ColorScheme.ButtonPressedBackground1.SetColor(currentStyle, Color.Empty, false);
            composite.ColorScheme.ButtonPressedBackground2.SetColor(currentStyle, Color.Empty, false);
            composite.ColorScheme.CompositeItemBackground1.SetColor(currentStyle, Color.White, false);
            composite.ColorScheme.CompositeItemBackground2.SetColor(currentStyle, Color.White, false);
            composite.ColorScheme.CompositeItemHotBackground1.SetColor(currentStyle, Color.Transparent, false);
            composite.ColorScheme.CompositeItemHotBackground2.SetColor(currentStyle, Color.Transparent, false);
            composite.ColorScheme.CompositeItemHotBorder.SetColor(currentStyle, Color.Transparent, false);
            composite.ColorScheme.CompositeItemPressedBackground1.SetColor(currentStyle, Color.White, false);
            composite.ColorScheme.CompositeItemPressedBackground2.SetColor(currentStyle, Color.White, false);
            composite.ColorScheme.Scope = QColorSchemeScope.All;
            composite.Configuration.ShrinkHorizontal = true;
            composite.Configuration.ShrinkVertical = true;
            composite.Configuration.StretchHorizontal = true;
            composite.Configuration.StretchVertical = true;

            panel.Items.Add(composite);            
        }

        public static QCompositeGroup CreateCompositeGroup(this QRibbonPage parentPage, string title, bool createSeparatorBefore = false)
        {
            QRibbonPanel panel;
            QCompositeGroup compositeGroup;
            CreatePanel(out panel, out compositeGroup, title);
            if (createSeparatorBefore)
            {
                parentPage.Items.Add(new QCompositeSeparator());
            }
            parentPage.Items.Add(panel);            
            return compositeGroup;
        }

        public static QCompositeItem CreateCompositeItem(string title, Image image)
        {
            return CreateCompositeItem(null, new QCompositeImage { Image = image }, title, true);
        }

        public static QCompositeItem CreateCompositeItem(string itemName, string imageResourceName, string title)
        {
            return CreateCompositeItem(itemName, GetImageFromResource(imageResourceName), title, false);
        }

        public static QCompositeItem CreateCompositeItem(string itemName, QCompositeImage image, string title, bool useOriginalImageSize)
        {
            var currentStyle = QColorScheme.Global.CurrentTheme;
            var item = new QCompositeItem();
            item.ColorScheme.ButtonPressedBackground1.SetColor(currentStyle, Color.White, false);
            item.ColorScheme.ButtonPressedBackground2.SetColor(currentStyle, Color.White, false);
            item.Configuration.Appearance.Shape = new QShape(QBaseShapeType.SquareButton);
            var itemGroup = GetSolidColorCompositeGroup(QPartDirection.Vertical, Color.White, 1);
            var textsGroup = GetSolidColorCompositeGroup(QPartDirection.Vertical, Color.White, 0, true, false);
            var fontDefinition = new QFontDefinition { Bold = true, Size = -1 };
            image.Configuration.AlignmentHorizontal = QPartAlignment.Centered;
            itemGroup.Items.Add(image);            
            if (!useOriginalImageSize)
            {
                image.Configuration.MaximumSize = new Size(36, 27);
            }
            textsGroup.Items.Add(new QCompositeText
            {
                Title = title,
                Configuration =
                {
                    AlignmentHorizontal = QPartAlignment.Centered,
                    FontDefinition = fontDefinition,
                    FontDefinitionHot = fontDefinition,
                    FontDefinitionPressed = fontDefinition
                }
            });
            itemGroup.Items.Add(textsGroup);
            item.Items.Add(itemGroup);
            item.ItemName = itemName;
            item.Configuration.Margin = new QMargin(3, 3, 3, 3);
            item.Configuration.Padding = new QPadding(4, 4, 4, 4);
            item.Configuration.StretchVertical = true;
            item.Configuration.Appearance.BorderWidth = 1;
            item.Configuration.MinimumSize = new Size(70, 50);
            return item;
        }

        private static QCompositeGroup GetSolidColorCompositeGroup(QPartDirection direction, Color? color, int borderWidth, bool stretchHorizontal = true, bool stretchVertical = true)
        {
            var group = new QCompositeGroup { Configuration = { Direction = direction } };
            group.Configuration.Appearance.Shape = new QShape(QBaseShapeType.SquareButton);
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
                group.ColorScheme.CompositeItemPressedBorder.SetColor(QColorScheme.Global.CurrentTheme, Color.Empty);
                group.ColorScheme.CompositeItemHotBorder.SetColor(QColorScheme.Global.CurrentTheme, Color.Empty);
                group.ColorScheme.CompositeGroupBackground1.SetColor(QColorScheme.Global.CurrentTheme, color.Value);
                group.ColorScheme.CompositeGroupBackground2.SetColor(QColorScheme.Global.CurrentTheme, color.Value);

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
            var image =  Images.ResourceManager.GetObject(name.Replace(" ", "")) ?? new Bitmap(64,48);
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
