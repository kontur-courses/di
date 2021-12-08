using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudDrawer.ImageSaveService;
using TagsCloudDrawer.ImageSettings;
using TagsCloudVisualization.CloudLayouter;
using TagsCloudVisualization.DrawerSettingsProvider;
using TagsCloudVisualization.DrawerSettingsProvider.TagColorGenerator;
using TagsCloudVisualization.WordsPreprocessor;

namespace TagsCloudVisualization
{
    public class TagsCloudDrawerModuleSettings
    {
        public string WordsFile { get; init; }

        public ImageSettingsProvider ImageSettingsProvider { get; init; } = new()
        {
            BackgroundColor = Color.Gray,
            ImageSize = new Size(1000, 1000)
        };

        public TagDrawableSettingsProvider TagDrawableSettingsProvider { get; init; } = new()
        {
            Font = new FontSettings
            {
                Family = "Arial",
                MaxSize = 50
            },
            ColorGenerator = new StrengthAlphaTagColorGenerator(Color.Red)
        };

        public ILayouter Layouter { get; init; } = new CircularLayouter(Point.Empty);

        public IEnumerable<string> BoredWords { get; init; } = Array.Empty<string>();

        public Func<IImageSavior> ImageSavior { get; init; } = () => new PngSavior();

        public IEnumerable<IWordsPreprocessor> WordsPreprocessors { get; init; } =
            Enumerable.Empty<IWordsPreprocessor>();
    }
}