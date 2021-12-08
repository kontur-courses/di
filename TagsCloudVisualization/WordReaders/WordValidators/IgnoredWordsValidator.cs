using System.Collections.Generic;
using Autofac.Features.AttributeFilters;

namespace TagsCloudVisualization.WordReaders.WordValidators
{
    public class IgnoredWordsValidator : IWordValidator
    {
        private readonly HashSet<string> boringWords = new();
        private readonly IWordReader boringWordsReader;

        public IgnoredWordsValidator([KeyFilter("IgnoreWords")]IWordReader boringWords)
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