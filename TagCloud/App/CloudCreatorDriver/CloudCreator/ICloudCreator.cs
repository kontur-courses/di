using System.Drawing;
using TagCloud.App.CloudCreatorDriver.CloudDrawers;
using TagCloud.App.CloudCreatorDriver.DrawingSettings;
using TagCloud.App.CloudCreatorDriver.RectanglesLayouters;
using TagCloud.App.WordPreprocessorDriver.InputStream;
using TagCloud.App.WordPreprocessorDriver.InputStream.TextSplitters;
using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor;
using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.BoringWords;

namespace TagCloud.App.CloudCreatorDriver.CloudCreator;

public interface ICloudCreator
{
    Bitmap CreatePicture(
        IInputWordsStream inputWordsStream,
        IWordsPreprocessor wordsPreprocessor,
        ITextSplitter textSplitter,
        IReadOnlyCollection<IBoringWords> boringWords,
        ICloudLayouter cloudLayouter,
        ICloudLayouterSettings cloudLayouterSettings,
        ICloudDrawer cloudDrawer,
        IDrawingSettings drawingSettings,
        IWordVisualisation defaultVisualisation);
}