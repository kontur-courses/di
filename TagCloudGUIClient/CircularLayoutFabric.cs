using System.Drawing;
using CloudLayouters;

namespace TagCloudGUIClient
{
    public class CircularLayoutFabric : IBaseCloudLayouterFabric
    {
        public string Name => "Круглое облако";

        public BaseCloudLayouter Create(Point center)
        {
            return new CircularCloudLayouter(center);
        }
    }
}