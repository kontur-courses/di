using System.Collections.Generic;

namespace TagsCloudVisualization.Interfaces
{
    public interface IWordPreparator
    {
        public IEnumerable<string> GetPreparedWords(IEnumerable<string> unpreparedWords);
    }
}