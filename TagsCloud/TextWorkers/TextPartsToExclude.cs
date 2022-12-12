using System;
using System.Collections.Generic;
using System.Text;
using TagsCloud.Interfaces;

namespace TagsCloud.TextWorkers
{
    public class TextPartsToExclude : ITextPartsToExclude
    {
        public string[] SpeechPartsToExclude { get; } = { "мест", "межд", "част", "предл", "союз" };

        private readonly string[] wordsToExclude;

        public string[] WordsToExclude => wordsToExclude;
    }
}
