using System.Linq;
using TagsCloudVisualization.WordProcessors.WordProcessingSettings;

namespace TagsCloudVisualization.WordProcessors
{
    public class WordProcessor : IWordProcessor 
    {
        public IProcessingSettings Settings { get; }

        public bool WordIsAllowed(string word)
        {
            return word.Length >= Settings.MinWordLength &&
                   word.Length <= Settings.MaxWordLength &&
                   !Settings.ExcludedWords.Contains(word);
        }

        public string[] Process(string[] words)
        {
            return words.Where(WordIsAllowed)
                 .Select(word => word.ToLower()).ToArray();
        }

        public WordProcessor(IProcessingSettings settings)
        {
            Settings = settings;
        }
    }
}
