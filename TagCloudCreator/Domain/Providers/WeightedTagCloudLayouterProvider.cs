using System.Drawing;
using CircularCloudLayouter;
using CircularCloudLayouter.WeightedLayouter;
using TagCloudCreator.Domain.Settings;
using TagCloudCreator.Interfaces.Providers;

namespace TagCloudCreator.Domain.Providers;

public class WeightedTagCloudLayouterProvider : ITagCloudLayouterProvider
{
    private readonly ImageSettings _imageSettings;
    private readonly WeightedTagCloudLayouterSettings _layouterSettings;

    public WeightedTagCloudLayouterProvider(ImageSettings imageSettings, WeightedTagCloudLayouterSettings layouterSettings)
    {
        _imageSettings = imageSettings;
        _layouterSettings = layouterSettings;
    }

    public ITagCloudLayouter CreateLayouter()
    {
        var formFactor = _layouterSettings.FormFactor.WithRatio(_layouterSettings.WidthToHeightRatio);
        var center = new Point(_imageSettings.Width / 2, _imageSettings.Height / 2);
        return new WeightedTagCloudLayouter(center, formFactor);
    }
}