using System.Collections.Generic;
using System.Text;

namespace TagCloud.Words.Writing
{
    public interface IWordsWriter
    {
        void TypeToConsole();

        void WriteToConsole(IEnumerable<string> words);

        void WriteToFile(string pathToFile, IEnumerable<string> words, Encoding encoding);
    }
}