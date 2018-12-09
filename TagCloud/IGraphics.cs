using System.Collections.Generic;

namespace TagsCloud
{
    public interface IGraphics
    {
        void Save(IReadOnlyCollection<Tag> words);
    }
}