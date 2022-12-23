using FluentResults;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.Infrastructure.Settings;
using TagsCloudContainer.Infrastructure.WordFontSizeProviders;
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

        public Result<WordPlate[]> GeneratePlates(IEnumerable<string> words, PointF center, WordFontSettings fontSettings)
        {
            var fontSizeProvider = fontSizeProviderFactory.CreateDefault(fontSettings.FontSizeSettings);
            
            wordLayoutBuilder.Clear();
            var addWordsResult = AddWordsToBuilder(words, fontSizeProvider, fontSettings);
            if (addWordsResult.IsFailed)
                return addWordsResult;

            var wordRectanglesResult = wordLayoutBuilder.Build(center);
            if (wordRectanglesResult.IsFailed)
                return wordRectanglesResult.ToResult();

            var wordPlates = new List<WordPlate>();
            foreach(var wordRectangle in wordRectanglesResult.Value)
            {
                var fontSizeResult = fontSizeProvider.GetFontSize(wordRectangle.Word);
                if (fontSizeResult.IsFailed)
                    return fontSizeResult.ToResult();

                var font = new Font(fontSettings.FontFamily, fontSizeResult.Value);
                wordPlates.Add(new WordPlate() { Font = font, WordRectangle = wordRectangle });
            }
            return Result.Ok(wordPlates.ToArray());
        }

        private Result AddWordsToBuilder(IEnumerable<string> words, IWordFontSizeProvider provider, WordFontSettings settings)
        {
            var graphics = Graphics.FromImage(new Bitmap(1, 1));
            foreach (var word in words)
            {
                var fontSizeResult = provider.GetFontSize(word);
                if (fontSizeResult.IsFailed)
                    return Result.Fail(fontSizeResult.Errors);

                var font = new Font(settings.FontFamily, fontSizeResult.Value);
                var floatSize = graphics.MeasureString(word, font);
                wordLayoutBuilder.AddWord(word, new Size((int)Math.Ceiling(floatSize.Width), (int)Math.Ceiling(floatSize.Height)));
            }

            return Result.Ok();
        }
    }
}