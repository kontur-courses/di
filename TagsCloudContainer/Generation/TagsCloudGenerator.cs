using System.Drawing;
using System.Linq;
using TagsCloudContainer.GettingTokens;
using TagsCloudContainer.Visualization;

namespace TagsCloudContainer.Generation
{
    public class TagsCloudGenerator
    {
        private readonly ITokenizer tokenizer;
        private readonly IVisualizer visualizer;
        
        public TagsCloudGenerator(ITokenizer tokenizer, IVisualizer visualizer)
        {
            this.tokenizer = tokenizer;
            this.visualizer = visualizer;
        }
        
        public Bitmap GenerateTagsCloud(string text, TagsCloudSettings settings) =>
            tokenizer
                .GetTokens(text)
                .Where(token => token.WordType != WordType.None)
                .Where(token => token.Word.Length > 3)
                .Select(token => token.Word)
                .Where(word => !settings.StopWords.Contains(word))
                .SortByFrequency()
                .Visualize(settings, visualizer);
    }
}