#region

using System.Collections.Generic;

#endregion

namespace TagsCloudVisualization.Interfaces
{
    public interface IWordPreparator
    {
        IEnumerable<string> GetPreparedWords(IEnumerable<string> unpreparedWords);
    }
}