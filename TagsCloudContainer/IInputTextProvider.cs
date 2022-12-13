using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface IInputTextProvider
    {
        Dictionary<string, int> GetWords();
    }
}