using CloudLayouter.Infrastructer.Common;

namespace CloudLayouter.Infrastructer
{
    public interface IUiAction
    {
        string Name { get; }
        string Description { get; }
        void Perform();
        MenuCategory Category { get; }
    }
}