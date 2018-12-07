using System;
using System.IO;
using NHunspell;

namespace TagCloud
{
    public class SimpleWordParser : IWordParser
    {
        private readonly NHunspell.Hunspell hunspell;
        public SimpleWordParser()
        {
            try
            {
                hunspell = new Hunspell("en_US.aff", "en_US.dic");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Dictionary not find.");
            }
        }

        public bool IsValidWord(string word)
        {
            return hunspell.Spell(word);
        }
    }
}