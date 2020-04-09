namespace FractalPainting.Infrastructure.UiActions
{
    public interface IUiAction
    {
        string Category { get; }
        int Order { get; }
        int CategoryOrder { get; }
        string Name { get; }
        string Description { get; }
        void Perform();
    }
}