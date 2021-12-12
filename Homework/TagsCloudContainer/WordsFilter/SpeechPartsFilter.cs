﻿using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.WordsConverters;

namespace TagsCloudContainer.WordsFilter
{
    public class SpeechPartsFilter : IWordsFilter
    {
        private readonly SpeechPart[] selectedSpeechParts;

        internal SpeechPartsFilter(params SpeechPart[] selectedSpeechParts)
        {
            this.selectedSpeechParts = selectedSpeechParts;
        }

        public SpeechPartsFilter(ITagCloudSettings settings)
        {
            selectedSpeechParts = settings.SelectedSpeechParts.ToArray();
        }

        public ICollection<WordInfo> Filter(ICollection<WordInfo> words)
        {
            if (selectedSpeechParts.Length == 0)
                return words;
            return words
                .Where(word => selectedSpeechParts.Contains(word.SpeechPart))
                .ToArray();
        }
    }
}