using System.Drawing;

namespace TagsCloudContainer.Core.ColoringAlgorithms
{
    interface IColoringAlgorithm
    {
        Color GetNextColor();
    }
}