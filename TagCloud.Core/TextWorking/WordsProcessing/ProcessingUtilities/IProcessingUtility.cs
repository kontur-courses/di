using System.Collections.Generic;

namespace TagCloud.Core.TextWorking.WordsProcessing.ProcessingUtilities
{
    public interface IProcessingUtility
    {
        IEnumerable<string> Handle(IEnumerable<string> words);
    }
}