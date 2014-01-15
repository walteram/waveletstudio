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

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using WaveletStudio.Designer.Resources;
using ZedGraph;

namespace WaveletStudio.Designer.Utils
{
    internal static class ApplicationUtils
    {
        public static string GetResourceString(string name)
        {
            var key = name.ToLower().Replace(" ", "");
            var resource = DesignerResources.ResourceManager.GetObject(key) ?? "";
            if (resource.GetType() != typeof(string)) 
            {
                resource = DesignerResources.ResourceManager.GetString(key + "_text") ?? "";
            }
            if (resource.ToString() == "")
            {
                resource = name;
            }
            return resource.ToString();
        }

        public static Image GetResourceImage(string name, int width = 64, int height = 48)
        {
            var bitmap = new Bitmap(width, height);
            var image = (Images.ResourceManager.GetObject(name.Replace(" ", ""))) as Image;
            if (image == null && name.ToLower().EndsWith("mini"))
                return GetResourceImage(name.Substring(0, name.Length - 4), width, height);
            if (image == null)
                return bitmap;
            
            return image.ResizeTo(width, height);
        }

        public static Image ResizeTo(this Image image, int width, int height)
        {
            if (image.Width < width && image.Height < height)
            {
                return image;
            }
            var bitmap = new Bitmap(width, height);
            var graph = Graphics.FromImage(bitmap);
            graph.SmoothingMode = SmoothingMode.HighQuality;
            graph.DrawImage(image, 0, 0, width, height);
            graph.Dispose();
            return bitmap;
        }

        public static void ConfigureGraph(ZedGraphControl graph, string title)
        {
            graph.ContextMenuBuilder += (sender, strip, pt, state) => strip.Items.RemoveByKey("set_default");

            var pane = graph.GraphPane;            
            pane.IsFontsScaled = false;
            pane.Title.Text = GetResourceString(title);
            pane.Title.FontSpec.Size = 12;
            pane.XAxis.Scale.FontSpec.Size = 11;
            pane.YAxis.Scale.FontSpec.Size = 11;
            pane.Legend.IsVisible = false;
            pane.XAxis.Title.IsVisible = false;
            pane.YAxis.Title.IsVisible = false;
        }

        public static string RemoveSpecialChars(this string text)
        {
            const string past = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç ";
            const string future = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc_";
            const string not = "()@$%?#\"'\\/:<>|*-+";
            for (var i = 0; i < past.Length; i++)
            {
                text = text.Replace(past[i].ToString(CultureInfo.InvariantCulture), future[i].ToString(CultureInfo.InvariantCulture));
            }
            foreach (var t in not)
            {
                text = text.Replace(t.ToString(CultureInfo.InvariantCulture), "");
            }
            return text;
        }
    }
}
