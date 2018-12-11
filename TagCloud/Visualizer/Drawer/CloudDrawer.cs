using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Models;
using TagCloud.Visualizer.Settings;

namespace TagCloud.Visualizer.Drawer
{
    public class CloudDrawer : ICloudDrawer
    {
        private Graphics graphics;

        private readonly StringFormat format = new StringFormat(StringFormat.GenericTypographic)
        {
            Alignment = StringAlignment.Center,
            Trimming = StringTrimming.None,
            FormatFlags = StringFormatFlags.NoWrap
        };

        public void Draw(Graphics graphics, IList<CloudItem> cloudItems, IDrawSettings settings)
        {
            this.graphics = graphics;

            if (settings.DrawFormat != DrawFormat.OnlyWords)
                graphics.DrawRectangles(Pens.Black, cloudItems.Select(t => t.Bounds).ToArray());

            if (settings.DrawFormat == DrawFormat.OnlyWords || settings.DrawFormat == DrawFormat.WordsInRectangles)
                DrawAsString(cloudItems, settings);

            if (settings.DrawFormat == DrawFormat.RectanglesWithNumeration)
                DrawNumeration(cloudItems, settings);
        }

        private void DrawAsString(IEnumerable<CloudItem> cloudItems, IDrawSettings settings)
        {
            foreach (var cloudItem in cloudItems)
            {
                graphics.DrawString(
                    cloudItem.Word,
                    cloudItem.Font,
                    settings.Colorizer.GetBrush(cloudItem),
                    cloudItem.Bounds,
                    format);
            }
        }

        private void DrawNumeration(IList<CloudItem> cloudItems, IDrawSettings settings)
        {
            for (var i = 0; i < cloudItems.Count; i++)
            {
                graphics.DrawString(
                    i.ToString(),
                    cloudItems[i].Font,
                    settings.Colorizer.GetBrush(cloudItems[i]),
                    cloudItems[i].Bounds,
                    format);
            }
        }
    }
}