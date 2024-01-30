using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.Extensions.DependencyInjection;

namespace TagsCloudVisualization;

public class TagLayoutSettings
{
    public TagLayoutSettings(string algorithm)
    {
        Algorithm = algorithm;
    }

    public string Algorithm { get; }
}