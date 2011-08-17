using System.Drawing;
using Qios.DevSuite.Components;
using WaveletStudio.MainApplication.Properties;

namespace WaveletStudio.MainApplication.Controls
{
    public static class QControlUtils
    {
        public static QCompositeItem CreateCompositeListItem(string itemName, string imageResourceName, string title, string text, int borderWidth, QPartDirection direction, QPartAlignment textAlignment, Color? color)
        {
            var item = new QCompositeItem();
            var image = QControlUtils.GetImageFromResource(imageResourceName);
            var itemGroup = QControlUtils.GetSolidColorCompositeGroup(direction, color, 1);
            var textsGroup = QControlUtils.GetSolidColorCompositeGroup(QPartDirection.Vertical, color, 0);
            var fontDefinition = new QFontDefinition { Bold = true, Size = -1 };
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
            item.Configuration.Appearance.BorderWidth = borderWidth;
            return item;
        }

        public static QCompositeGroup GetSolidColorCompositeGroup(QPartDirection direction, Color? color, int borderWidth)
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
            if (direction == QPartDirection.Vertical)
            {
                group.Configuration.StretchHorizontal = true;
            }
            else
            {
                group.Configuration.StretchVertical = true;
            }
            return group;
        }

        public static QCompositeImage GetImageFromResource(string name)
        {
            var image =  Resources.ResourceManager.GetObject(name) ?? new Bitmap(64,48);
            return new QCompositeImage {Image = (Image)image};
        }
    }
}
