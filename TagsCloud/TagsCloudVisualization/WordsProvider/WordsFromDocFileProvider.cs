using System.Collections.Generic;
using Xceed.Words.NET;

namespace TagsCloudVisualization.WordsProvider
{
    public class WordsFromDocFileProvider : WordsFromFileProvider
    {
        public WordsFromDocFileProvider(string pathToFile) : base(pathToFile)
        {
        }

        protected override IEnumerable<string> GetText()
        {
            yield return DocX.Load(PathToFile).Text;
        }
    }
}