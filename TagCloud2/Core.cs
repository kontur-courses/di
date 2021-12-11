using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagCloud2.Image;
using TagCloud2.TextGeometry;
using TagCloudVisualisation;

namespace TagCloud2
{
    public class Core
    {
        private IFileReader reader;
        private IWordReader wordReader;
        private IStringPreprocessor preprocessor;
        private IStringToSizeConverter sizeConverter;
        private ICloudLayouter layouter;
        private IColoredCloud coloredCloud;
        private IColoringAlgorithm coloringAlgorithm;
        private IColoredCloudToImageConverter converterToImage;
        private IFileGenerator fileGenerator;
        private IImageFormatter formatter;

        public void Run(string fileName, Font font, string outputName)
        {
            var input = reader.ReadFile(fileName);
            var lines = wordReader.GetWords(input);
            lines = lines.Select(x => preprocessor.PreprocessString(x)).Where(x => x != "").ToArray();
            var rectangles = lines.Select(x => sizeConverter.Convert(x, font)).ToArray();
            foreach (var size in rectangles)
            {
                var currentRectangle = layouter.PutNewRectangle(size);
            }

            var colored = coloredCloud.GetFromCloudLayouter(lines, layouter, coloringAlgorithm, font);
            var image = converterToImage.GetImage(colored);
            fileGenerator.GenerateFile(outputName, formatter, image);
        }

        public Core(IFileReader reader, IWordReader wordReader, IStringPreprocessor preprocessor, IStringToSizeConverter sizeConverter,
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
