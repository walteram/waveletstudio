using System.Drawing;
using Qios.DevSuite.Components;
using WaveletStudio.MainApplication.Properties;

namespace WaveletStudio.MainApplication.Controls
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
                textsGroup.Items.Add(new QCompositeText { Title = text, Configuration = { AlignmentHorizontal = textAlignment } });
            }
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
            return new QCompositeImage {Image = (Image)image};
        }
    }
}
