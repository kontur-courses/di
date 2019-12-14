using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using TagsCloudContainer.Layouter;

namespace TagsCloudContainer
{
    public class TagsCloudContainer
    {
        private ITextReader TextReader;
        private IWordsFilter WordsFilter;
        private IWordsCounter WordsCounter;
        private IWordsToSizesConverter WordsToSizesConverter;
        private ICloudLayouter CCL;
        private IVisualiser Visualiser;
        private string OutputFile;
        private string InputFile;
        
        public TagsCloudContainer(ITextReader textReader, IWordsFilter wordsFilter, IWordsCounter wordsCounter,
            IWordsToSizesConverter wordsToSizesConverter,
            ICloudLayouter ccl, IVisualiser visualiser,
            string output,
            string input
        )
        {
            TextReader = textReader;
            WordsFilter = wordsFilter;
            WordsCounter = wordsCounter;
            WordsToSizesConverter = wordsToSizesConverter;
            CCL = ccl;
            Visualiser = visualiser;
            OutputFile = output;
            InputFile = input;
        }

        public void Perform()
        {
            var size = new Size(2000, 2000);
            var text = TextReader.Read(InputFile);
            var textFiltered = WordsFilter.FilterWords(text);
            var wordsCount = WordsCounter.CountWords(textFiltered);
            var sizes = WordsToSizesConverter.GetSizesOf(wordsCount).ToArray();
            sizes = sizes.OrderBy(x => x.Item2.Width).ThenBy(x => x.Item2.Height).ToArray();
            
            CCL.Center = new Point(CCL.Center.X , CCL.Center.Y - sizes[0].Item2.Height);  
            for (var i = 0; i < sizes.Length; i++)
            {
                CCL.PutNextRectangle(sizes[i].Item2);
            }

            var bitmap = Visualiser.DrawRectangles(CCL, sizes);
            bitmap.Save(OutputFile, ImageFormat.Png);
        }
    }
}