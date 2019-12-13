using System.Collections.Generic;
using TagsCloudVisualization.Settings;
using TagsCloudVisualization.WordSource.Interfaces;

namespace TagsCloudVisualization.WordSource.Changers
{
    public class WordChangerFactory : IChangerFactory<string>
    {
        public IEnumerable<IChanger<string>> GetChangers(ReaderSettings settings)
        {
            yield return new LowerCaseChanger();
        }
    }
}