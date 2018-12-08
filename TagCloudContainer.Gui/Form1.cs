using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Autofac;
using TagsCloudContainer;
using TagsCloudContainer.WordLayouts;
using TagsCloudContainer.WordsReaders;

namespace TagCloudContainer.Gui
{
    public partial class Form1 : Form
    {
        private ILifetimeScope scope;
        private Color chooseColor = Color.BlueViolet;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var reader = new CloudContainerBuilder()
                        .BuildReaderContainer()
                        .Resolve<IWordsReader>();
                    textBox1.Text = string.Join(Environment.NewLine,
                        reader.GetWords(openFileDialog.FileName));
                }
            }
        }

        private void SaveFileButton_Click(object sender, EventArgs e)
        {
            using (var saveFileDialog = new SaveFileDialog {Filter = "Images (*.png)|*.png"})
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image.Save(saveFileDialog.FileName, ImageFormat.Png);
                }
            }
        }

        private void ChooseFontColor_Click(object sender, EventArgs e)
        {
            var colorPickerDialog = new ColorDialog {Color = chooseColor};
            colorPickerDialog.ShowDialog();
            chooseColor = colorPickerDialog.Color;
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            var size = new Size(int.Parse(textBox2.Text), int.Parse(textBox3.Text));
            var config = new Config(size, new Font(FontFamily.GenericMonospace, 12), chooseColor);
            var layoutConfig = new CircularCloudLayoutConfig(PointF.Empty, 10);

            scope = new CloudContainerBuilder()
                .BuildTagsCloudContainer(config, layoutConfig)
                .BeginLifetimeScope();

            var tagCloud = scope.Resolve<TagsCloudBuilder>();
            pictureBox1.Image = tagCloud.Visualize(textBox1.Lines);
        }
    }
}