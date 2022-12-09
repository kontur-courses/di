using CircularCloudLayouter;
using CircularCloudLayouter.Domain;
using CircularCloudLayouter.WeightedLayouter;
using TagCloudCreator.Domain.Settings;
using TagCloudCreator.Interfaces.Providers;

namespace TagCloudCreator.Domain.Providers;

public class WeightedTagCloudLayouterProvider : ITagCloudLayouterProvider
{
    private readonly IImageSettingsProvider _imageSettingsProvider;
    private readonly WeightedTagCloudLayouterSettings _layouterSettings;

    public WeightedTagCloudLayouterProvider(
        IImageSettingsProvider imageSettingsProvider,
        WeightedTagCloudLayouterSettings layouterSettings
    )
    {
        _imageSettingsProvider = imageSettingsProvider;
        _layouterSettings = layouterSettings;
    }

    public ITagCloudLayouter CreateLayouter()
    {
        var imageSettings = _imageSettingsProvider.GetImageSettings();

        var formFactor = _layouterSettings.FormFactor.WithRatio(_layouterSettings.WidthToHeightRatio);
        var center = new ImmutablePoint(imageSettings.Width / 2, imageSettings.Height / 2);
        return new WeightedTagCloudLayouter(center, formFactor);
    }
}