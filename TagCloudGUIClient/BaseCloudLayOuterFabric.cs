using System.Drawing;
using CloudLayouters;

namespace TagCloudGUIClient
{
    public interface IBaseCloudLayouterFabric
    {
        string Name { get; }
        BaseCloudLayouter Create(Point center);
    }
}