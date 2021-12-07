using System.Drawing;
using TagCloud.configurations;
using TagCloud.repositories;

namespace TagCloud.visual
{
    public class TagVisualizer : IVisualizer
    {
        private readonly IRepository<Tag> tagRepository;
        private readonly IImageConfiguration imageConfiguration;

        public TagVisualizer(IRepository<Tag> tagRepository, IImageConfiguration imageConfiguration)
        {
            this.tagRepository = tagRepository;
            this.imageConfiguration = imageConfiguration;
        }

        public Image GetImage()
        {
            using var bitmap = new Bitmap(imageConfiguration.GetWidth(), imageConfiguration.GetHeight());
            using var graphics = Graphics.FromImage(bitmap);
            
            graphics.Clear(imageConfiguration.GetBackgroundColor());
            foreach (var tag in tagRepository.Get())
            {
                var brush = new SolidBrush(tag.GetConfiguration().GetColor());
                //TODO offset
                graphics.DrawString(tag.GetText(), tag.GetConfiguration().GetFont(), brush, tag.GetLayoutRectangle());
            }
            
            return bitmap;
        }
    }
}