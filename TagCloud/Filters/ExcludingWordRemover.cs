using System.Collections.Generic;
using System.Linq;
using TagCloud.Interfaces;

namespace TagCloud.Filters
{
    public class ExcludingWordsRemover : IWordFilter
    {
        private readonly IExcludingRepository excludingWordsRepository;
        
        public ExcludingWordsRemover(IExcludingRepository excludingWordsRepository)
        {
            this.excludingWordsRepository = excludingWordsRepository;
        }
        
        public IEnumerable<string> Filter(IEnumerable<string> words)
        {
            return words.Where(w => !excludingWordsRepository.Contains(w));
        }
    }
} 