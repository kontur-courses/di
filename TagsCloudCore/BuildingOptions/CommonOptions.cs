using TagsCloudCore.Drawing.Colorers;
using TagsCloudCore.WordProcessing.WordInput;
using TagsCloudVisualization;

namespace TagsCloudCore.BuildingOptions;

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