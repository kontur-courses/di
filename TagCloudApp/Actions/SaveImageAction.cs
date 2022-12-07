using TagCloudApp.Domain;
using TagCloudApp.Infrastructure;
using TagCloudCreator.Interfaces;
using TagCloudCreator.Interfaces.Providers;
using TagCloudCreator.Interfaces.Settings;

namespace TagCloudApp.Actions;

public class SaveImageAction : IUiAction
{
    private readonly IImageHolder _imageHolder;
    private readonly IImagePathSettings _imagePathSettings;
    private readonly IImageSaverProvider _imageSaverProvider;

    public SaveImageAction(
        IImageHolder imageHolder,
        IImagePathSettings imagePathSettings,
        IImageSaverProvider imageSaverProvider
    )
    {
        _imageHolder = imageHolder;
        _imagePathSettings = imagePathSettings;
        _imageSaverProvider = imageSaverProvider;
    }

    public MenuCategory Category => MenuCategory.File;
    public string Name => "Save image...";
    public string Description => "Save image to file";

    private string? _filter;

    public void Perform()
    {
        _filter ??= string.Join("|", _imageSaverProvider.SupportedExtensions
            .Select(extension => $"{extension}|*{extension}")
        );
        var dialog = new SaveFileDialog
        {
            CheckFileExists = false,
            InitialDirectory = Path.GetFullPath(_imagePathSettings.ImagePath),
            FileName = "image",
            AddExtension = true,
            Filter = _filter
        };
        var res = dialog.ShowDialog();
        if (res is not DialogResult.OK)
            return;

        _imagePathSettings.ImagePath = dialog.FileName;
        _imageHolder.SaveImage();
    }
}