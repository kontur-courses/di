using System.Drawing;
using System.Linq;
using TagsCloudContainer.GettingTokens;
using TagsCloudContainer.Infrastructure.PointTracks;
using TagsCloudContainer.Visualization;

namespace TagsCloudContainer
{
    public class TagsCloudGenerator
    {
        public Bitmap GenerateTagsCloud(string text, TagsCloudSettings settings) =>
            new Tokenizer()
                .GetTokens(text)
                .Where(token => token.WordType != WordType.None)
                .Where(token => token.Word.Length > 3)
                .Select(token => token.Word)
                .Where(word => !settings.StopWords.Contains(word))
                .SortByFrequency()
                .Visualize(settings);
    }
}