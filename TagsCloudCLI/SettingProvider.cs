using System.Drawing;
using TagsCloudVisualization.Settings;


namespace TagsCloudCLI
{
    internal class SettingProvider
    {
        public GeneralSettings GetSettings(Options options)
        {
            var fontSettings = new FontSettings(options.MaxFontSize, options.FontFamilyName);
            var saverSettings = new SaverSetting(options.Directory, options.ImageName);
            var wordsPreprocessorSettings = new WordsPreprocessorSettings(new[]
                { "в", "что", "не", "и", "с", "на", "то", "а", "он", "его", "для", "из" });
            var reader = new ReaderSettings(options.FileWithWords);
            
            var drawerSettings = new DrawerSettings(Color.FromName(options.TagColor));

            return new GeneralSettings(fontSettings, saverSettings, wordsPreprocessorSettings, reader, drawerSettings,
                new Point(0, 0));
        }
    }
}