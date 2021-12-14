using System.Drawing;
using TagCloud.configurations;
using TagCloud.repositories;

namespace TagCloud.visual
{
    public class TagVisualizer : IVisualizer
    {
        private readonly IRepository<Tag> tagRepository;

        public TagVisualizer(IRepository<Tag> tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        public Image GetImage(IImageConfiguration imageConfiguration)
        {
            var bitmap = new Bitmap(imageConfiguration.GetWidth(), imageConfiguration.GetHeight());
            var graphics = Graphics.FromImage(bitmap);

            var offset = new Point(imageConfiguration.GetWidth() / 2, imageConfiguration.GetHeight() / 2);
            graphics.Clear(imageConfiguration.GetBackgroundColor());
            foreach (var tag in tagRepository.Get())
            {
                var brush = new SolidBrush(tag.GetColor());
                var layoutRectangle = tag.GetLayoutRectangle();
                layoutRectangle.Offset(offset);
                graphics.DrawString(
                    tag.GetText(),
                    tag.GetFont(),
                    brush,
                    layoutRectangle
                );
            }

            return bitmap;
        }
    }
}