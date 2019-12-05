namespace TagsCloudVisualization.VisualizerActions
{
    public interface IVisualizerAction
    {
        string GetActionDescription();

        string GetActionName();

        void Perform();
    }
}