using System.Windows.Forms;
using TagsCloud.Infrastructure;
using TagsCloud.Infrastructure.UiActions;

namespace TagsCloud.Actions;

public class SaveFileAction : IUiAction
{
    private readonly IImageHolder imageHolder;

    public SaveFileAction(IImageHolder imageHolder)
    {
        this.imageHolder = imageHolder;
    }

    public MenuCategory Category => MenuCategory.File;
    public string Name => "Сохранить";
    public string Description => "Сохранить изображение в файл";

    public void Perform()
    {
        var imagesDirectoryPath = Path.GetFullPath("..//..//..//images");
        if (!Directory.Exists(imagesDirectoryPath)) Directory.CreateDirectory(imagesDirectoryPath);
        var dialog = new SaveFileDialog
        {
            CheckFileExists = false,
            InitialDirectory = Path.GetFullPath(imagesDirectoryPath),
            DefaultExt = "png",
            FileName = "image.png",
            Filter = "Изображения (*.png)|*.png"
        };
        var res = dialog.ShowDialog();
        if (res == DialogResult.OK)
            imageHolder.SaveImage(dialog.FileName);
    }
}