using TagsCloudContainer.Drawing.Colorers;
using TagsCloudContainer.WordProcessing.WordInput;
using TagsCloudVisualization;

namespace TagsCloudContainer.BuildingOptions;

public class CommonOptions
{
    public IWordProvider WordProvider { get; }
    
    public IWordColorer? WordColorer { get; }
    
    public ICloudLayouter CloudLayouter { get; }

    public CommonOptions(IWordProvider wordProvider, IWordColorer? wordColorer, ICloudLayouter cloudLayouter)
    {
        WordProvider = wordProvider;
        WordColorer = wordColorer;
        CloudLayouter = cloudLayouter;
    }
}