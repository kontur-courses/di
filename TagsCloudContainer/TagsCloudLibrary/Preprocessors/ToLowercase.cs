using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudLibrary.Preprocessors
{
    public class ToLowercase : IPreprocessor
    {
        public int Priority { get; } = 10;
        public IEnumerable<string> Act(IEnumerable<string> words)
        {
            return words.Select(word => word.ToLower());
        }
    }
}
