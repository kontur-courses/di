using System.Collections.Generic;
using System.Linq;
using YandexMystem.Wrapper;
using YandexMystem.Wrapper.Enums;

namespace TagsCloudContainer.WordsPreprocessors
{
    public class BoringWordsRemover : IWordsPreprocessor
    {
        private readonly HashSet<GramPartsEnum> excludedGrammars = new HashSet<GramPartsEnum>
        {
            GramPartsEnum.PronounAdjective,
            GramPartsEnum.PronominalAdverb,
            GramPartsEnum.NounPronoun,
            GramPartsEnum.Pretext,
            GramPartsEnum.Part,
        };

        public IEnumerable<string> Preprocess(IEnumerable<string> words)
        {
            var mystem = new Mysteam();

            foreach (var wordModel in mystem.GetWords(string.Join(" ", words)))
            {
                if (!wordModel.Lexems.Any(z => excludedGrammars.Contains(z.GramPart)))
                {
                    yield return wordModel.SourceWord.Text;
                }
            }
        }
    }
}