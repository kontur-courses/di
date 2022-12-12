using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Algorithm
{
    public interface IWordsCounter
    {
        public Dictionary<string, int> CountWords(string pathToSource, string pathToCustomBoringWords);
    }
}
