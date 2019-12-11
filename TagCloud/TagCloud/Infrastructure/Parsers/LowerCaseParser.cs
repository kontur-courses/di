﻿using System.Linq;

namespace TagCloud
{
    public class LowerCaseParser : IParser
    {
        public string[] ParseWords(string[] words) => words
            .Select(word => word.ToLower())
            .ToArray();
    }
}
