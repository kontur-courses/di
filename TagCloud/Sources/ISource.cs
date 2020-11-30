using System;
using System.Collections.Generic;
using System.Text;

namespace TagCloud.Sources
{
    public interface ISource
    {
        IEnumerable<string> Words();
    }
}
