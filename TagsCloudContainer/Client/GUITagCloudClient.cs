using TagsCloudContainer.Algorithm;
using TagsCloudContainer.Client;
using TagsCloudContainer.Infrastucture.Extensions;
using TagsCloudContainer.Infrastucture.Settings;
using TagsCloudContainer.Infrastucture.Visualization;

namespace TagsCloudContainer.Actions
{
    public class GUITagCloudClient : ITagCloudClient
    {
        private readonly PictureBox pictureBox;
        private readonly ImageSettings imageSettings;
        private readonly IDrawer drawer;
        private readonly ICloudLayouter cloudLayouter;
        private IWordProcessor wordProcessor;

        public GUITagCloudClient(PictureBox pictureBox, ImageSettings imageSettings, 
            IDrawer drawer, ICloudLayouter cloudLayouter, IWordProcessor wordProcessor)
        {
            this.pictureBox = pictureBox;
            this.imageSettings = imageSettings;
            this.drawer = drawer;
            this.cloudLayouter = cloudLayouter;
            this.wordProcessor = wordProcessor;
        }

        public void DrawImage(string sourceFilePath, string boringFilePath, int imgWidth, int imgHeight)
        {
            var wordsCount = wordProcessor.CalculateFrequencyInterestingWords(sourceFilePath, boringFilePath);
            var rectangles = cloudLayouter.GetRectangles(wordsCount);
            drawer.Draw(rectangles);
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

        public string SetBoringFilePath(string filePath)
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

        public string SetSourceFilePath(string filePath)
        {
            var dialog = new OpenFileDialog()
            {
                CheckFileExists = true,
                InitialDirectory = Path.GetFullPath(filePath),
                DefaultExt = "txt",
                FileName = "source.txt",
                Filter = "Текстовые файлы (*.txt)|*.txt"
            };

            var res = dialog.ShowDialog();

            if (res == DialogResult.OK)
                return dialog.FileName;

            return "";
        }

        public void SetSettings<TSettings>(TSettings property)
        {
            SettingsForm.For(property).ShowDialog();
            pictureBox.RecreateImage(imageSettings);
        }

    }
}