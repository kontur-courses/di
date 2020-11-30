using System;
using System.Collections.Generic;
using System.Text;

namespace TagsCloud.App
{
    public interface IWordsConverter
    {
        IEnumerable<string> ConvertWords(IEnumerable<string> words);
        string ConvertWord(string word);
    }
}
