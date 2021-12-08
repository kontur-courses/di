using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer
{
    public interface IWordsContainer
    {
        Dictionary<string, int> GetWords();
    }
}
