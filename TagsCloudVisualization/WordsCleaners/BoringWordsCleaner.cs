using System.Collections.Generic;
using System.Linq;
using MyStemWrapper;

namespace TagsCloudVisualization
{
    public class BoringWordsCleaner : IWordsCleaner
    {
        private readonly HashSet<string> boringWords;
        
        public BoringWordsCleaner(HashSet<string> boringWords)
        {
            this.boringWords = boringWords;
        }
        
        public List<string> CleanWords(List<string> words)
        {
            /*var lemmatizer = new Lemmatizer();
            var normalizedWords = words.Select(x => lemmatizer.GetText(x)).ToArray();*/
            return words.Select(word => word.ToLower())
                .Where(loweredWord => !boringWords.Contains(loweredWord))
                .ToList();
        }
    }
}