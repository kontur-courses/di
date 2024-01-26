using System.Windows.Forms;
using TagsCloud.Infrastructure;
using TagsCloud.Infrastructure.UiActions;

namespace TagsCloud.App.Actions;

public class UploadFileAction : IUiAction
{
    private readonly AppSettings settings;

    public UploadFileAction(AppSettings settings)
    {
        this.settings = settings;
    }

    public MenuCategory Category => MenuCategory.File;
    public string Name => "Загрузить";
    public string Description => "";

    public void Perform()
    {
        var openFileDialog = new OpenFileDialog
        {
            Title = "Выберите файл",
            Filter = "Текстовые файлы (*.txt)|*.txt|Документы (*.doc;*.docx)|*.doc;*.docx|Все файлы (*.*)|*.*",
            FilterIndex = 1,
            RestoreDirectory = true
        };
        if (openFileDialog.ShowDialog() != DialogResult.OK) return;
        settings.File = new FileInfo(openFileDialog.FileName);
    }
}