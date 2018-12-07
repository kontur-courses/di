using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudContainer.Settings;
using TagsCloudContainer.Tags;
using TagsCloudContainer.Themes;

namespace TagsCloudContainer.CloudDrawers
{
    public class CloudDrawer : ICloudDrawer
    {
        private readonly ImageSettings imageSettings;
        private readonly ITheme theme;

        public CloudDrawer(ImageSettings imageSettings, ITheme theme)
        {
            this.imageSettings = imageSettings;
            this.theme = theme;
        }

        public void Draw(IEnumerable<Tag> tagsCloud)
        {
            var bmp = new Bitmap(imageSettings.Height, imageSettings.Width);
            using (var graphics = Graphics.FromImage(bmp))
            {
                graphics.FillRectangle(theme.BackgroundColor, 0, 0, imageSettings.Height, imageSettings.Width);
                foreach (var tag in tagsCloud)
                    graphics.DrawString(tag.Word, tag.Font, theme.WordColor, tag.Rectangle.Location);
            }

            bmp.Save(imageSettings.OutputFile, ImageFormat.Png);
        }
    }
}