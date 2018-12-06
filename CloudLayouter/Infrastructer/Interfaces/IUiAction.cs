using CloudLayouter.Infrastructer.Common;

namespace CloudLayouter.Infrastructer.Interfaces
{
    public interface IUiAction
    {
        string Name { get; }
        string Description { get; }
        MenuCategory Category { get; }
        void Perform();
    }
}