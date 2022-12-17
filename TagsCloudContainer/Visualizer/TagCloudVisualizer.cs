using System;
using System.Drawing;
using System.IO;
using TagsCloudContainer.CloudLayouter;

namespace TagsCloudContainer.Visualizer
{
    public class TagCloudVisualizer : IVisualizer
    {
        private readonly Bitmap bitmap;
        private readonly ICloudLayouter cloud;
        private readonly Graphics graphics;
        private readonly Options options;

        public TagCloudVisualizer(ICloudLayouter cloud, Options options)
        {
            this.cloud = cloud;
            this.options = options;
            bitmap = new Bitmap(options.Width, options.Height);
            graphics = Graphics.FromImage(bitmap);
        }

        public void Visualize()
        {
            var foregroundBrush = new SolidBrush(Color.FromName(options.ForegroundColor));
            var backgroundBrush = new SolidBrush(Color.FromName(options.BackgroundColor));

            graphics.FillRectangle(backgroundBrush, 0, 0, options.Width, options.Height);
            foreach (var item in cloud.Items)
                graphics.DrawString(
                    item.Word, item.Font, foregroundBrush, item.Rectangle);
        }

        public void Save()
        {
            var outputPath = Path.Combine(Program.ProjectPath, options.OutputFile);
            bitmap.Save(outputPath);
        }

        public SizeF MeasureString(string text, Font font)
        {
            return graphics.MeasureString(text, font);
        }

        public Font GetFontByWeightWord(int weight, int minWeight, int maxWeight)
        {
            var fontSize = Math.Max(options.MinFontSize,
                options.MaxFontSize * (weight - minWeight) / (maxWeight - minWeight));
            return new Font(options.FontFamily, fontSize);
        }
    }
}