using System;
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
        private readonly FormConfig config;

        public TagsCloudForm(ITagsContainer container, FormConfig config, string text)
        {
            tags = container.GetTags(text);
            this.config = config;
        }

        protected override void OnLoad(EventArgs e)
        {
            BackColor = config.BackgroundColor;
            Size = config.FormSize;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            foreach (var tag in tags)
            {
                e.Graphics.DrawRectangle(new Pen(Color.Red, 1), tag.Rectangle);
                e.Graphics.DrawString(tag.Text, new Font(config.FontFamily, tag.Font.Size), config.TextColor, tag.Rectangle.X, tag.Rectangle.Y);
            }
        }
    }
}