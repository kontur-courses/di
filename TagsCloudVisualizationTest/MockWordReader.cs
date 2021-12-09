using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.WordReaders;

namespace TagsCloudVisualizationTest
{
    public class MockWordReader : IWordReader
    {
        private readonly List<string> words;
        private int pointer;
        
        public MockWordReader(params string[] words)
        {
            this.words = words.ToList();
        }
        
        public string Read()
        {
            return words[pointer++];
        }

        public bool HasWord()
        {
            return pointer < words.Count();
        }
    }
}