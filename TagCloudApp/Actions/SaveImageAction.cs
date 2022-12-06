using TagCloudApp.Abstractions;

namespace TagCloudApp.Actions;

public class SaveImageAction : IUiAction
{
    private readonly IImageDirectoryProvider _imageDirectoryProvider;
    private readonly IImageHolder _imageHolder;

    public SaveImageAction(IImageDirectoryProvider imageDirectoryProvider, IImageHolder imageHolder)
    {
        _imageDirectoryProvider = imageDirectoryProvider;
        _imageHolder = imageHolder;
    }

    public MenuCategory Category => MenuCategory.File;
    public string Name => "Сохранить...";
    public string Description => "Сохранить изображение в файл";

    public void Perform()
    {
        var dialog = new SaveFileDialog
        {
            CheckFileExists = false,
            InitialDirectory = Path.GetFullPath(_imageDirectoryProvider.ImagesDirectory),
            DefaultExt = "bmp",
            FileName = "image.bmp",
            Filter = "Изображения (*.bmp)|*.bmp"
        };
        var res = dialog.ShowDialog();
        if (res == DialogResult.OK)
            _imageHolder.SaveImage(dialog.FileName);
    }
}