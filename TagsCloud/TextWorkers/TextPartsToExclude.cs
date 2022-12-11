using System;
using System.Collections.Generic;
using System.Text;
using TagsCloud.Interfaces;

namespace TagsCloud.TextWorkers
{
    public class TextPartsToExclude : ITextPartsToExclude
    {
        private readonly string[] speechPartsToExclude =
        {
            "мест",
            "межд",
            "част",
            "предл",
            "союз"
        };

        public string[] SpeechPartsToExclude => speechPartsToExclude;

        private readonly string[] wordsToExclude;

        public string[] WordsToExclude => wordsToExclude;
    }
}
