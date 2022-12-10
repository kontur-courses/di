using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using TagsCloudVisualization.Infrastructure;
using TagsCloudVisualization.Infrastructure.Analyzer;
using TagsCloudVisualization.Infrastructure.Parsers;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.InfrastructureUI
{
    public class PictureBoxImageHolder : PictureBox, IImageHolder
    {
        private readonly IAnalyzer analyzer;
        private readonly TextFileProvider fileProvider;

        public PictureBoxImageHolder(IAnalyzer analyzer, TextFileProvider fileProvider)
        {
            this.fileProvider = fileProvider;
            this.analyzer = analyzer;
        }

        public Size GetImageSize()
        {
            FailIfNotInitialized();
            return Image.Size;
        }

        public Graphics StartDrawing()
        {
            FailIfNotInitialized();
            return Graphics.FromImage(Image);
        }

        private void FailIfNotInitialized()
        {
            if (Image == null)
                throw new InvalidOperationException("Call PictureBoxImageHolder.RecreateImage before other method call!");
        }

        public void UpdateUi()
        {
            Refresh();
            Application.DoEvents();
        }

        public void RecreateImage(ImageSettings imageSettings)
        {
            Image = new Bitmap(imageSettings.Width, imageSettings.Height, PixelFormat.Format24bppRgb);
        }

        public void SaveImage(string fileName)
        {
            FailIfNotInitialized();
            Image.Save(fileName);
        }


        public void SetFile(string path)
        {
            fileProvider.Path = path;
        }

        public void SetParser(IParser parser)
        {
            analyzer.SetParser(parser);
        }
    }
}