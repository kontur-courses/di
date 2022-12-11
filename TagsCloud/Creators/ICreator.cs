using System.Drawing;

namespace TagsCloud.Creators
{
    public interface ICreator<out T>
    {
        T Place(Point point, Size size);
    }
}