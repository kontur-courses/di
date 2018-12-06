using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CloodLayouter.Infrastructer;

namespace CloodLayouter.App
{
    public class SimpleWordSelector : IWordSelector
    {
        public IEnumerable<string> Select(IEnumerable<string> words)
        {
            return words.Where(x => x.Length > 4);
        }
    }
}