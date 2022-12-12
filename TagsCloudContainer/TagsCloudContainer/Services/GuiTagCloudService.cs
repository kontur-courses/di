using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.Algorithm;
using TagsCloudContainer.Extensions;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Visualisator;

namespace TagsCloudContainer.Services
{
    public class GuiTagCloudService : ITagCloudService
    {
        private readonly IPainter painter;
        private readonly ICloudLayouter cloudLayouter;
        private readonly ImageSettings imageSettings;
        private readonly IWordsCounter wordscounter;
        private readonly PictureBox pictureBox;

        public GuiTagCloudService(ImageSettings imageSettings, PictureBox pictureBox, ICloudLayouter cloudLayouter, 
            IPainter painter, IWordsCounter wordsCounter)
        {
            this.imageSettings = imageSettings;
            this.pictureBox = pictureBox;
            this.cloudLayouter = cloudLayouter;
            this.painter = painter;
            this.wordscounter = wordsCounter;
        }

        public void SetSettings<TSettings>(TSettings property)
        {
            SettingsForm.For(property).ShowDialog();
            pictureBox.RecreateImage(imageSettings);
        }

        public string SetFilePath(string filePath)
        {
            var dialog = new OpenFileDialog()
            {
                CheckFileExists = true,
                InitialDirectory = Path.GetFullPath(filePath),
                DefaultExt = "txt",
                FileName = "boring.txt",
                Filter = "Текстовые файлы (*.txt)|*.txt"
            };
            var res = dialog.ShowDialog();
            if (res == DialogResult.OK)
                return dialog.FileName;

            return "";
        }

        public void DrawImage(string sourceFilePath, string customboringFilePath, int imgWidth, int imgHeight)
        {
            var wordsCount = wordscounter.CountWords(sourceFilePath, 
                customboringFilePath);
            painter.Paint(cloudLayouter.FindRectanglesPositions(imgWidth, imgHeight, wordsCount));
        }

        public void SaveImage(string filePath)
        {
            var dialog = new SaveFileDialog
            {
                CheckFileExists = false,
                InitialDirectory = Path.GetFullPath(filePath),
                DefaultExt = "png",
                FileName = "image.png",
                Filter = "Изображения (*.png)|*.png|Изображения (*.jpg)|*.jpg|Изображения (*.bmp)|*.bmp"
            };
            var res = dialog.ShowDialog();
            if (res == DialogResult.OK)
                pictureBox.SaveImage(dialog.FileName);
        }
    }
}
