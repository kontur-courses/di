using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TagsCloudContainer.TagsCloudContainer;
using TagsCloudContainer.TagsCloudContainer.Interfaces;
using TagsCloudContainer.TagsCloudVisualization.Interfaces;

namespace TagsCloudUI
{
    public class TagsCloudForm : Form
    {
        private List<Tag> tags;
        private readonly FormConfig config;
        private readonly IBitmapSaver bitmapSaver;
        private readonly ITagsContainer container;
        private string fontFamily;
        private Size imageSize;

        public TagsCloudForm(ITagsContainer container, FormConfig config, IBitmapSaver bitmapSaver)
        {
            this.config = config;
            this.bitmapSaver = bitmapSaver;
            this.container = container;
        }

        protected override void OnLoad(EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            BackColor = config.BackgroundColor;
            Size = config.FormSize;
            fontFamily = config.FontFamily;
            imageSize = config.FormSize;

            var menu = new MenuStrip();
            menu.Items.Add(new ToolStripMenuItem("Text", null, (sender, args) => GetText()));
            menu.Items.Add(new ToolStripMenuItem("Save", null, (sender, args) => SaveBitmap()));
            menu.Items.Add(new ToolStripMenuItem("Font", null, (sender, args) => ChangeFont()));
            menu.Items.Add(new ToolStripMenuItem("Background", null, (sender, args) => ChangeBackgroundColor()));
            menu.Items.Add(new ToolStripMenuItem("ImageSize", null, (sender, args) => ChangeImageSize()));
            Controls.Add(menu);
        }

        private void GetText()
        {
            var dialog = new OpenFileDialog();

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            tags = container.GetTags(File.ReadAllText(dialog.FileName));
            Invalidate();
        }


        private void ChangeImageSize()
        {
            using var form = new Form {Width = 400, FormBorderStyle = FormBorderStyle.FixedDialog, MaximizeBox = false};

            var labelWidth = new Label {Text = "Width", Width = 50};
            var textBoxWidth = new NumericUpDown {Width = 200, Left = labelWidth.Right, Maximum = 5000, Value = imageSize.Width};
            textBoxWidth.ValueChanged += (sender, args) => imageSize.Width = int.Parse(textBoxWidth.Text);

            var labelHeight = new Label {Text = "Height", Width = 50, Top = textBoxWidth.Bottom};
            var textBoxHeight = new NumericUpDown {Width = 200, Left = labelWidth.Right, Top = textBoxWidth.Bottom, Maximum = 5000, Value = imageSize.Height};
            textBoxHeight.ValueChanged += (sender, args) => imageSize.Height = int.Parse(textBoxHeight.Text);

            var button = new Button {Text = "Set", Top = textBoxHeight.Bottom, DialogResult = DialogResult.OK};
            button.Click += (sender, args) => form.Close();

            form.Controls.Add(labelWidth);
            form.Controls.Add(labelHeight);
            form.Controls.Add(textBoxWidth);
            form.Controls.Add(textBoxHeight);
            form.Controls.Add(button);
            form.ShowDialog();
            Invalidate();
        }

        private void ChangeFont()
        {
            var fontDialog = new FontDialog();

            if (fontDialog.ShowDialog() != DialogResult.OK)
                return;

            fontFamily = fontDialog.Font.FontFamily.Name;
            Invalidate();
        }

        private void ChangeBackgroundColor()
        {
            var colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                BackColor = colorDialog.Color;
            }
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
            Size = imageSize;

            if (tags == null)
                return;

            foreach (var tag in tags)
            {
                e.Graphics.DrawRectangle(new Pen(Color.Red, 1), tag.Rectangle);
                e.Graphics.DrawString(tag.Text, new Font(fontFamily, tag.Font.Size), config.TextColor, tag.Rectangle.X, tag.Rectangle.Y);
            }
        }
    }
}