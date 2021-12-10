using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud2
{
    public interface IStringPreprocessor
    {
        string PreprocessString(string input);
    }
}
