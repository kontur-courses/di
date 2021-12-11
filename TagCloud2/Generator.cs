using System;
using System.Drawing;
using Autofac;
using TagCloud2.Image;
using TagCloud2.Text;
using TagCloud2.TextGeometry;
using TagCloudVisualisation;

namespace TagCloud2
{
    public class Generator
    {
        public void Generate()
        {
            var FR = new TxtFileReader();
            var WR = new LinesWordReader();
            var SWR = new SillyWordsRemover();
            var SWS = new ShortWordsSelector();
            var PCPU = new StringPreprocessor(SWR, SWS);
            var SC = new StringToRectangleConverter();
            var Spiral = new ArchimedeanSpiral(new Point (500, 500));
            var CL = new CircularCloudLayouter(Spiral);
            var CC = new ColoredCloud();
            var CA = new RandomColoringAlgorithm();
            var CTI = new ColoredCloudToBitmap();
            var FG = new FileGenerator();
            var JPEG = new JpegImageFormatter();

            var core = new Core(FR, WR, PCPU, SC, CL, CC, CA, CTI, FG, JPEG);
            core.Run("input.txt", SystemFonts.DefaultFont, "output.jpg");
        }
    }
}
