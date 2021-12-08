using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.Settings.Interfaces;

namespace TagsCloudContainer.Rendering
{
    public class TagsCloudImageSaver : ITagsCloudImageSaver
    {
        private readonly ITagsCloudRenderer tagsCloudRenderer;
        private readonly ISaveSettings saveSettings;

        public TagsCloudImageSaver(ITagsCloudRenderer tagsCloudRenderer, ISaveSettings saveSettings)
        {
            this.tagsCloudRenderer = tagsCloudRenderer;
            this.saveSettings = saveSettings;
        }

        public void Save(IEnumerable<WordStyle> words, Size imageSize)
        {
            using var bitmap = tagsCloudRenderer.GetBitmap(words, imageSize);
            bitmap.Save(saveSettings.OutputFile, saveSettings.ImageFormat);
        }

        public void Dispose()
        {
            tagsCloudRenderer.Dispose();
        }
    }
}