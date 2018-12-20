using System.Collections.Generic;
using System.Drawing;
using CloodLayouter.Infrastructer;

namespace CloodLayouter.App
{
    public class TagCloudDrawer : IDrawer
    {
        private readonly ICloudLayouter cloudLayouter;
        private readonly ImageSettings imageSettings;
        private readonly IProvider<IEnumerable<Tag>> tagProvider;

        public TagCloudDrawer(ICloudLayouter cloudLayouter,
            IProvider<IEnumerable<Tag>> tagProvider, ImageSettings imageSettings)
        {
            this.cloudLayouter = cloudLayouter;
            this.imageSettings = imageSettings;
            this.tagProvider = tagProvider;
        }

        public Bitmap Draw()
        {
            var bitmap = new Bitmap(imageSettings.Width, imageSettings.Height);
            using (var grapghic = Graphics.FromImage(bitmap))
            {
                foreach (var tag in tagProvider.Get())
                {
                    var rect = cloudLayouter.PutNextRectangle(tag.Size);
                    grapghic.DrawString(tag.Word, tag.Font, new SolidBrush(Color.Blue), rect); //HARD DEOENDENCY
                }
            }

            return bitmap;
        }
    }
}