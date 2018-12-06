using System.IO;

namespace TagsCloudContainer
{
    internal class WordsReader: IWordsReader
    {
        public IWordStorage ReadWords(string path, IWordStorage wordStorage)
        {
            var words = File.ReadAllLines(path);

            wordStorage.Add(words);

            return wordStorage;
        }
    }
}
