using System.Drawing;

namespace TagsCloudContainer.CloudLayouters
{
    public static class CloudLayoutingAlgorithms
    {
        public static ICloudLayoutingAlgorithm TryGetLayoutingAlgorithm(string name, double step, int broadness)
        {
            var lower = name.ToLower();
            switch (lower)
            {
                default:
                    return null;
                case "circular":
                case "circle":
                    return new CircularCloudLayouter.CircularCloudLayouter(new Point(0, 0), step, broadness);
            }
        }
    }
}