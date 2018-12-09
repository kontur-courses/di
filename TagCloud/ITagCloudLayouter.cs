using System.Collections.Generic;

namespace TagsCloud
{
    public interface ITagCloudLayouter
    {
        IReadOnlyCollection<Tag> GetLayout(ICollection<KeyValuePair<string, double>> words);
    }
}