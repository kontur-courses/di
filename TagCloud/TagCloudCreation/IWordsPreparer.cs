using System.Collections.Generic;

namespace TagCloudCreation
{
    public interface IWordsPreparer
    {
        List<WordInfo> PrepareWords(List<WordInfo> stats);
    }
}
