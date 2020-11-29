using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TagsCloudContainer.TagsCloudVisualization;

namespace TagsCloudContainer
{
    public class TagsCloudForm : Form
    {
        private readonly ILayouter layouter;
        private readonly Dictionary<string, Rectangle> rectangles;
        private readonly Dictionary<string, int> wordsEntry;

        public TagsCloudForm(Dictionary<string, int> wordsEntry, ILayouter layouter)
        {
            this.wordsEntry = wordsEntry;
            this.layouter = layouter;
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