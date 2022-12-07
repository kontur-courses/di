using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualisation.App.CloudCreatorDriver.DrawingSettings;
using TagsCloudVisualisation.App.CloudDrawers;
using TagsCloudVisualisation.App.DrawingSettings;
using TagsCloudVisualisation.App.InputStream;
using TagsCloudVisualisation.App.RectanglesLayouters;
using TagsCloudVisualisation.App.WordsPreprocessor;
using TagsCloudVisualisation.App.WordsPreprocessor.BoringWords;

namespace TagsCloudVisualisation.App.CloudCreator
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