using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.FileSavers;
using TagsCloudContainer.LayouterAlgorithms;
using TagsCloudContainer.WordsColoringAlgorithms;

namespace TagsCloudContainer
{
    public class CircularCloudDrawer
    {
        private readonly Graphics graphics;
        private readonly string fontName;
        private readonly Color[] brushColors;
        private int counter;
        private readonly Dictionary<Tuple<string, int>, Rectangle> rectanglesForWords;
        private readonly int coefficient;
        private readonly IFileSaver fileSaver;

        public CircularCloudDrawer(IWordStainer stainer, ICloudLayouterAlgorithm layouter, IFileSaver filesaver,
            CoefficientCalculator calculator, CustomSettings settings)
        {
            graphics = Graphics.FromImage(filesaver.Canvas);
            graphics.Clear(Color.FromName(settings.BackGroundColor));
            fontName = settings.FontName;
            brushColors = stainer.GetColorsSequence();
            rectanglesForWords = layouter.GetWordRectangleDictionary();
            coefficient = calculator.CalculateCoefficient();
            fileSaver = filesaver;
        }

        public void DrawWords()
        {
            foreach (var pair in rectanglesForWords)
            {
                var word = pair.Key.Item1;
                var wordCount = pair.Key.Item2;
                var rectangle = pair.Value;
                graphics.DrawString(word, new Font(fontName, (coefficient + 2) * wordCount - 2),
                    new SolidBrush(brushColors[counter]), rectangle);
                counter++;
            }

            fileSaver.SaveCanvas();
        }
    }
}