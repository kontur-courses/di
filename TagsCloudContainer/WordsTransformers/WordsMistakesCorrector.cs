using TagsCloudContainer.Dictionaries;

namespace TagsCloudContainer.WordsTransformers
{
    public class WordsMistakesCorrector : IWordsTransformer
    {
        private readonly IGrammarDictionary grammarDictionary;

        public WordsMistakesCorrector(IGrammarDictionary grammarDictionary)
        {
            this.grammarDictionary = grammarDictionary;
        }

        public string TransformWord(string word)
        {
            string fixedWord;
            return grammarDictionary.TryGetCorrectForm(word, out fixedWord)
                ? fixedWord
                : word;
        }
    }
}