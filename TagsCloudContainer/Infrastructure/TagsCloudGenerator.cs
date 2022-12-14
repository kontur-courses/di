using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.Infrastructure.Settings;
using TagsCloudContainer.Infrastructure.WordFontSizeProviders.Factories;
using TagsCloudContainer.Infrastructure.WordLayoutBuilders;

namespace TagsCloudContainer.Infrastructure
{
    public class TagsCloudGenerator : ITagsCloudGenerator
    {
        private readonly IWordLayoutBuilder wordLayoutBuilder;
        private readonly IWordFontSizeProviderFactory fontSizeProviderFactory;

        public TagsCloudGenerator(IWordLayoutBuilder wordLayoutBuilder, IWordFontSizeProviderFactory fontSizeProviderFactory)
        {
            this.wordLayoutBuilder = wordLayoutBuilder;
            this.fontSizeProviderFactory = fontSizeProviderFactory;
        }

        public WordPlate[] GeneratePlates(IEnumerable<string> words, PointF center, WordFontSettings fontSettings)
        {
            var fontSizeProvider = fontSizeProviderFactory.CreateDefault(fontSettings.FontSizeSettings);

            var graphics = Graphics.FromImage(new Bitmap(1, 1));
            foreach (var word in words)
            {
                var font = new Font(fontSettings.FontFamily, fontSizeProvider.GetFontSize(word));
                var floatSize = graphics.MeasureString(word, font);
                wordLayoutBuilder.AddWord(word, new Size((int)Math.Ceiling(floatSize.Width), (int)Math.Ceiling(floatSize.Height)));
            }

            var wordRectangles = wordLayoutBuilder.Build(center);
            return wordRectangles.Select(wr => new WordPlate()
            {
                Font = new Font(fontSettings.FontFamily, fontSizeProvider.GetFontSize(wr.Word)),
                WordRectangle = wr
            }).ToArray();
        }
    }
}