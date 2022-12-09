using System.Drawing;
using CircularCloudLayouter.Domain;
using TagCloudCreator.Domain.Settings;
using TagCloudCreator.Infrastructure;
using TagCloudCreator.Interfaces;
using TagCloudCreator.Interfaces.Providers;

namespace TagCloudCreator.Domain;

public class TagCloudPainter
{
    private readonly IImageHolder _imageHolder;
    private readonly TagCloudPaintSettings _paintSettings;
    private readonly Func<Graphics, IWordSizeCalculator> _wordCalculatorProvider;
    private readonly Func<IWordSizeCalculator, IWordsPaintDataProvider> _paintDataProviderFactory;

    public TagCloudPainter(
        IImageHolder imageHolder,
        TagCloudPaintSettings paintSettings,
        Func<Graphics, IWordSizeCalculator> wordCalculatorProvider,
        Func<IWordSizeCalculator, IWordsPaintDataProvider> paintDataProviderFactory
    )
    {
        _imageHolder = imageHolder;
        _paintSettings = paintSettings;
        _wordCalculatorProvider = wordCalculatorProvider;
        _paintDataProviderFactory = paintDataProviderFactory;
    }

    public void Paint()
    {
        var imageSize = _imageHolder.GetImageSize();

        using (var graphics = _imageHolder.StartDrawing())
        {
            var calculator = _wordCalculatorProvider(graphics);
            var paintDataProvider = _paintDataProviderFactory(calculator);
            
            using (var backgroundBrush = new SolidBrush(_paintSettings.BackgroundColor))
            {
                graphics.FillRectangle(backgroundBrush, 0, 0, imageSize.Width, imageSize.Height);
            }

            using (var wordsBrush = new SolidBrush(_paintSettings.WordsColor))
            {
                var i = 0;
                foreach (var wordPaintData in paintDataProvider.GetWordsPaintData())
                {
                    using var font = _paintSettings.BasicFont.WithSize(wordPaintData.FontSize);
                    graphics.DrawString(wordPaintData.Word, font, wordsBrush, ConvertRectangle(wordPaintData.Rect));
                    if (++i % 10 == 0)
                        _imageHolder.UpdateUi();
                }
            }
        }

        _imageHolder.UpdateUi();
    }

    private static Rectangle ConvertRectangle(ImmutableRectangle rectangle) =>
        new(
            rectangle.X,
            rectangle.Y,
            rectangle.Width,
            rectangle.Height
        );
}