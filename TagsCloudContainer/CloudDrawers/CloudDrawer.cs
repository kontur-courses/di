using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using TagsCloudContainer.CloudBuilder;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.CloudDrawers
{
    public class CloudDrawer : ICloudDrawer
    {
        private ICloudBuilder cloudBuilder { get; set; }
        private int height { get; set; }
        private int width { get; set; }
        private string outputFile { get; set; }

        public CloudDrawer(ICloudBuilder cloudBuilder, ImageSettings imageSettings)
        {
            this.cloudBuilder = cloudBuilder;
            this.height = imageSettings.Heigth;
            this.width = imageSettings.Width;
            this.outputFile = imageSettings.OutputFile;
        }

        public void Draw()
        {
            var bmp = new Bitmap(height, width);
            var graphics = Graphics.FromImage(bmp);
            cloudBuilder.BuildTagsCloud()
                .ToList()
                .ForEach(tag => graphics.DrawString(tag.Word, tag.Font, Brushes.Red, tag.Rectangle.Location));
            bmp.Save(outputFile, ImageFormat.Png);
        }
    }
}