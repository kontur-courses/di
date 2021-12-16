using System.Collections.Generic;
using TagsCloudVisualization.Parsers;
using TagsCloudVisualization.TextPreparers;

namespace TagsCloudVisualization.TextHandlers
{
    public class TextHandler : ITextHandler
    {
        private readonly IParser parser;
        private readonly ITextPreparer preparer;

        public TextHandler(ITextPreparer preparer, IParser parser)
        {
            this.preparer = preparer;
            this.parser = parser;
        }
        
        public IEnumerable<string> Handle(string filePath)
        {
            var words = parser.ParseWords(filePath);

            return preparer.PrepareText(words);
        }
    }
}