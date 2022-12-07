using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagsCloudVisualisation.App.CloudCreatorDriver.DrawingSettings;
using TagsCloudVisualisation.App.CloudDrawers;
using TagsCloudVisualisation.App.CloudDrawers.WordToDraw;
using TagsCloudVisualisation.App.DrawingSettings;
using TagsCloudVisualisation.App.InputStream;
using TagsCloudVisualisation.App.RectanglesLayouters;
using TagsCloudVisualisation.App.WordsPreprocessor;
using TagsCloudVisualisation.App.WordsPreprocessor.BoringWords;
using TagsCloudVisualisation.App.WordsPreprocessor.Word;

namespace TagsCloudVisualisation.App.CloudCreator
{
    /// <summary>
    /// Класс, который позволяет создать облаго тегов
    /// </summary>
    public class CloudCreator : ICloudCreator
    {
        public Bitmap CreatePicture(
            IInputWordsStream inputWordsStream, IWordsPreprocessor wordsPreprocessor,
            IReadOnlyCollection<IBoringWords> boringWords,
            ICloudLayouter cloudLayouter, ICloudLayouterSettings cloudLayouterSettings,
            IDrawingSettings drawingSettings, IWordVisualisation defaultVisualisation,
            ICloudDrawer cloudDrawer)
        {
            var allWords = new List<string>();
            while(inputWordsStream.Next())
                allWords.Add(inputWordsStream.GetWord());
            var words = wordsPreprocessor.GetProcessedWords(allWords, boringWords)
                .OrderByDescending(word => word.Tf)
                .ToList();
            var sizes = words.Select(word =>
            {
                var stile = GetVisualisation(word, defaultVisualisation, drawingSettings.Visualisation);
                return GetSizeForWord(word, stile);
            });
            
            cloudLayouter.SetSettings(cloudLayouterSettings);
            var rectangles = GetWordsRectangles(sizes, cloudLayouter);

            var drawingWords =
                CreateDrawingWords(words, rectangles, defaultVisualisation, drawingSettings.Visualisation);

            return cloudDrawer.DrawWords(drawingWords, drawingSettings);
        }

        private static IEnumerable<Rectangle> GetWordsRectangles(IEnumerable<Size> sizes, ICloudLayouter cloudLayouter)
        {
            return sizes.Select(cloudLayouter.PutNextRectangle);
        }

        private static Size GetSizeForWord(IWord word, IWordVisualisation visualisation)
        {
            return TextRenderer.MeasureText(word.Value, visualisation.GetFont());
        }

        private static List<IDrawingWord> CreateDrawingWords(IEnumerable<Word> words,
            IEnumerable<Rectangle> rectangles,
            IWordVisualisation defaultVisualisation,
            IReadOnlyCollection<IWordVisualisation> wordVisualisations)
        {
            var result = new List<IDrawingWord>();
            using var enumerator = rectangles.GetEnumerator();
            foreach (var word in words)
            {
                if (!enumerator.MoveNext()) break;
                var stile = GetVisualisation(word, defaultVisualisation, wordVisualisations);
                result.Add(new DrawingWord(word, stile.GetFont(), stile.GetColor(), enumerator.Current));
            }

            return result;
        }

        private static IWordVisualisation GetVisualisation(IWord word,
            IWordVisualisation defaultVisualisation, IReadOnlyCollection<IWordVisualisation> wordVisualisations)
        {
            if (wordVisualisations == null)
                throw new ArgumentException("words Visualisation can not be null");
                
            return 
                wordVisualisations.FirstOrDefault(v => v.GetStartingValue() <= word.Tf)
                ?? defaultVisualisation;
        }
    }
}