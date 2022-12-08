using System.Drawing;
using TagCloud.App.CloudCreatorDriver.CloudDrawers;
using TagCloud.App.CloudCreatorDriver.DrawingSettings;
using TagCloud.App.CloudCreatorDriver.RectanglesLayouters;
using TagCloud.App.WordPreprocessorDriver.InputStream;
using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor;
using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.BoringWords;

namespace TagCloud.App.CloudCreatorDriver.CloudCreator
{
    /// <summary>
    /// Интерфейс, который позволяет создать изображение облака тегов
    /// </summary>
    public interface ICloudCreator
    {
        Bitmap CreatePicture(
            IInputWordsStream inputWordsStream,
            IWordsPreprocessor wordsPreprocessor,
            IReadOnlyCollection<IBoringWords> boringWords,
            ICloudLayouter cloudLayouter,
            ICloudLayouterSettings cloudLayouterSettings,
            IDrawingSettings drawingSettings,
            IWordVisualisation defaultVisualisation,
            ICloudDrawer cloudDrawer);
    }
}