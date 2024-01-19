using System.CodeDom.Compiler;
using TagCloud.TextHandlers;
//TODO: перенести в TagCloud
using TagCloudTests;

namespace TagCloud;

public class TagCloudGenerator
{
    private readonly ITextHandler handler;
    private readonly CircularCloudLayouter layouter;
    private readonly ICloudDrawer drawer;

    public TagCloudGenerator(ITextHandler handler, CircularCloudLayouter layouter, ICloudDrawer drawer)
    {
        this.handler = handler;
        this.layouter = layouter;
        this.drawer = drawer;
    }

    public void Generate()
    {
        throw new NotImplementedException();
    }
}