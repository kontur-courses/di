using System.Drawing;

namespace TagCloud.Creators
{
    public interface ITagCreatorFactory
    {
        ITagCreator Create(Font font);
    }
}
