using System.Collections.Generic;

namespace TagCloud.TextProcessing
{
    public interface IMorphologyAnalyzer
    {
        public IEnumerable<ILexeme?> GetLexemesFrom(string filePath);
    }
}