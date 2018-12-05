using System.Collections.Generic;

namespace TagCloud.Interfaces
{
    public interface IWordProcessor
    {
        IEnumerable<string> Preprocess (IEnumerable<string> words);
    }
}