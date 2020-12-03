using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TagsCloudContainer.TagsCloudContainer;
using TagsCloudContainer.TagsCloudContainer.Interfaces;

namespace TagsCloudUI
{
    public class TagsCloudForm : Form
    {
        private readonly List<Tag> tags;

        public TagsCloudForm(ITagsContainer container, string text)
        {
            tags = container.GetTags(text);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            foreach (var tag in tags)
            {
                e.Graphics.DrawRectangle(new Pen(Color.Red, 1), tag.Rectangle);
                e.Graphics.DrawString(tag.Text, tag.Font, Brushes.Black, tag.Rectangle.X, tag.Rectangle.Y);
            }
        }
    }
}