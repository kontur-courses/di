using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using TagsCloudVisualization.Settings;


namespace TagsCloudCLI
{
    internal class SettingProvider
    {
        public GeneralSettings GetSettings(Options options)
        {
            var fontSettings = new FontSettings(options.MaxFontSize, options.FontFamilyName);
            var saverSettings = new SaverSetting(options.Directory, options.ImageName);
            var wordsPreprocessorSettings = new WordsPreprocessorSettings(GetBoringWordsFromFile(options.PathToBoringWords));
            var reader = new ReaderSettings(options.FileWithWords);
            
            var drawerSettings = new DrawerSettings(Color.FromName(options.TagColor));

            return new GeneralSettings(fontSettings, saverSettings, wordsPreprocessorSettings, reader, drawerSettings,
                new Point(0, 0));
        }
        
        
        private static IEnumerable<string> GetBoringWordsFromFile(string filename)
        {
            if (!File.Exists(filename))
                throw new ArgumentException($"No such file {filename}");
            var words = File.ReadLines(filename);
            return words;
        }
    }
}