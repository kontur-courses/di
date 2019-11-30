namespace TagsCloudVisualization.VisualizerActions
{
    public interface IVisualizerAction
    {
        string GetActionDescription();

        void Perform();
    }
}