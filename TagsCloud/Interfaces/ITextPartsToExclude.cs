using System;
using System.Collections.Generic;
using System.Text;

namespace TagsCloud.Interfaces
{
    public interface ITextPartsToExclude
    {
        public string[] SpeechPartsToExclude { get; }
        public string[] WordsToExclude { get; }
    }
}
