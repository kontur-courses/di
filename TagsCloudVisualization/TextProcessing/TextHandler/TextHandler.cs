﻿using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.Words;

namespace TagsCloudVisualization.TextProcessing.TextHandler
{
    public class TextHandler
    {
        public static IEnumerable<Word> GetOrderedByFrequencyWords(string text, HashSet<string> forbiddenWords = null)
        {
            var words = text.Split(new[]
                {
                    ' ', ',', '.', ';', ':',
                    '\n', '\t', '\r',
                    '[', ']',
                    '<', '>',
                    '{', '}',
                    '"', '\'',
                    '(', ')',
                    '+', '-', '=', '/', '*', '\\',
                    '!', '?', '#', '$', '|', '_', '&', '^', '%',
                    '1', '2', '3', '4', '5', '6', '7', '8', '9', '0'
                }, StringSplitOptions.RemoveEmptyEntries)
                .Where(word => word.Length > 3)
                .Select(word => word.ToLower());

            return GetWordsFrequency(forbiddenWords == null
                ? words
                : words.Where(word => !forbiddenWords.Contains(word)));
        }

        private static IEnumerable<Word> GetWordsFrequency(IEnumerable<string> words)
        {
            var wordsFrequency = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (wordsFrequency.TryGetValue(word, out var frequency))
                    wordsFrequency[word] = frequency + 1;
                else wordsFrequency[word] = 1;
            }

            return wordsFrequency
                .OrderByDescending(pair => pair.Value)
                .Select(pair => new Word(pair.Key, pair.Value));
        }
    }
}