using System;
using System.Windows.Forms;

namespace TagsCloudVisualization.Actions
{
    public interface IUiAction
    {
        string Name { get; }
        void Perform(object sender, EventArgs e);
    }
}