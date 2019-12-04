using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using TagCloudVisualization.Infrastructure.Common;
using TagsCloudGenerator.Core.Drawers;
using TagsCloudGenerator.Core.Translators;

namespace TagCloudVisualization.Core.Painter
{
    public class TagCloudPainter
    {
        public delegate TagCloudPainter Factory(TagCloudSettings tagCloudSettings);

        private readonly IImageHolder imageHolder;
        private readonly TagCloudSettings tagCloudSettings;
        private readonly ImageSettings imageSettings;
        private readonly Palette palette;
        private readonly Func<float, float, TextToTagsTranslator> textToTagsTranslatorFactory;
        private readonly float size;

        public TagCloudPainter(IImageHolder imageHolder,
            TagCloudSettings tagCloudSettings, Palette palette, ImageSettings imageSettings,
            Func<float, float, TextToTagsTranslator> textToTagsTranslatorFactory)
        {
            this.imageHolder = imageHolder;
            this.tagCloudSettings = tagCloudSettings;
            this.palette = palette;
            this.imageSettings = imageSettings;
            this.textToTagsTranslatorFactory = textToTagsTranslatorFactory;
            var imageSize = imageHolder.GetImageSize();
            size = Math.Min(imageSize.Width, imageSize.Height) / 2.1f;
        }

        public void Paint()
        {
            using (var graphics = imageHolder.StartDrawing())
            {
                var translator = textToTagsTranslatorFactory(tagCloudSettings.SpiralAlpha, tagCloudSettings.StepPhi);
                var tags = translator.TranslateTextToTags(
                    File.ReadLines(tagCloudSettings.TextFilename),
                    new HashSet<string>());

                var cloudDrawer =
                    new RectangleCloudDrawer(palette.BackgroundColor, new SolidBrush(palette.PrimaryColor));
                var bitmap = cloudDrawer.DrawCloud(tags.ToList());
                imageSettings.Width = bitmap.Width;
                imageSettings.Height = bitmap.Height;
                graphics.DrawImage(bitmap, 0, 0);
            }

            imageHolder.UpdateUi();
        }
    }
}