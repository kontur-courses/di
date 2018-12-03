using System.Collections.Generic;
using System.Linq;
using TagCloud.Interfaces;

namespace TagCloud.Words
{
    public class WordFilter : IWordFilter
    {
        private readonly IExcludingRepository excludingWordsRepository;
        
        public WordFilter(IExcludingRepository excludingWordsRepository)
        {
            this.excludingWordsRepository = excludingWordsRepository;
        }
        
        public IEnumerable<string> Filter(IEnumerable<string> words)
        {
            return words.Where(w => !excludingWordsRepository.Contains(w));
        }
    }
} 