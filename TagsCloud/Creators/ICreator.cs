using System.Drawing;

namespace TagsCloud.Creators
{
    internal interface ICreator<out T>
    {
        T Place(Point point, Size size);
    }
}