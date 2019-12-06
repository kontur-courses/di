using System.Collections.Specialized;

namespace TagsCloudVisualization.Actions
{
    public interface IUiAction
    {
        string Name { get; }
        void Perform();
    }
}