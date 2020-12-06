using System.Drawing;
using TagCloud.Infrastructure.Graphics;
using TagCloud.Infrastructure.Text;

namespace TagCloud.App
{
    public class TagCloudGenerator : IImageGenerator
    {
        private readonly IReader<string> reader;
        private readonly  ITokenAnalyzer<string> wordAnalyzer;
        private readonly IPainter<string> painter;

        public TagCloudGenerator(IReader<string> reader, ITokenAnalyzer<string> wordAnalyzer, IPainter<string> painter)
        {
            this.reader = reader;
            this.wordAnalyzer = wordAnalyzer;
            this.painter = painter;
        }

        public Image Generate()
        {
            var tokens = reader.ReadTokens();
            var analyzedTokens = wordAnalyzer.Analyze(tokens);
            return painter.GetImage(analyzedTokens);
        }
    }
}