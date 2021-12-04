using System;
using System.Collections.Generic;
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

        public void Create(string filename, IEnumerable<Tag> tags) => throw new NotImplementedException();
    }
}