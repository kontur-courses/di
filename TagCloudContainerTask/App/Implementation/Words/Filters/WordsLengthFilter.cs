using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Words.Filters;

namespace App.Implementation.Words.Filters
{
    public class WordsLengthFilter : IFilter
    {
        private readonly int minLength;

        public WordsLengthFilter(int minLength = 3)
        {
            if (minLength < 1)
                throw new ArgumentException("Word length can not be lesser then 1");

            this.minLength = minLength;
        }

        public IEnumerable<string> FilterWords(IEnumerable<string> words)
        {
            return words.Where(word => word.Length >= minLength);
        }
    }
}