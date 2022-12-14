using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface IInputTextProvider
    {
        IWordFilter WordsFilter { get; set; }
        Dictionary<string, int> GetWords(string text);
    }
}