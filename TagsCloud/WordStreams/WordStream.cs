using System.Collections.Generic;
using System.Linq;
using TagsCloud.Interfaces;

namespace TagsCloud.WordStreams
{
    public class WordStream : IWordStream
    {
        private readonly IWordHandler wordHandler;
        private readonly ITextSpliter textSpliter;
        private readonly ITextReader fileReader;
        private readonly IWordValidator wordValidator;

        public WordStream(IWordHandler wordHandler, ITextSpliter textSpliter, ITextReader fileReader, IWordValidator wordValidator)
        {
            this.textSpliter = textSpliter;
            this.wordHandler = wordHandler;
            this.fileReader = fileReader;
            this.wordValidator = wordValidator;
        }

        public IEnumerable<string> GetWords(string path)
        {
            var text = fileReader.ReadFile(path);
            return textSpliter.SplitText(text).Select(word => wordHandler.ProseccWord(word)).Where(newWord => wordValidator.IsValidWord(newWord));
        }
    }
}
