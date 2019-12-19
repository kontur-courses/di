using System.Collections.Generic;
using TagsCloudVisualization.Settings;
using TagsCloudVisualization.WordSource.Interfaces;

namespace TagsCloudVisualization.WordSource.Changers
{
    public class WordChangerFactory : IChangerFactory
    {
        public IEnumerable<IWordChanger> GetChangers(ReaderSettings settings)
        {
            yield return new LowerCaseWordChanger();
        }
    }
}