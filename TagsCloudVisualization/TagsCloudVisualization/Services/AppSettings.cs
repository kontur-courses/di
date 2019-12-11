using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagsCloudVisualization.Logic;

namespace TagsCloudVisualization.Services
{
    public class AppSettings : IImageSettingsProvider, IDocumentPathProvider, IBoringWordsProvider
    {
        private string textDocumentPath;
        public HashSet<string> BoringWords { get; set; }

        public AppSettings()
        {
            BoringWords = TextRetriever
                .RetrieveTextFromFile(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BoringWords.txt")))
                .Split(new[] {'\r','\n'}, StringSplitOptions.RemoveEmptyEntries)
                .ToHashSet();
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