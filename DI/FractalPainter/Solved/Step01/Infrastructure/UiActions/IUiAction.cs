namespace FractalPainting.Solved.Step01.Infrastructure.UiActions
{
    public interface IUiAction
    {
        string Category { get; }
        string Name { get; }
        string Description { get; }
        void Perform();
    }
}