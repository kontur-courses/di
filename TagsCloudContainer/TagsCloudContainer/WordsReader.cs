using System.IO;

namespace TagsCloudContainer
{
    class WordsReader: IWordsReader
    {
        public IWordStorage ReadWords(string path, IWordStorage wordStorage)
        {
            var words = File.ReadAllLines(path);

            wordStorage.Add(words);

            return wordStorage;
        }
    }
}
