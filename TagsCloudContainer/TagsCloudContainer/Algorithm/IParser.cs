using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Algorithm
{
    public interface IParser
    {
        public Dictionary<string, int> GetWordsCountWithoutBoring();
        public Dictionary<string, int> CountWordsInSourceFile();
    }
}
