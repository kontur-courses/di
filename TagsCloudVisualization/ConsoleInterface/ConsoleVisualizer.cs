using TagsCloudVisualization.VisualizerActions;

namespace TagsCloudVisualization.ConsoleInterface
{
    public class ConsoleVisualizer
    {
        private IVisualizerAction[] actions;

        public ConsoleVisualizer(IVisualizerAction[] actions)
        {
            this.actions = actions;
        }
    }
}