using System.Drawing;
using TagCloudCreator.Domain.Settings;
using TagCloudCreator.Interfaces;
using TagCloudCreator.Interfaces.Providers;

namespace TagCloudCreator.Domain;

public class TagCloudPainter
{
    private readonly IImageHolder _imageHolder;
    private readonly TagCloudPaintSettings _paintSettings;
    private readonly Func<Graphics, IWordsPaintDataProvider> _paintDataProviderFactory;

    public TagCloudPainter(
        IImageHolder imageHolder,
        TagCloudPaintSettings paintSettings,
        Func<Graphics, IWordsPaintDataProvider> paintDataProviderFactory
    )
    {
        _imageHolder = imageHolder;
        _paintSettings = paintSettings;
        _paintDataProviderFactory = paintDataProviderFactory;
    }

    public void Paint()
    {
        var imageSize = _imageHolder.GetImageSize();

        using (var graphics = _imageHolder.StartDrawing())
        {
            using (var backgroundBrush = new SolidBrush(_paintSettings.BackgroundColor))
            {
                graphics.FillRectangle(backgroundBrush, 0, 0, imageSize.Width, imageSize.Height);
            }

            using (var wordsBrush = new SolidBrush(_paintSettings.WordsColor))
            {
                var i = 0;
                foreach (var wordPaintData in _paintDataProviderFactory(graphics).GetWordsPaintData())
                {
                    graphics.DrawString(wordPaintData.Word, wordPaintData.Font, wordsBrush, wordPaintData.Rect);
                    if (++i % 10 == 0)
                        _imageHolder.UpdateUi();
                }
            }
        }

        _imageHolder.UpdateUi();
    }
}