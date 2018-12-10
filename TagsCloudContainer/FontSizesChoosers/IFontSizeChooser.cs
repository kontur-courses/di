using System.Collections.Generic;
using TagsCloudContainer.WordsHandlers;

namespace TagsCloudContainer.FontSizesChoosers
{
    public interface IFontSizeChooser
    {
        IEnumerable<PrintedWordInfo> GetWordInfos(IEnumerable<WordInfo> words);
    }
}