using TagsCloudPainterApplication.Infrastructure;

namespace TagsCloudPainterApplication.Actions;

public class SaveImageAction : IUiAction
{
    private readonly IImageHolder imageHolder;

    public SaveImageAction(IImageHolder imageHolder)
    {
        this.imageHolder = imageHolder;
    }

    public string Category => "Файл";
    public string Name => "Сохранить...";
    public string Description => "Сохранить изображение в файл";

    public void Perform()
    {
        var dialog = new SaveFileDialog
        {
            CheckFileExists = false,
            InitialDirectory = Path.GetFullPath(Environment.CurrentDirectory),
            DefaultExt = "png",
            FileName = "image.png",
            Filter = "Изображения (*.png)|*.png|Изображения (*.jpeg)|*.jpeg"
        };
        var res = dialog.ShowDialog();
        if (res == DialogResult.OK)
            imageHolder.SaveImage(dialog.FileName);
    }
}