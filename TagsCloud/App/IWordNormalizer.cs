using System;
using System.Collections.Generic;
using System.Text;

namespace TagsCloud.App
{
    public interface IWordNormalizer
    {
        string NormalizeWord(string word);
    }
}
