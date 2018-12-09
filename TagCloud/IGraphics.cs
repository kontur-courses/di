using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud
{
    public interface IGraphics
    {
        void Save(IReadOnlyCollection<Tag> words);
    }
}