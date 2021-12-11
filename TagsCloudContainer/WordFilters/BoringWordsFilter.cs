using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.WordFilters
{
    public class BoringWordsFilter : IWordsFilter
    {
        private HashSet<string> boringWords;
        private readonly string boringWordsPath;

        public BoringWordsFilter(IAppSettings appSettings)
        {
            boringWords = new HashSet<string>();
            boringWordsPath = appSettings.BoringWordsPath;
        }

        public IEnumerable<string> Filter(IEnumerable<string> words)
        {
            if (!boringWords.Any())
            {
                boringWords = File.ReadAllLines(boringWordsPath).ToHashSet();
            }
            
            return words.Where(word => !boringWords.Contains(word));
        }
    }
}