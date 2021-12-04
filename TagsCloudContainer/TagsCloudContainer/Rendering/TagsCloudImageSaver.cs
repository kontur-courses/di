using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.Rendering
{
    public interface ITagsCloudImageSaver
    {
        void Save(IEnumerable<WordStyle> words, Size imageSize);
    }

    public class TagsCloudImageSaver : ITagsCloudImageSaver
    {
        private readonly ITagsCloudRenderer tagsCloudRenderer;
        private readonly ISaveSettings saveSettings;

        public TagsCloudImageSaver(ITagsCloudRenderer tagsCloudRenderer, ISaveSettings saveSettings)
        {
            this.tagsCloudRenderer = tagsCloudRenderer ?? throw new ArgumentNullException(nameof(tagsCloudRenderer));
            this.saveSettings = saveSettings ?? throw new ArgumentNullException(nameof(saveSettings));
        }

        public void Save(IEnumerable<WordStyle> words, Size imageSize)
        {
            if (words == null)
                throw new ArgumentNullException(nameof(words));

            using var bitmap = tagsCloudRenderer.GetBitmap(words, imageSize);
            bitmap.Save(saveSettings.OutputFile, saveSettings.ImageFormat);
        }
    }
}