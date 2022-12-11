namespace TagCloudGraphicalUserInterface.Interfaces
{
    public interface IActionForm
    {
        string Category { get; }
        string Name { get; }
        string Description { get; }
        void Perform();
    }
}
