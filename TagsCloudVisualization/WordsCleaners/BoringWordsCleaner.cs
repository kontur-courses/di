using System.Collections.Generic;
using System.Linq;

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
            return words.Select(word => word.ToLower())
                .Where(loweredWord => !boringWords.Contains(loweredWord))
                .ToList();
        }
    }
}