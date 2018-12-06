using System;
using System.Collections.Generic;

namespace CloodLayouter.Infrastructer
{
    public interface IWordSelector
    {
        IEnumerable<string> Select(IEnumerable<String> words);
    }
}