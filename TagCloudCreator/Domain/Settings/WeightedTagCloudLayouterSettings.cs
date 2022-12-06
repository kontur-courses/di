using System.ComponentModel;
using CircularCloudLayouter.WeightedLayouter.Forming;
using CircularCloudLayouter.WeightedLayouter.Forming.Standard;
using TagCloudCreator.Infrastructure;

namespace TagCloudCreator.Domain.Settings;

public class WeightedTagCloudLayouterSettings
{
    [DisplayName("Form factor")]
    [Description("Result tag cloud form factor")]
    [TypeConverter(typeof(FormFactorConverter))]
    public FormFactor FormFactor { get; set; } = new RectangleFormFactor();

    private double _widthToHeightRatio = 1;

    [DisplayName("Width to height ratio")]
    [Description("Result tag cloud aspect ratio")]
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