using System.Drawing;
using TagsCloud.ColorGenerators;
using TagsCloud.Distributors;
using TagsCloud.Entities;

namespace TagsCloud.Layouters;

public class CircularCloudLayouter:ILayouter
{
    public List<Tag> Tags { get; set; }
    private IDistributor distributor;
    private IColorGenerator colorGenerator;
    private Point center;

    public CircularCloudLayouter(IDistributor distributor,IColorGenerator colorGenerator, Point center)
    {
        this.distributor = distributor;
        this.center = center;
        this.colorGenerator = colorGenerator;
        
        Tags = new();
    }
    
    public void AddTag(Tag tag)
    {
        
    }
}