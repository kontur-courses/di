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
        private ICircularCloudLayouter CCL;
        private IVisualiser Visualiser;
        private string OutputFile;
        
        public TagsCloudContainer(ITextReader textReader, IWordsFilter wordsFilter, IWordsCounter wordsCounter,
            IWordsToSizesConverter wordsToSizesConverter,
            ICircularCloudLayouter ccl, IVisualiser visualiser,
            string output
        )
        {
            TextReader = textReader;
            WordsFilter = wordsFilter;
            WordsCounter = wordsCounter;
            WordsToSizesConverter = wordsToSizesConverter;
            CCL = ccl;
            Visualiser = visualiser;
            OutputFile = output;
        }

        public void Perform()
        {
            var size = new Size(2000, 2000);
//            var textReader = new SimpleTextReader("words.txt");
//            var textFilter = new SimpleWordsFilter(textReader.Read().ToArray());
//            var wordsCounter = new SimpleWordsCounter(textFilter.FilterWords().ToArray());
//            var wordsToSizes = new SimpleWordsToSizesConverter(size,
//                wordsCounter.CountWords().ToDictionary(kvp => kvp.Key, kvp => kvp.Value));
            var sizes = WordsToSizesConverter.GetSizesOf().ToArray();
            sizes = sizes.OrderBy(x => x.Item2.Width).ThenBy(x => x.Item2.Height).ToArray();
           // var oneSizedCcl = new CircularCloudLayouter(new Point(1000, 1000));
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