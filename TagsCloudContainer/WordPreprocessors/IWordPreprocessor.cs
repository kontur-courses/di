using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.WordPreprocessors
{
    interface IWordPreprocessor
    {
        List<string> WordPreprocessing(string[] words);
    }
}
