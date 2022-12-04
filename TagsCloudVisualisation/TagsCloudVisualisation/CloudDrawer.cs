using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    public partial class CloudDrawer : Form
    {
        private readonly int count;
        private readonly double spiralStep;
        private readonly Point center;
        
        public CloudDrawer(int count, Point center, double spiralStep)
        {
            this.count = count;
            this.spiralStep = spiralStep;
            this.center = center;
            InitializeComponent();
            var rectangles = RandomRectanglesCreator.GetRectangles(count, center, spiralStep);
            var bitmap = TagCloudDrawer.DrawWithAutoSize(rectangles, Color.White, Color.DarkOrange);

            var statusBar = AddInfoBar();
            var pictureBox = AddPictureBox(bitmap, statusBar);
            var field = AddFileNameField(pictureBox);
            var button = AddSaveButton(field);
            StartPosition = FormStartPosition.CenterScreen;
            SetWindowSize();
        }

        private void SetWindowSize()
        {
            var height = 0;
            var width = 0;
            foreach (Control control in Controls)
            {
                height = Math.Max(height, control.Bottom);
                width = Math.Max(width, control.Right);
            }
            Size = new Size(width + 10, height + 10);
            AutoSize = true;
        }

        private Control AddInfoBar()
        {
            var info = new Label();
            info.Name = "infoBar";
            info.Location = new Point(5, 5);
            info.Text = $"{count} прямоугольников. Центр ({center.X}, {center.Y})\nШаг спирали {spiralStep}";
            info.AutoSize = true;
            Controls.Add(info);
            return info;
        }
        
        private Control AddFileNameField(Control image)
        {
            var field = new TextBox();
            field.Name = "fileName";
            field.Height = 10;
            field.Width = 250;
            field.Top = image.Bottom + 10;
            field.Left = 10;
            field.Text = $"tcv n={count}, c=({center.X}, {center.Y}), step={spiralStep}.png";
            Controls.Add(field);
            field.BringToFront();
            return field;
        }
        
        private Control AddSaveButton(Control fileNameField)
        {
            var button = new Button();
            button.Text = "Сохранить";
            button.Name = "saveButton";
            button.Click += SaveImage;
            button.Left = fileNameField.Right + 10;
            button.Top = fileNameField.Top;
            Controls.Add(button);
            button.BringToFront();
            return button;
        }

        private Control AddPictureBox(Image image, Control info)
        {
            var pictureBox = new PictureBox();
            pictureBox.Image = image;
            pictureBox.Height = image.Height;
            pictureBox.Width = image.Width;
            pictureBox.Name = "image";
            pictureBox.Top = info.Bottom + 10;
            Controls.Add(pictureBox);
            return pictureBox;
        }

        private void SaveImage(object sender, EventArgs eventArgs)
        {
            var field = Controls.Find("fileName", false).First();
            var fileName = field.Text.EndsWith(".png") ? field.Text : field.Text + ".png";
            var pictureBox = Controls.Find("image", false).First();
            var path = AppContext.BaseDirectory;
            ((PictureBox)pictureBox).Image.Save(fileName);
            MessageBox.Show($"Изображение было сохранено в {path + fileName}");
        }
    }
}