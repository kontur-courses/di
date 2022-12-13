using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Autofac;
using TagsCloudContainer;

namespace TagsCloudGUI
{
    public partial class MainForm : Form
    {
        private InputFileHandler inputFileHandler;
        private ISettingsProvider settingsProvider;
        public MainForm(InputFileHandler inputTextProvider, ISettingsProvider settingsProvider)
        {
            InitializeComponent();
            inputFileHandler = inputTextProvider;
            this.settingsProvider = settingsProvider;
        }

        private void but_fileOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = openFileDialog.FileName;
            inputFileHandler.InputFilePath = filename;
        }

        private void but_generate_Click(object sender, EventArgs e)
        {
            try
            {
                var container = BuildContainer();
                var imgDrawer = container.Resolve<IDrawer>();
                var bitmap = imgDrawer.DrawImage();
                pic_main.BackgroundImage = bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void but_save_Click(object sender, EventArgs e)
        {
            int heigth = TryToParse(box_heightPic.Text);
            int width = TryToParse(box_widthPic.Text);
            var image = pic_main.BackgroundImage;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PNG picture(*.png)|*.png|JPG picture(*.jpg)|*.jpg";
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog.FileName;
            if (heigth > 0 && width > 0)
            {
                image = new Bitmap(image, new Size(width, heigth));
            }
            image.Save(filename);
        }

        private void but_backgroundColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.FullOpen = true;
            colorDialog.Color = settingsProvider.Settings.BackgroundColor;
            if (colorDialog.ShowDialog() == DialogResult.Cancel)
                return;
            settingsProvider.Settings.BackgroundColor = colorDialog.Color;
        }

        private void but_fontColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.FullOpen = true;
            colorDialog.Color = settingsProvider.Settings.FontColor;
            if (colorDialog.ShowDialog() == DialogResult.Cancel)
                return;
            settingsProvider.Settings.FontColor = colorDialog.Color;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() == DialogResult.Cancel)
                return;
            Font font = fontDialog.Font;
            settingsProvider.Settings.Font = font;
        }

        private int TryToParse(string str)
        {
            int number;
            try
            {
                number = int.Parse(str);
            }
            catch
            {
                number = 0;
            }

            return number;
        }

        private void but_openExludedWordForm_Click(object sender, EventArgs e)
        {
            var form = new ExludedWordsForm(inputFileHandler.WordsFilter);
            form.Show();
        }
        private IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<DefaultDrawer>().As<IDrawer>();
            builder.RegisterType<DefaultRectangleArranger>().As<IRectangleArranger>();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
            builder.RegisterInstance(inputFileHandler).As<IInputTextProvider>();
            builder.RegisterInstance(settingsProvider).As<ISettingsProvider>();
        
            return builder.Build();
        }
    }
}