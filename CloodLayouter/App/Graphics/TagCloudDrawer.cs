using System.Collections.Generic;
using System.Drawing;
using CloodLayouter.Infrastructer;

namespace CloodLayouter.App
{
    public class TagCloudDrawer : IDrawer
    {
        private readonly ICloudLayouter cloudLayouter;

        private readonly IProvider<Bitmap> imageHolder;
        private readonly IProvider<IEnumerable<Tag>> tagProvider;

        public TagCloudDrawer(ICloudLayouter cloudLayouter, IProvider<Bitmap> imageHolder, IProvider<IEnumerable<Tag>> tagProvider)
        {
            this.cloudLayouter = cloudLayouter;
            this.imageHolder = imageHolder;
            this.tagProvider = tagProvider;
        }

        public Bitmap Draw()
        {
            using (var grapghic = Graphics.FromImage(imageHolder.Get()))
            {
                foreach (var tag in tagProvider.Get())
                {
                    var rect = cloudLayouter.PutNextRectangle(tag.Size);
                    grapghic.DrawString(tag.Word, tag.Font, new SolidBrush(Color.Blue), rect); //HARD DEOENDENCY
                }
            }

            return imageHolder.Get();
        }
    }
}