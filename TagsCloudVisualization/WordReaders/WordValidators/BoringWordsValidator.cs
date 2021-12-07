using System.Collections.Generic;

namespace TagsCloudVisualization.WordReaders.WordValidators
{
    public class BoringWordsValidator : IWordValidator
    {
        private readonly HashSet<string> boringWords = new();
        private readonly IWordReader boringWordsReader;

        public BoringWordsValidator(IWordReader boringWords)
        {
            boringWordsReader = boringWords;
            Load();
        }

        private void Load()
        {
            while (boringWordsReader.HasWord())
            {
                boringWords.Add(boringWordsReader.Read());
            }
        }

        public bool Validate(string word)
        {
            return !boringWords.Contains(word);
        }
    }
}