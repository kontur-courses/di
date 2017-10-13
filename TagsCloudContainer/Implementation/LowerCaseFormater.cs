using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer
{
    class LowerCaseFormater : IWordFormater
    {
        public LowerCaseFormater() { }

        public IEnumerable<string> HandleWords(IEnumerable<string> words)
        {
            return words.Select(w => w.ToLower()).ToArray();
        }
    }
}
