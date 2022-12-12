namespace TagsCloudVisualization.InfrastructureUI
{
    public interface IUiAction
    {
        Category Category { get; }
        string Name { get; }
        string Description { get; }
        void Perform();
    }
}