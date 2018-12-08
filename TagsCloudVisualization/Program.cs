using TagsCloudVisualization.Infrastructure;
using TagsCloudVisualization.Visualizer;

namespace TagsCloudVisualization
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = new DIBuilder();
            var visualize = builder.Resolve<IVisualizer>();
            visualize.Draw().Save("examples/text.png");
        }
    }
}
