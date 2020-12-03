using System;
using System.Collections.Generic;
using System.Text;

namespace TagsCloud.Infrastructure
{
    public class Tag
    {
        public string Word { get; set; }
        public int Weight { get; set; }

        public Tag() {}

        public Tag(string word, int weight)
        {
            Word = word;
            Weight = weight;
        }
    }
}
