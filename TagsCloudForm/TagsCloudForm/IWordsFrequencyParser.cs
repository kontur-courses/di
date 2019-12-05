using System.Collections.Generic;
using TagsCloudForm.CircularCloudLayouter;

namespace TagsCloudForm
{
    public interface IWordsFrequencyParser
    {
        Dictionary<string, int> GetWordsFrequency(IEnumerable<string> lines, LanguageEnum language);
    }
}
