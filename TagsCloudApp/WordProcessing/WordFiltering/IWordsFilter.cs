using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudApp.WordFiltering
{
    public interface IWordFilter
    {
        IEnumerable<string> FilterWords(IEnumerable<string> words);
    }
}
