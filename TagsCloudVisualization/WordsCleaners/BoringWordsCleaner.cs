﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using NHunspell;

namespace TagsCloudVisualization.WordsCleaners
{
    public class BoringWordsCleaner : IWordsCleaner
    {
        private readonly HashSet<string> boringWords;

        public BoringWordsCleaner(HashSet<string> boringWords)
        {
            this.boringWords = boringWords;
        }

        public List<string> CleanWords(List<string> words)
        {
            var ruDict = Path.Join(Directory.GetCurrentDirectory(), "ru_RU.dic");
            var ruAff = Path.Join(Directory.GetCurrentDirectory(), "ru_RU.aff");
            var hunspell = new Hunspell(ruAff, ruDict);
            return words
                .Where(loweredWord => !boringWords.Contains(hunspell.Stem(loweredWord.ToLower()).FirstOrDefault()))
                .ToList();
        }
    }
}