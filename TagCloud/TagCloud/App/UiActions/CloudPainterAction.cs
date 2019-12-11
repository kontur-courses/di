namespace TagCloud
{
    public class CloudPainter : IUiAction
    {
        private readonly IVisualizer visualizer;

        public CloudPainter(IVisualizer visualizer)
        {
            this.visualizer = visualizer;
        }

        public MenuCategory Category => MenuCategory.CloudPainter;

        public string Name => "CloudPainter";

        public string Description => "Paints the tag cloud";

        public void Perform()
        {
            visualizer.Visualize();
        }
    }
}
