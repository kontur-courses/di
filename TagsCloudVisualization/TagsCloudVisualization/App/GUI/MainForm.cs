using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    public class MainForm : Form, IApplication
    {
        private Image image;
        private IFileReader fileReader;
        private IVisualizer visualizer;
        private IWordPalette wordPalette;
        private ISizeDefiner sizeDefiner;
        private ICloudLayouter layouter;
        private List<GraphicWord> words;
        private WordCounter counter = new WordCounter();
        private IImageSettings imageSettings;
        private string rawFile;

        public MainForm(IFileReader fileReader, IVisualizer visualizer, IWordPalette wordPalette, ISizeDefiner sizeDefiner, IImageSettings imageSettings, ICloudLayouter cloudLayouter)
        {
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Size = new Size(616, 664);
            this.fileReader = fileReader;
            this.visualizer = visualizer;
            this.wordPalette = wordPalette;
            this.sizeDefiner = sizeDefiner;
            this.imageSettings = imageSettings;
            layouter = cloudLayouter;

            var mainMenu = new MenuStrip();
            mainMenu.Dock = DockStyle.Top;
            Controls.Add(mainMenu);

            var fileItem = new ToolStripMenuItem("Файл");
            var openButton = new ToolStripMenuItem("Открыть файл");
            openButton.Click += OpenFile;
            fileItem.DropDownItems.Add(openButton);

            var saveButton = new ToolStripMenuItem("Сохранить файл");
            saveButton.Click += SaveFile;
            fileItem.DropDownItems.Add(saveButton);

            var algorithmItem = new ToolStripMenuItem("Алгоритм");
            var circularAlgorithm = new ToolStripMenuItem("Круговой");
            circularAlgorithm.Click += (sender, args) =>
            {
                layouter = new CircularCloudLayouter(sizeDefiner);
                CheckOneItem(circularAlgorithm, algorithmItem);
                if (image != null)
                    GenerateImage(sender, args);
            };
            algorithmItem.DropDownItems.Add(circularAlgorithm);

            var spiralAlgorithm = new ToolStripMenuItem("Спиральный");
            spiralAlgorithm.Click += (sender, args) =>
            {
                layouter = new SpiralCloudLayouter(sizeDefiner);
                CheckOneItem(spiralAlgorithm, algorithmItem);
                if (image != null)
                    GenerateImage(sender, args);
            };
            algorithmItem.DropDownItems.Add(spiralAlgorithm);

            var settingItem = new ToolStripMenuItem("Расцевтка");
            var paletteButton = new ToolStripMenuItem("Палитра");
            paletteButton.Click += SetMonochromePalette;
            settingItem.DropDownItems.Add(paletteButton);

            var gradPaletteButton = new ToolStripMenuItem("Градиентная палитра");
            gradPaletteButton.Click += SetGradientPalette;
            settingItem.DropDownItems.Add(gradPaletteButton);

            var sizerItem = new ToolStripMenuItem("Размер слова");
            var linearSizer = new ToolStripMenuItem("Линейный");
            linearSizer.Click += (sender, args) => {
                this.sizeDefiner = new LinearSizer();
                CheckOneItem(linearSizer, sizerItem);
                if (image != null)
                    GenerateImage(sender, args);
            };
            sizerItem.DropDownItems.Add(linearSizer);

            var nSizer = new ToolStripMenuItem(".Net");
            nSizer.Click += (sender, args) => {
                this.sizeDefiner = new NWordSizer();
                CheckOneItem(nSizer, sizerItem);
                if (image != null)
                    GenerateImage(sender, args);
            };
            sizerItem.DropDownItems.Add(nSizer);

            var settingsItems = new ToolStripMenuItem("Настройки");
            var imageSettingsButton = new ToolStripMenuItem("Размеры изображения");
            imageSettingsButton.Click += (sender, args) =>
            {
                new SettingsForm<IImageSettings>(imageSettings).ShowDialog();
                if (image != null)
                    GenerateImage(sender, args);
            };
            settingsItems.DropDownItems.Add(imageSettingsButton);

            var fontSettingsButton = new ToolStripMenuItem("Шрифт");
            fontSettingsButton.Click += (sender, args) =>
            {
                new SettingsForm<WordCounter>(counter).ShowDialog();
                if (rawFile != null)
                {
                    words = counter.Count(true, rawFile);
                    GenerateImage(sender, args);
                }
            };
            settingsItems.DropDownItems.Add(fontSettingsButton);


            mainMenu.Items.Add(fileItem);
            mainMenu.Items.Add(algorithmItem);
            mainMenu.Items.Add(settingItem);
            mainMenu.Items.Add(sizerItem);
            mainMenu.Items.Add(settingsItems);
        }

        private void CheckOneItem(ToolStripMenuItem item, ToolStripMenuItem parent)
        {
            foreach (var parentDropDownItem in parent.DropDownItems)
            {
                ((ToolStripMenuItem) parentDropDownItem).Checked = false;
            }

            item.Checked = true;
        }

        private void SetMonochromePalette(object sender, EventArgs args)
        {
            if (!(wordPalette is MonochromePalette))
                wordPalette = new MonochromePalette(Color.Black, Color.White);

            new SettingsForm<IWordPalette>(wordPalette).ShowDialog();
            if (image != null)
                GenerateImage(sender, args);
        }

        private void SetGradientPalette(object sender, EventArgs args)
        {
            if (!(wordPalette is GradientPalette))
                wordPalette = new GradientPalette(Color.Red, Color.Blue, Color.Black);

            new SettingsForm<IWordPalette>(wordPalette).ShowDialog();
            if (image != null)
                GenerateImage(sender, args);
        }

        private void OpenFile(object sender, EventArgs args)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Text file | *.txt; ";
            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;
            var path = dialog.FileName;
            rawFile = fileReader.Read(path);
            words = counter.Count(true, rawFile);

            GenerateImage(sender, args);
        }

        private void GenerateImage(object sender, EventArgs args)
        {
            layouter.Process(words, sizeDefiner, imageSettings.Center);
            image = visualizer.Render(words, imageSettings.Size.Width, imageSettings.Size.Height, wordPalette);
            Size = imageSettings.Size;
            Invalidate();
        }

        private void SaveFile(object sender, EventArgs args)
        {
            if (image == null)
                return;

            var dialog = new SaveFileDialog();
            dialog.Filter = "BMP | *.bmp| PNG | *.png;";
            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;
            var path = dialog.FileName;
            ImageSaver.WriteToFile(path, image);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (image != null)
                e.Graphics.DrawImage(image, new Point(0, 0));
        }
    }
}
