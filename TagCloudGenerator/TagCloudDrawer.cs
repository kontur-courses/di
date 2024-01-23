using System.Drawing;
using TagsCloudVisualization.PointDistributors;
using TagsCloudVisualization;
using System.Reflection;

namespace TagCloudGenerator
{
    public class TagCloudDrawer
    {
        public void DrawWordsCloud(string filePath)
        {
            var tagCloudDrawer = new TagCloudDrawer();

            var words = ReadTextFromFile(filePath);           
            tagCloudDrawer.Draw(words);

            Console.WriteLine($"The tag cloud is drawn, the path to the image: {Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)}");
        }

        private string[] ReadTextFromFile(string filePath)
        {
            TextProcessor textProcessor = new TextProcessor();

            var text = File.ReadAllLines(filePath);
            return textProcessor.ProcessTheText(text);
        }

        private void Draw(string[] text)
        {
            var center = new Point(500, 500);
            var distributor = new Spiral(1,center, 0.1);
            var layouter = new CircularCloudLayouter(center, distributor);
            var brush = new SolidBrush(Color.Aqua);
            var font = new Font("Arial", 24);

            var bitmap = new Bitmap(1000, 1000);
            var graphics = Graphics.FromImage(bitmap);
          
            for (var i = 0; i < text.Length; i++)
            {
                SizeF size = graphics.MeasureString(text[i], font);
                var rect = layouter.PutNextRectangle(size.ToSize());

                if (i == 0 )
                    distributor.centerOnPoint = true;

                graphics.DrawString(text[i], font, brush, rect.X, rect.Y);
            }
           
            bitmap.Save("Test33.png");
        }
    }
}