using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using TagsCloudContainer.CloudBuilder;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.CloudDrawers
{
    public class CloudDrawer : ICloudDrawer
    {
        public CloudDrawer(ICloudBuilder cloudBuilder, ImageSettings imageSettings)
        {
            this.cloudBuilder = cloudBuilder;
            height = imageSettings.Height;
            width = imageSettings.Width;
            outputFile = imageSettings.OutputFile;
        }

        private ICloudBuilder cloudBuilder { get; }
        private int height { get; }
        private int width { get; }
        private string outputFile { get; }

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