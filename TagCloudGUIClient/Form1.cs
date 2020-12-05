using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using Autofac;
using CloudLayouters;
using TagCloudCreator;

namespace TagCloudGUIClient
{
    public partial class Form1 : Form
    {
        private CloudPrinter? cloudPrinter;
        private List<IColorSelector>? colorSelectors;
        private List<BaseCloudLayouter>? layouters;

        public Form1()
        {
            InitializeComponent();
            fontSelector.Items.AddRange(FontFamily.Families.Select(x => x.Name).ToArray());
        }

        private Size ImageSize { get; set; }

        private IContainer InitializeContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<CircularCloudLayouter>().As<BaseCloudLayouter>().AsSelf();
            builder.RegisterType<RectangleLayouter>().As<BaseCloudLayouter>().AsSelf();
            builder.Register(context => (Point) (ImageSize / 2)).AsSelf();
            builder.RegisterType<CloudPrinter>().AsSelf();
            builder.RegisterType<TxtFileReader>().As<IFileReader>().AsSelf();
            builder.RegisterType<FullRandomColorSelector>().As<IColorSelector>();
            builder.RegisterType<RandomFromColorsColorSelector>().As<IColorSelector>();
            builder.Register(x => new SingleColorSelector(Color.Black)).As<IColorSelector>();
            builder.RegisterType<TextExtractorBasedReader>().As<IFileReader>();
            return builder.Build();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void drawButton_Click(object sender, EventArgs e)
        {
            var container = InitializeContainer();
            cloudPrinter = container.Resolve<CloudPrinter>();
            layouters = container.Resolve<IEnumerable<BaseCloudLayouter>>().ToList();
            colorSelectors = container.Resolve<IEnumerable<IColorSelector>>().ToList();
            RedrawImage();
        }


        private void fileSelector_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = @"files with text|*.txt;*.doc;*.docx;*.html;*.pdf";
            if (openFileDialog.ShowDialog() == DialogResult.OK) textBox1.Text = openFileDialog.FileName;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            using SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = @"png files (*.png)|*.png";
            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
            var stream = saveFileDialog.OpenFile();
            pictureBox1.Image.Save(stream, ImageFormat.Png);
            stream.Close();
        }

        private void RedrawImage()
        {
            if (!AllSelected())
                return;
            var fontFamily = FontFamily.Families[fontSelector.SelectedIndex];
            var colorSelector = colorSelectors?[colorSelectorSelector.SelectedIndex];
            var layouter = layouters?[layouterSelector.SelectedIndex];
            var path = textBox1.Text;
            if (layouter != null && colorSelector != null)
                pictureBox1.Image =
                    cloudPrinter?.DrawCloud(path,
                        layouter,
                        ImageSize,
                        fontFamily,
                        colorSelector);
        }

        private bool AllSelected()
        {
            return fontSelector.SelectedIndex != -1
                   && layouterSelector.SelectedIndex != -1
                   && colorSelectorSelector.SelectedIndex != -1
                   && textBox1.Text != "";
        }

        private void sizeSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            var pair = ((string) sizeSelector.SelectedItem).Split('x');
            ImageSize = new Size(int.Parse(pair[0]), int.Parse(pair[1]));
        }
    }
}