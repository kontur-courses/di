﻿namespace TagsCloudVisualizationDI.TextAnalization.Normalizer
{
    public class WordNormalizerOrigin : IWordNormalizer
    {
        public Word NormalizeWord(Word word)
        {
            word.WordText = word.WordText.ToLower();
            return word;
        }
    }
}
