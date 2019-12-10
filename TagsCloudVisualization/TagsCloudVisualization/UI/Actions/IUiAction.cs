using System;

namespace TagsCloudVisualization.UI.Actions
{
    public interface IUiAction
    {
        string Name { get; }
        void Perform(object sender, EventArgs e);
    }
}