using System.Collections.Generic;

namespace TagsCloudLibrary.Preprocessors
{
    public interface IPreprocessor
    {
        int Priority { get; }
        IEnumerable<string> Act(IEnumerable<string> words);
    }
}
