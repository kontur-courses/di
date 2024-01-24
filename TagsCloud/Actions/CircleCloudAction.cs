using System.Drawing;
using TagsCloud.Infrastructure;
using TagsCloud.Infrastructure.UiActions;

namespace TagsCloud.Actions;

public class CircleCloudAction : IUiAction
{
    private readonly IImageHolder imageHolder;
    private readonly TagCloudPainter painter;
    private readonly AppSettings settings;

    public CircleCloudAction(
        AppSettings appSettings, IImageHolder imageHolder, TagCloudPainter painter)
    {
        settings = appSettings;
        this.imageHolder = imageHolder;
        this.painter = painter;
    }

    public MenuCategory Category => MenuCategory.Types;
    public string Name => "Круг";
    public string Description => "";

    public void Perform()
    {
        if (settings.File == null) throw new Exception("сначала загрузи файл");
        var size = imageHolder.GetImageSize();
        painter.Paint(new Spiral(new Point(size.Width / 2, size.Height / 2)), settings.File.FullName);
    }
}