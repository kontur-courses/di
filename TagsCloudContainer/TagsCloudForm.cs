using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TagsCloudContainer.TagsCloudVisualization;

namespace TagsCloudContainer
{
    public class TagsCloudForm : Form
    {
        private readonly ILayouter layouter;
        private readonly Dictionary<string, Rectangle> rectangles;
        private readonly Dictionary<string, int> wordsEntry;

        public TagsCloudForm(ITextParser parser, ILayouter layouter)
        {
            this.layouter = layouter;
            var root = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            var path = $"{root}\\TagsCloudContainer\\Texts\\ParsedSong.txt";
            wordsEntry = parser.GetParsedText(File.ReadAllText(path));
            rectangles = new Dictionary<string, Rectangle>();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            foreach (var key in wordsEntry.Keys) DrawTag(e, key);
        }

        private void DrawTag(PaintEventArgs e, string word)
        {
            var stringFont = new Font("Arial", wordsEntry[word] + 10);

            if (!rectangles.ContainsKey(word))
            {
                var stringSize = e.Graphics.MeasureString(word, stringFont).ToSize();
                var rectangle = layouter.PutNextRectangle(stringSize);
                rectangles.Add(word, rectangle);
            }

            e.Graphics.DrawRectangle(new Pen(Color.Red, 1), rectangles[word]);
            e.Graphics.DrawString(word, stringFont, Brushes.Black, rectangles[word].X, rectangles[word].Y);
        }
    }
}