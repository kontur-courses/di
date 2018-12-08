using System.Collections.Generic;
using CloodLayouter.Infrastructer;

namespace CloodLayouter.App
{
    public class WordProvider : IWordProvider
    {
        public List<string> Words { get; set; } = new List<string>();
    }
}