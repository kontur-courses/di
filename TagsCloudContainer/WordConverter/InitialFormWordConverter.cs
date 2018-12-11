using NHunspell;

namespace TagsCloudContainer.WordConverter
{
    public class InitialFormWordConverter : IWordConverter
    {
        public string Convert(string word)
        {
            using (var hunspell = new Hunspell("../../ru_RU.aff", "../../ru_RU.dic"))
            {
                var stems = hunspell.Stem(word);
                return stems.Count > 0 ? stems[0] : word;
            }
        }
    }
}