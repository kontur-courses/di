using System.Drawing;
using System.Linq;
using TagsCloudContainer.TextAnalyzing;

namespace TagsCloudContainer.Common
{
    internal class Gradient : IColorSettings
    {
        private readonly TagCreator tagCreator;
        private Color currentColor;
        private double deltaB;
        private double deltaG;
        private double deltaR;
        private int numberOfColor;
        private int tagsCount;

        public Gradient(TagCreator tagCreator)
        {
            this.tagCreator = tagCreator;
        }

        public Color ColorFrom { get; set; } = Color.White;
        public Color ColorTo { get; set; } = Color.Blue;
        public Color BackgroundColor { get; set; } = Color.Black;

        public Color GetNextColor()
        {
            if (numberOfColor == 0)
                RenewGradient();
            var currentR = GetNumberOfColor(currentColor.R + deltaR, ColorTo.R);
            var currentG = GetNumberOfColor(currentColor.G + deltaG, ColorTo.G);
            var currentB = GetNumberOfColor(currentColor.B + deltaR, ColorTo.B);
            currentColor = Color.FromArgb(currentR, currentG, currentB);
            numberOfColor++;
            if (numberOfColor == tagsCount)
                numberOfColor = 0;
            return currentColor;
        }

        public int GetNumberOfColor(double calculatedNumber, int colorToNumber)
        {
            return (int) calculatedNumber < 0 || (int) calculatedNumber > 255
                ? colorToNumber
                : (int) calculatedNumber;
        }

        public void RenewGradient()
        {
            tagsCount = tagCreator.GetTagsForVisualization().Count();
            deltaR = (double) (ColorTo.R - ColorFrom.R) / tagsCount;
            deltaG = (double) (ColorTo.G - ColorFrom.G) / tagsCount;
            deltaB = (double) (ColorTo.B - ColorFrom.B) / tagsCount;
            currentColor = ColorFrom;
        }
    }
}