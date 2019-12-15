using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using TagsCloudContainer.Interfaces;
using TagsCloudContainer.Layouter;

namespace TagsCloudContainer
{
    public class TagsCloudContainer
    {
        private ITextReader textReader;
        private IWordsFilter wordsFilter;
        private IWordsCounter wordsCounter;
        private IWordsToSizesConverter wordsToSizesConverter;
        private ICloudLayouter CCL;
        private IVisualiser visualiser;
        private IFileSaver imageSaver;
        private string outputFile;
        private string inputFile;

        public TagsCloudContainer(ITextReader textReader, IWordsFilter wordsFilter, IWordsCounter wordsCounter,
            IWordsToSizesConverter wordsToSizesConverter,
            ICloudLayouter ccl, IVisualiser visualiser, IFileSaver fileSaver,
            string output,
            string input
        )
        {
            this.textReader = textReader;
            this.wordsFilter = wordsFilter;
            this.wordsCounter = wordsCounter;
            this.wordsToSizesConverter = wordsToSizesConverter;
            CCL = ccl;
            this.visualiser = visualiser;
            outputFile = output;
            inputFile = input;
            imageSaver = fileSaver;
        }

        public void Perform()
        {
            var text = textReader.Read(inputFile);
            var textFiltered = wordsFilter.FilterWords(text);
            var wordsCount = wordsCounter.CountWords(textFiltered);
            var sizes = wordsToSizesConverter.GetSizesOf(wordsCount).ToArray();
            sizes = sizes.OrderByDescending(x => x.Item2.Width).ThenBy(x => x.Item2.Height).ToArray();

            CCL.Center = new Point(CCL.Center.X, CCL.Center.Y - sizes[0].Item2.Height);
            for (var i = 0; i < sizes.Length; i++)
            {
                CCL.PutNextRectangle(sizes[i].Item2);
            }

            var bitmap = visualiser.DrawRectangles(CCL, sizes);
            imageSaver.Save(bitmap, outputFile);
        }
    }
}