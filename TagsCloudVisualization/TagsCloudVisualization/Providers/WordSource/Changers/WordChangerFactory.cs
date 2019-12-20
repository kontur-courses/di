using System.Collections.Generic;
using TagsCloudVisualization.Providers.WordSource.Interfaces;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Providers.WordSource.Changers
{
    public class WordChangerFactory : IChangerFactory
    {
        public IEnumerable<IWordChanger> GetChangers(ReaderSettings settings)
        {
            yield return new LowerCaseWordChanger();
        }
    }
}