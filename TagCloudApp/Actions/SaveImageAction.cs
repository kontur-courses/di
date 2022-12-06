using TagCloudApp.Domain;
using TagCloudApp.Infrastructure;
using TagCloudCreator.Interfaces;
using TagCloudCreator.Interfaces.Providers;

namespace TagCloudApp.Actions;

public class SaveImageAction : IUiAction
{
    private readonly IImageHolder _imageHolder;
    private readonly IImagePathProvider _imagePathProvider;
    private readonly IImageSaverProvider _imageSaverProvider;

    public SaveImageAction(
        IImageHolder imageHolder,
        IImagePathProvider imagePathProvider,
        IImageSaverProvider imageSaverProvider
    )
    {
        _imageHolder = imageHolder;
        _imagePathProvider = imagePathProvider;
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
            InitialDirectory = Path.GetFullPath(_imagePathProvider.ImagePath),
            FileName = "image",
            AddExtension = true,
            Filter = _filter
        };
        var res = dialog.ShowDialog();
        if (res is not DialogResult.OK)
            return;

        _imagePathProvider.ImagePath = dialog.FileName;
        _imageHolder.SaveImage();
    }
}