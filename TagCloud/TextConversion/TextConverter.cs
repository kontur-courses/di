using System.Collections.Generic;
using System.Linq;

namespace TagCloud.TextConversion
{
    public class TextConverter
    {
        private readonly ITextConversion[] textConversions;
        private readonly TextFiltration.TextFilter textFilter;

        public TextConverter(ITextConversion[] textConversions, TextFiltration.TextFilter textFilter)
        {
            this.textConversions = textConversions;
            this.textFilter = textFilter;
        }

        public List<string> ConvertWords()
        {
            return textFilter.FilterWords().Select(ConvertOneWord).ToList();
        }

        private string ConvertOneWord(string word)
        {
            return textConversions.Aggregate(word, (current, conversion)
                => conversion.ConvertWord(current));
        }
    }
}