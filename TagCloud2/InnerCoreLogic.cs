using System.Drawing;
using System.Linq;
using TagCloud2.Image;
using TagCloud2.TextGeometry;
using TagCloudVisualisation;

namespace TagCloud2
{
    public class InnerCoreLogic
    {
        private readonly IFileReader reader;
        private readonly IWordReader wordReader;
        private readonly IStringPreprocessor preprocessor;
        private readonly IStringToSizeConverter sizeConverter;
        private readonly ICloudLayouter layouter;
        private readonly IColoredCloud coloredCloud;
        private readonly IColoringAlgorithm coloringAlgorithm;
        private readonly IColoredCloudToImageConverter converterToImage;
        private readonly IFileGenerator fileGenerator;
        private readonly IImageFormatter formatter;

        public void Run(IOptions options)
        {
            var fontF = new FontFamily(options.FontName);
            var font = new Font(fontF, options.FontSize);
            var input = reader.ReadFile(options.Path);
            var lines = wordReader.GetUniqueLowercaseWords(input);
            var words = lines
                .Select(x => preprocessor.PreprocessString(x))
                .Where(x => x != "")
                .Select(x => new ColoredSizedWord(x, font))
                .ToArray();

            var rectangles = words.Select(x => sizeConverter.Convert(x.Word, x.Font)).ToArray();
            foreach (var size in rectangles)
            {
                layouter.PutNewRectangle(size);
            }

            var colored = new ColoredCloud();
            colored.AddColoredWordsFromCloudLayouter(words, layouter, coloringAlgorithm);
            var image = converterToImage.GetImage(colored, options.X, options.Y);
            fileGenerator.GenerateFile(options.OutputName, formatter, image);
        }

        public InnerCoreLogic(IFileReader reader, IWordReader wordReader, IStringPreprocessor preprocessor, IStringToSizeConverter sizeConverter,
            ICloudLayouter layouter, IColoredCloud coloredCloud, IColoringAlgorithm coloringAlgorithm, 
            IColoredCloudToImageConverter converterToImage, IFileGenerator fileGenerator, IImageFormatter formatter)
        {
            this.reader = reader;
            this.wordReader = wordReader;
            this.preprocessor = preprocessor;
            this.sizeConverter = sizeConverter;
            this.layouter = layouter;
            this.coloredCloud = coloredCloud;
            this.coloringAlgorithm = coloringAlgorithm;
            this.converterToImage = converterToImage;
            this.fileGenerator = fileGenerator;
            this.formatter = formatter;
        }
    }
}
