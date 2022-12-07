using System.Collections.Generic;
using TagsCloudContainer.FileOpeners;

namespace TagsCloudContainer
{
    /// <summary>
    /// Обрабатывает входной файл. На выходе - частотный словарь без скучных слов.
    /// </summary>
    public class InputFileHandler
    {
        private IFileOpener fileOpener;

        public InputFileHandler(IFileOpener fileOpener)
        {
            this.fileOpener = fileOpener;
        }

        public Dictionary<string, int> FormFrequencyDictionary()
        {
            var words = FileToWordsArray();
            var frequencyDict = MakeWordsFrequencyDictionary(words);
            var filteredDict = new BoringWordsDeleter().DeleteBoringWords(frequencyDict);
            return filteredDict;
        }

        private string[] FileToWordsArray()
        {
            var input = fileOpener.OpenFile();
            return input.Split(new[] {'\n', ' '});
        }

        private Dictionary<string, int> MakeWordsFrequencyDictionary(string[] words)
        {
            var wordOccurrences = new Dictionary<string, int>();
            foreach (var word in words)
                wordOccurrences[word.ToLower()] = wordOccurrences.ContainsKey(word.ToLower())
                    ? wordOccurrences[word.ToLower()] + 1
                    : 1;
            return wordOccurrences;
        }
    }
}