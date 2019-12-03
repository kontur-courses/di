using System.Collections.Generic;
using System.Linq;
using TagsCloud.Interfaces;

namespace TagsCloud.FileReader
{
    public class WordStream : IWordStream
    {
        private readonly IWordHandler wordHandler;
        private readonly ITextSpliter textSpliter;
        private readonly IFileReader fileReader;
        private readonly IWordValidator wordValidator;

        public WordStream(IWordHandler wordHandler, ITextSpliter textSpliter, IFileReader fileReader, IWordValidator wordValidator)
        {
            this.textSpliter = textSpliter;
            this.wordHandler = wordHandler;
            this.fileReader = fileReader;
            this.wordValidator = wordValidator;
        }

        public IEnumerable<string> GetWords(string path)
        {
            var text = fileReader.ReadFile(path);
            var result = new List<string>();
            foreach(var word in textSpliter.SplitText(text))
            {
                var newWord = wordHandler.ProseccWord(word);
                if (!wordValidator.ISValidWord(newWord))
                    continue;
                result.Add(newWord);
            }
            return result;
        }
    }
}
