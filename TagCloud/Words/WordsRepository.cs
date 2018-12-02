using System.Collections.Generic;
using System.Linq;

namespace TagCloud.Words
{
    public class WordsRepository
    {
        private List<string> words = new List<string>();
        
        public void Load(IEnumerable<string> words)
        {
            this.words = words.ToList();
        }
        
        public IEnumerable<string> Get()
        {
            return words;
        }
    }
}