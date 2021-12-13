using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudVisualizationDI.Settings
{
    public class DeffaultSettingsConfiguration : ISettingsConfiguration
    {
        
        public DeffaultSettingsConfiguration(string pathToFile, string pathToSave, ImageFormat format, List<string> excludedWords)
        {
            SavePath = pathToSave;
            FilePath = pathToFile;


            Format = SetFormat(format);
            ExcludedWords = SetExcludedWords(excludedWords);
        } 

        public string SavePath { get; }
        public string FilePath { get; }

        public ImageFormat Format { get; }

        public List<string> ExcludedWords { get; }


        public SolidBrush Brush => new SolidBrush(Color.Black);

        


        private List<string> SetExcludedWords(List<string> excludedWords)
        {
            return (excludedWords == null) ? DefaultExcludedWords : excludedWords;
        }

        private ImageFormat SetFormat(ImageFormat format)
        {
            return (format == null) ? DefaultFormat : format;
        }

        private ImageFormat DefaultFormat
        {
            get => ImageFormat.Png;
        }

        private List<string> DefaultExcludedWords
        {
            get => new List<string> {"к", "с", "он", "она", "в", "как", "что", "это", "то", "этот"};
        }
    }
}
