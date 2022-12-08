using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization.TextFormatters
{
    public class SmallWordFilter : IWordFilter
    {
        public bool IsPermitted(string word)
        {
            return word.Length > 3;
        }
    }
}
