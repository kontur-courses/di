using System.IO;

namespace TagsCloudVisualization
{
    public class AppSettings : IImageSettingsProvider, IDocumentPathProvider
    {
        private string textDocumentPath;

        public AppSettings()
        {
            ImageSettings = ImageSettings.InitializeDefaultSettings();
        }

        public ImageSettings ImageSettings { get; set; }
        public bool TryGetPath(out string path)
        {
            path = null;
            if (!File.Exists(textDocumentPath)) 
                return false;
            path = textDocumentPath;
            return true;

        }

        public void SetPath(string path)
        {
            textDocumentPath = path;
        }
    }
}