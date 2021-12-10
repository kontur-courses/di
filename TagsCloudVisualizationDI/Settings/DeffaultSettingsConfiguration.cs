using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualizationDI.Settings
{
    public class DeffaultSettingsConfiguration : ISettingsConfiguration
    {
        
        public DeffaultSettingsConfiguration(string pathToFile, string pathToSave)
        {
            SavePath = pathToSave;
            FilePath = pathToFile;
        } 

        public string SavePath { get; }
        public string FilePath { get; }

        public SolidBrush Brush => new SolidBrush(Color.Black);

        public List<string> ExcludedWords
        {
            get => new List<string> {"к", "с", "он", "она", "в", "как", "что", "это", "то", "этот"};
        }
    }
}
