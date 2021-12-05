using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    public class Word
    {
        public string WordText { get; set; }

        public Word(string word)
        {
            if (word.Split(' ').Length != 1)
                throw new ArgumentException();
            WordText = word;
        }

        public Word ToLower()
        {
            return new Word(WordText.ToLower());
        }
    }
}
