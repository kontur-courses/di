using System.Drawing;
using TagsCloud.Infrastructure;
using TagsCloud.Infrastructure.UiActions;

namespace TagsCloud.Actions;

public class FlowerCloudAction : IUiAction
{
    private readonly IImageHolder imageHolder;
    private readonly TagCloudPainter painter;
    private readonly AppSettings settings;

    public FlowerCloudAction(
        IImageHolder imageHolder, TagCloudPainter painter, AppSettings settings)
    {
        this.settings = settings;
        this.imageHolder = imageHolder;
        this.painter = painter;
    }
    
    public void Perform()
    {
        if (settings.File == null){
            throw new Exception("сначала загрузи файл");
        }
        var size = imageHolder.GetImageSize();
        painter.Paint(new FlowerSpiral(new Point(size.Width/2, size.Height/2), 0.5, 4), settings.File.FullName);
    }

    public MenuCategory Category => MenuCategory.Types;
    public string Name => "Цветок";
    public string Description => "";
}