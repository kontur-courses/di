using System.Drawing.Imaging;

namespace TagsCloudVisualizationDI.Saving
{
    public class DefaultSaver : ISaver
    {
        private string SavePath { get; }

        public DefaultSaver(string savePath, ImageFormat imageFormat)
        {
            SavePath = savePath + '.' + imageFormat;
        }

        public string GetSavePath()
        {
            return SavePath;
        }
    }
}
