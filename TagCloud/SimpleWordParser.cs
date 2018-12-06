using NHunspell;

namespace TagCloud
{
    public class SimpleWordParser : IWordParser
    {
        private readonly NHunspell.Hunspell hunspell;
        public SimpleWordParser()
        {
            hunspell = new Hunspell("en_US.aff", "en_US.dic");
        }

        public bool IsValidWord(string word)
        {
            return hunspell.Spell(word);
        }
    }
}