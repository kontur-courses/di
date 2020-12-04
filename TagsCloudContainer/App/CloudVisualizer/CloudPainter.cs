using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.App.CloudGenerator;
using TagsCloudContainer.App.Settings;
using TagsCloudContainer.Infrastructure.CloudVisualizer;

namespace TagsCloudContainer.App.CloudVisualizer
{
    internal class CloudPainter : ICloudPainter
    {
        private readonly AppSettings settings;

        public CloudPainter(AppSettings settings)
        {
            this.settings = settings;
        }

        public void Paint(IEnumerable<Tag> cloud, Graphics g)
        {
            using var brush = new SolidBrush(Color.White);
            foreach (var tag in cloud)
                g.DrawString(tag.Word, new Font(settings.FontSettings.FontName, (float) tag.FontSize), brush, tag.Location);
        }
    }
}