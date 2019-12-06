using System.Collections.Generic;

namespace TagsCloudContainer.RectangleTranslation
{
    public interface ISizeTranslator
    {
        IEnumerable<SizedWord> TranslateWordsToSizedWords(Dictionary<string, int> countedWords);
    }
}