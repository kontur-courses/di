using System.ComponentModel;
using CircularCloudLayouter.WeightedLayouter.Forming;
using CircularCloudLayouter.WeightedLayouter.Forming.Standard;
using TagCloudApp.Infrastructure;

namespace TagCloudApp.Domain;

public class TagCloudLayouterSettings
{
    [TypeConverter(typeof(FormFactorConverter))]
    public FormFactor FormFactor { get; set; } = new RectangleFormFactor();

    private double _widthToHeightRatio = 1;

    public double WidthToHeightRatio
    {
        get => _widthToHeightRatio;
        set
        {
            if (value <= 0)
                throw new ArgumentException($"{nameof(WidthToHeightRatio)} should be positive!");
            _widthToHeightRatio = value;
        }
    }
}