using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.Filtering
{
    public class BlacklistWordsFilter : IWordsFilter
    {
        private IBoringWordsRepository BoringWordsRepository { get; }


        public BlacklistWordsFilter(IBoringWordsRepository boringWordsRepository)
        {
            BoringWordsRepository = boringWordsRepository;
        }


        public List<string> Filter(IEnumerable<string> words)
        {
            return words.Where(x => !BoringWordsRepository.Words.Contains(x)).ToList();
        }
    }
}