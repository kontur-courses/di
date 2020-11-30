using System;
using System.Collections.Generic;

namespace TagsCloudVisualisation.Text
{
    public interface IWordsReader : IDisposable
    {
        IEnumerable<string> EnumerateWords();
    }
}