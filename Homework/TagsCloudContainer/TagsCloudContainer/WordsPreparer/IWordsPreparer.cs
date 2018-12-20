using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudBuilder
{
    public interface IWordsPreparer
    {
        Dictionary<string, int> GetPreparedWords();
    }
}
