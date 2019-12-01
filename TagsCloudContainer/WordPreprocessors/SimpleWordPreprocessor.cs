using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.WordPreprocessors
{
    class SimpleWordPreprocessor : IWordPreprocessor
    {
        public List<string> WordPreprocessing(string[] words)
        {
            return words.Select(word => word.ToLower()).ToList();
        }
    }
}
