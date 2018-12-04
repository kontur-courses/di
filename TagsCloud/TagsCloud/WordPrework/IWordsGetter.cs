using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloud.WordPrework
{
    public interface IWordsGetter
    {
        IEnumerable<string> GetWords(params char[] delimiters);
    }
}
