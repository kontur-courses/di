using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagsCloudVisualization.Services;

namespace TagsCloudVisualization
{
    public class AppSettings : IImageSettingsProvider, IDocumentPathProvider, IBoringWordsProvider
    {
        private string textDocumentPath;
        public HashSet<string> BoringWords { get; set; }

        public AppSettings()
        {
            BoringWords = TextRetriever.RetrieveTextFromFile("BoringWords.txt").Split('\n').ToHashSet();
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