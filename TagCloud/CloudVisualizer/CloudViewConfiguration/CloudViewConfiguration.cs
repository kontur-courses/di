using System;
using System.Drawing;
using TagCloud.CloudLayouter;
using TagCloud.Factories;
using TagCloud.FigurePaths;
using TagCloud.WordsPreprocessing;

namespace TagCloud.CloudVisualizer.CloudViewConfiguration
{
    public class CloudViewConfiguration
    {
        public IFigurePathFactory FigurePath { get; }
        public Func<ICloudLayouter> CloudLayouter { get; }
        public int WordsCount { get; set; }
        public double ScaleCoefficient { get; set; }
        public Size ImageSize { get; set; }
        public Point CloudCenter { get; set; }
        public FontFamily FontFamily { get; set; }
        public Color BackgroundColor { get; set; }
        public bool NeedSnuggle { get; set; }

        public Brush GetBrush(Word word)
        {
            return new SolidBrush(colorPicker.GetColor(word));
        }

        private IColorWordPicker colorPicker;


        public CloudViewConfiguration(IFigurePathFactory figureFactory, Func<ICloudLayouter> createCloudLayouter, IColorWordPicker colorWordPicker)
        {
            FigurePath = figureFactory;
            CloudLayouter = createCloudLayouter;
            colorPicker = colorWordPicker;
            InitializeDefaultValues();
        }

        private void InitializeDefaultValues()
        {
            WordsCount = 10;
            ScaleCoefficient = 100;
            ImageSize = new Size(600, 300);
            CloudCenter = new Point(300, 150);
            try
            {
                FontFamily = new FontFamily("Algerian");
            }
            catch (Exception)
            {
                FontFamily = FontFamily.GenericSerif;
            }
            BackgroundColor = Color.Green;
        }
    }
}
