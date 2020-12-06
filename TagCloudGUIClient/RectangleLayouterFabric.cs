using System.Drawing;
using CloudLayouters;

namespace TagCloudGUIClient
{
    public class RectangleLayouterFabric : IBaseCloudLayouterFabric
    {
        public string Name => "Квадратное облако";


        public BaseCloudLayouter Create(Point center)
        {
            return new RectangleLayouter(center);
        }
    }
}