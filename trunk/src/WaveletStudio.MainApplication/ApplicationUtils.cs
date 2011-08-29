using System.Drawing;
using System.Drawing.Drawing2D;
using WaveletStudio.MainApplication.Properties;
using ZedGraph;

namespace WaveletStudio.MainApplication
{
    internal static class ApplicationUtils
    {
        public static string GetResourceString(string name)
        {
            var key = name.ToLower().Replace(" ", "");
            var resource = Resources.ResourceManager.GetObject(key) ?? "";
            if (resource.GetType() != typeof(string)) 
            {
                resource = Resources.ResourceManager.GetString(key + "_text") ?? "";
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
            var image = (Image) (Resources.ResourceManager.GetObject(name.ToLower().Replace(" ", "")));
            if (image == null)
                return bitmap;
            
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
    }
}
