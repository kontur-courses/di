using System;
using System.Collections.Generic;
using System.Linq;
using TagCloud.Interfaces;

namespace TagCloud.Words
{
    public class WordsRepository : IRepository
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