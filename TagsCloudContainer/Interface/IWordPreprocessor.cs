using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer
{
    public interface IWordPreprocessor
    {
        IEnumerable<string> Handle(IEnumerable<string> words);
    }
}
