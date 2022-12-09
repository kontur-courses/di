using TagCloudApp.Domain;
using TagCloudApp.Infrastructure;
using TagCloudCreator.Interfaces;
using TagCloudCreator.Interfaces.Providers;

namespace TagCloudApp.Actions;

public class SaveImageAction : IUiAction
{
    private readonly IImageHolder _imageHolder;
    private readonly IImagePathSettingsProvider _imagePathSettingsProvider;
    private readonly IImageSaverProvider _imageSaverProvider;

    public SaveImageAction(
        IImageHolder imageHolder,
        IImagePathSettingsProvider imagePathSettingsProvider,
        IImageSaverProvider imageSaverProvider
    )
    {
        _imageHolder = imageHolder;
        _imagePathSettingsProvider = imagePathSettingsProvider;
        _imageSaverProvider = imageSaverProvider;
    }

    public MenuCategory Category => MenuCategory.File;
    public string Name => "Save image...";
    public string Description => "Save image to file";

    private string? _filter;

    public void Perform()
    {
        var imagePathSettings = _imagePathSettingsProvider.GetImagePathSettings();
        _filter ??= string.Join("|", _imageSaverProvider.SupportedExtensions
            .Select(extension => $"{extension}|*{extension}")
        );

        var initialDirectory = Path.GetFullPath(imagePathSettings.ImagePath);
        if (File.Exists(initialDirectory))
            initialDirectory = new FileInfo(initialDirectory).Directory!.FullName;
        var dialog = new SaveFileDialog
        {
            CheckFileExists = false,
            InitialDirectory = initialDirectory,
            FileName = "image",
            AddExtension = true,
            Filter = _filter
        };
        var res = dialog.ShowDialog();
        if (res is not DialogResult.OK)
            return;

        imagePathSettings.ImagePath = dialog.FileName;
        _imageHolder.SaveImage();
    }
}