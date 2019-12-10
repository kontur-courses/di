using System.Collections.Generic;

namespace TagsCloudVisualization.Services
{
    public interface IBoringWordsProvider
    {
        HashSet<string> BoringWords { get; set; }
    }
}