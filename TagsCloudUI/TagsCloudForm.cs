using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TagsCloudContainer.TagsCloudContainer;
using TagsCloudContainer.TagsCloudContainer.Interfaces;
using TagsCloudContainer.TagsCloudVisualization.Interfaces;

namespace TagsCloudUI
{
    public class TagsCloudForm : Form
    {
        private readonly List<Tag> tags;
        private readonly FormConfig config;
        private readonly IBitmapSaver bitmapSaver;

        public TagsCloudForm(ITagsContainer container, FormConfig config, string text, IBitmapSaver bitmapSaver)
        {
            tags = container.GetTags(text);
            this.config = config;
            this.bitmapSaver = bitmapSaver;
        }

        protected override void OnLoad(EventArgs e)
        {
            BackColor = config.BackgroundColor;
            Size = config.FormSize;

            var menu = new MenuStrip();
            menu.Items.Add(new ToolStripMenuItem("save", null, (sender, args) => SaveBitmap()));
            Controls.Add(menu);
        }

        private void SaveBitmap()
        {
            var path = Path.Join(Directory.GetCurrentDirectory(), "image.jpg");
            var bitmap = new Bitmap(Width, Height);

            DrawToBitmap(bitmap, new Rectangle(0, 0, Width, Height));
            bitmapSaver.SaveBitmapToDirectory(bitmap, path);

            MessageBox.Show($"saved to: {path}");
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