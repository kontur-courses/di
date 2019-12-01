using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHunspell;

namespace TagsCloudContainer.WordPreprocessors
{
    class SimpleWordPreprocessor : IWordPreprocessor
    {
        public string WordPreprocessing(string word)
        {
            return word.ToLower();
        }
    }
}
