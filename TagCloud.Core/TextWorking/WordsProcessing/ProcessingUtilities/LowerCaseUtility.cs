using System;
using System.Collections.Generic;
using System.Linq;

namespace TagCloud.Core.TextWorking.WordsProcessing.ProcessingUtilities
{
    public class LowerCaseUtility : IProcessingUtility
    {
        public IEnumerable<string> Handle(IEnumerable<string> words)
        {
            return words.Select(word => word.ToLower());
        }
    }
}