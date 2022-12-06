using CircularCloudLayouter;
using CircularCloudLayouter.WeightedLayouter;
using TagCloudApp.Abstractions;
using TagCloudApp.Domain;

namespace TagCloudApp.Implementations;

public class WeightedTagCloudLayouterCreator : ITagCloudLayouterCreator
{
    private readonly ImageSettings _imageSettings;
    private readonly TagCloudLayouterSettings _layouterSettings;

    public WeightedTagCloudLayouterCreator(ImageSettings imageSettings, TagCloudLayouterSettings layouterSettings)
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