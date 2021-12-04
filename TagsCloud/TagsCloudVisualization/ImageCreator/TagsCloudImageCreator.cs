using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.ImageSavior;
using TagsCloudVisualization.TagsCloudDrawer;

namespace TagsCloudVisualization.ImageCreator
{
    public class TagsCloudImageCreator : ITagsCloudImageCreator
    {
        private readonly ITagsCloudDrawer _drawer;
        private readonly IImageSavior _savior;
        private readonly IImageSettingsProvider _settingsProvider;

        public TagsCloudImageCreator(ITagsCloudDrawer drawer, IImageSavior savior,
            IImageSettingsProvider settingsProvider)
        {
            _drawer = drawer;
            _savior = savior;
            _settingsProvider = settingsProvider;
        }

        public void Create(string filename, IEnumerable<Tag> tags)
        {
            var size = _settingsProvider.ImageSize;
            using var bitmap = new Bitmap(size.Width, size.Height);
            using var graphics = Graphics.FromImage(bitmap);
            graphics.Clear(_settingsProvider.BackgroundColor);
            _drawer.Draw(graphics, bitmap.Size, tags);
            _savior.Save(filename, bitmap);
        }
    }
}