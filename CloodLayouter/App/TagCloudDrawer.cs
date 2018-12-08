using System.Drawing;
using CloodLayouter.Infrastructer;

namespace CloodLayouter.App
{
    public class TagCloudDrawer : IDrawer
    {
        private readonly ICloudLayouter cloudLayouter;

        private readonly IImageHolder imageHolder;
        private readonly ITagProvider tagProvider;

        public TagCloudDrawer(ICloudLayouter cloudLayouter, IImageHolder imageHolder, ITagProvider tagProvider)
        {
            this.cloudLayouter = cloudLayouter;
            this.imageHolder = imageHolder;
            this.tagProvider = tagProvider;
        }

        public Bitmap Draw()
        {
            using (var grapghic = Graphics.FromImage(imageHolder.Image))
            {
                foreach (var tag in tagProvider.GetTags())
                {
                    var rect = cloudLayouter.PutNextRectangle(tag.Size);
                    grapghic.DrawString(tag.Word, tag.Font, new SolidBrush(Color.Blue), rect); //HARD DEOENDENCY
                }
            }

            return imageHolder.Image;
        }
    }
}