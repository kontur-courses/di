using TagsCloud.Interfaces;
using System.Linq;
using System.Collections.Generic;
using TagsCloud.FileReader;

namespace TagsCloud.WordProcessing
{
    public class DefaultWordValidator : IWordValidator
    {
        private readonly HashSet<string> boringWords;

        public DefaultWordValidator(SpliterByLine textSpliter, ITextReader fileReader, WordValidatorSettings wordValidatorSettings)
        {
            boringWords = textSpliter.SplitText(fileReader.ReadFile(wordValidatorSettings.pathToBoringWords)).ToHashSet();
        }

        public bool IsValidWord(string word)
        {
            return word.Length != 0 
                && boringWords.FirstOrDefault(boringWord => boringWord.Equals(word, System.StringComparison.InvariantCultureIgnoreCase)) == null
                && !int.TryParse(word, out var res);
        }
    }
}