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

            var result = AddWordsToBuilder(words, fontSizeProvider, fontSettings).Bind(() => wordLayoutBuilder.Build(center))
                                                                                 .Bind(wordRectangles => CreateWordPlates(wordRectangles, fontSizeProvider, fontSettings));
            return result;
        }

        private static Result<WordPlate[]> CreateWordPlates(WordRectangle[] wordRectangles, IWordFontSizeProvider fontSizeProvider, WordFontSettings fontSettings)
        {
            var wordPlates = new List<WordPlate>();
            foreach (var wordRectangle in wordRectangles)
            {
                var result = fontSizeProvider.GetFontSize(wordRectangle.Word)
                                             .OnSuccess(r =>
                                             {
                                                 var font = new Font(fontSettings.FontFamily, r.Value);
                                                 wordPlates.Add(new WordPlate() { Font = font, WordRectangle = wordRectangle });
                                             });
                if (result.IsFailed)
                    return result.ToResult();
            }
            return Result.Ok(wordPlates.ToArray());
        }

        private Result AddWordsToBuilder(IEnumerable<string> words, IWordFontSizeProvider provider, WordFontSettings settings)
        {
            var graphics = Graphics.FromImage(new Bitmap(1, 1));
            foreach (var word in words)
            {
                var result = provider.GetFontSize(word)
                                     .OnSuccess(r =>
                                     {
                                         var font = new Font(settings.FontFamily, r.Value);
                                         var floatSize = graphics.MeasureString(word, font);
                                         wordLayoutBuilder.AddWord(word, new Size((int)Math.Ceiling(floatSize.Width), (int)Math.Ceiling(floatSize.Height)));
                                     });
                if (result.IsFailed)
                    return result.ToResult();
            }

            return Result.Ok();
        }
    }
}