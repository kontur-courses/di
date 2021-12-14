using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudDrawer.ImageSaveService;
using TagsCloudDrawer.ImageSettings;
using TagsCloudVisualization.CloudLayouter;
using TagsCloudVisualization.CloudLayouter.VectorsGenerator;
using TagsCloudVisualization.Drawable.Tags.Settings;
using TagsCloudVisualization.Drawable.Tags.Settings.TagColorGenerator;
using TagsCloudVisualization.WordsPreprocessor;

namespace TagsCloudVisualization
{
    public class TagsCloudVisualisationSettings
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

        public ILayouter Layouter { get; init; } =
            new NonIntersectedLayouter(Point.Empty, new CircularVectorsGenerator(0.005, 360));

        public IEnumerable<string> BoringWords { get; init; } = Array.Empty<string>();

        public IImageSaveService ImageSaveService { get; init; } = new PngSaveService();

        public IEnumerable<IWordsPreprocessor> WordsPreprocessors { get; init; } =
            Enumerable.Empty<IWordsPreprocessor>();
    }
}