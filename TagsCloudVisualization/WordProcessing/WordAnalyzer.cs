using System;
using System.Collections.Generic;
using System.Linq;
using NHunspell;
using TagsCloudVisualization.InterfacesForSettings;
using TagsCloudVisualization.WordProcessing.FileHandlers;


namespace TagsCloudVisualization.WordProcessing
{
    public class WordAnalyzer
    {
        public IWordsSettings WordsSettings { get; set; }
        private readonly Hunspell hunspell;

        public WordAnalyzer(IWordsSettings wordsSettings)
        {
            WordsSettings = wordsSettings;
            hunspell = new Hunspell($"{AppDomain.CurrentDomain.BaseDirectory}/RuDictionary/affRu.aff",
                $"{AppDomain.CurrentDomain.BaseDirectory}/RuDictionary/dicRu.dic");
        }

        public Dictionary<string, int> MakeWordFrequencyDictionary()
        {
            var fileType = RecognizeFileType(WordsSettings.PathToFile);
            var words = fileType.ReadFile();
            words = FilterWords(words);
            var dictionary = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (dictionary.ContainsKey(word))
                    dictionary[word]++;
                else dictionary.Add(word, 1);
            }

            return dictionary;
        }

        private IFileHandler RecognizeFileType(string pathToFile)
        {
            switch (pathToFile)
            {
                case var _ when DocFileHandler.Regex.IsMatch(pathToFile):
                    return new DocFileHandler(pathToFile);
                case var _ when TxtFileHandler.Regex.IsMatch(pathToFile):
                   return new TxtFileHandler(pathToFile);
                default: 
                    return new DefaultFileHandler(pathToFile);
            }
        }

        private IEnumerable<string> FilterWords(IEnumerable<string> words)
        {
            return words
                .Where(word=> hunspell.Spell(word))              
                .Select(word => LeadToInitialForm(word).ToLower())
                .Where(word => word.Length != 0 && !WordsSettings.BoringWords.Contains(word));
        }


        //Substing(4) cuts meta information from hunspell
        private string LeadToInitialForm(string word)
        {
            var initialForm = hunspell.Analyze(word).FirstOrDefault();
            return initialForm?.Substring(4).Split().FirstOrDefault() ?? "";
        }
    }
}