using System.Collections.Generic;
using TagCloud.Infrastructure;

namespace TagCloud.WordsProcessing
{
    public class WordClassSettings
    {
        public HashSet<WordClass> WordClasses { get; }
        public bool IsBlackList { get; }

        private static readonly HashSet<WordClass> DefaultWordClasses = new HashSet<WordClass>
            {WordClass.Conjunction, WordClass.Preposition, WordClass.Particle, WordClass.Pronoun};

        public WordClassSettings() : this(DefaultWordClasses, true)
        {
        }

        public WordClassSettings(HashSet<WordClass> wordClasses, bool isBlackList)
        {
            WordClasses = wordClasses;
            IsBlackList = isBlackList;
        }

        
    }
}
