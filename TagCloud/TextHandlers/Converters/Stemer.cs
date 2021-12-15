using System.Linq;
using NHunspell;

namespace TagCloud.TextHandlers.Converters
{
    public class Stemer : IConverter
    {
        private readonly Hunspell hunspell = new("../../../ru_ru.aff", "../../../ru_ru.dic");

        public string Convert(string original)
        {
            return hunspell.Stem(original).FirstOrDefault() ?? original;
        }
    }
}