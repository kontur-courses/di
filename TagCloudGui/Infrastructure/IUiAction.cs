namespace TagCloudGui.Infrastructure
{
    public interface IUiAction
    {
        MenuCategory Category { get; }
        string Name { get; }
        string Description { get; }
        void Perform();
    }
}
