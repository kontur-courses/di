using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using TagsCloud.ImageProcessing;
using TagsCloud.ImageProcessing.ImageBuilders;
using TagsCloud.ImageProcessing.SaverImage;
using TagsCloud.TextProcessing;

namespace TagsCloud.UserInterfaces.GUI
{
    public partial class ConfigWindow : Form
    {
        private readonly FontDialog fontDialog;
        private readonly ColorDialog colorDialog;
        private readonly SaveFileDialog saveFileDialog;
        private readonly OpenFileDialog openFileDialog;
        private readonly TableLayoutPanel tableLayoutPanel;
        private readonly NumericUpDown widthNumeric;
        private readonly NumericUpDown heightNumeric;

        private readonly IWordsConfig wordsConfig;
        private readonly IImageConfig imageConfig;
        private readonly IImageSaver imageSaver;
        private readonly IImageBuilder imageBuilder;


        public ConfigWindow(IWordsConfig wordsConfig, IImageConfig imageConfig,
            IImageSaver imageSaver, IImageBuilder imageBuilder)
        {
            InitializeComponent();

            this.wordsConfig = wordsConfig;
            this.imageConfig = imageConfig;
            this.imageBuilder = imageBuilder;
            this.imageSaver = imageSaver;

            colorDialog = new ColorDialog();
            fontDialog = new FontDialog();
            saveFileDialog = new SaveFileDialog();
            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовые файлы (*.txt)|";

            widthNumeric = new NumericUpDown();
            heightNumeric = new NumericUpDown();

            tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.AutoSize = true;
            Controls.Add(tableLayoutPanel);

            InitView();
        }

        private void InitView()
        {
            var colorButton = new Button();
            AddControl(colorButton, 0, 0, "Выбрать цвет", 1, 2);
            colorButton.Click += (s, e) => colorDialog.ShowDialog();

            var fontButton = new Button();
            AddControl(fontButton, 0, 1, "Выбрать шрифт", 1, 2);
            fontButton.Click += (s, e) => fontDialog.ShowDialog();

            var saveFileButton = new Button();
            AddControl(saveFileButton, 0, 2, "Указать путь для сохранения облака", 1, 2);
            saveFileButton.Click += (s, e) => saveFileDialog.ShowDialog();

            var openButton = new Button();
            AddControl(openButton, 0, 3, "Указать путь для исходного текста", 1, 2);
            openButton.Click += (s, e) => openFileDialog.ShowDialog();

            var sizeImageLabel = new Label();
            AddControl(sizeImageLabel, 0, 4, "Укажите размер изображения", 1, 2);
            sizeImageLabel.TextAlign = ContentAlignment.MiddleCenter;

            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));

            widthNumeric.Maximum = 5000;
            widthNumeric.Value = 500;
            AddControl(widthNumeric, 0, 5, "");

            heightNumeric.Maximum = 5000;
            heightNumeric.Value = 500;
            AddControl(heightNumeric, 1, 5, "");

            var accept = new Button();
            AddControl(accept, 0, 6, "Принять настройки", 1, 2);
            accept.Click += AcceptSettings;

            var showButton = new Button();
            showButton.Dock = DockStyle.Bottom;
            showButton.Text = "Показать изображение";
            showButton.Click += ShowImage;
            Controls.Add(showButton);
        }

        private void AddControl(Control control, int column, int row, string text, int spanRow = 1, int spanColumn = 1)
        {
            control.Text = text;
            control.Dock = DockStyle.Fill;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            tableLayoutPanel.Controls.Add(control, column, row);
            tableLayoutPanel.SetColumnSpan(control, spanColumn);
            tableLayoutPanel.SetRowSpan(control, spanRow);
        }

        private void AcceptSettings(object sender, EventArgs eventArgs)
        {
            var imageSize = new Size((int)widthNumeric.Value, (int)heightNumeric.Value);

            wordsConfig.Color = colorDialog.Color;
            wordsConfig.FontName = fontDialog.Font;
            wordsConfig.Path = openFileDialog.FileName;

            imageConfig.ImageFormat = ImageFormat.Png;
            imageConfig.ImageSize = imageSize;
            imageConfig.Path = saveFileDialog.FileName;
        }

        private void ShowImage(object sender, EventArgs eventArgs)
        {
            tableLayoutPanel.Hide();
            this.Size = imageConfig.ImageSize;
            var bitmap = imageBuilder.BuildImage(wordsConfig.Path);
            using var graphics = CreateGraphics();
            graphics.DrawImage(bitmap, new PointF(0, 0));
            imageSaver.SaveImageWithConfig(bitmap, imageConfig);
        }
    }
}
