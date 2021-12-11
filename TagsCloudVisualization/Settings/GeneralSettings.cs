using System.Drawing;

namespace TagsCloudVisualization.Settings
{
    public class GeneralSettings
    {
        public FontSettings Font { get; }
        public SaverSetting Saver { get; }
        public WordsPreprocessorSettings WordsPreprocessor { get; }
        public ReaderSettings Reader { get; }
        public DrawerSettings Drawer { get; }
        
        public Point StartPoint { get; }

        public GeneralSettings(FontSettings font, SaverSetting saver, WordsPreprocessorSettings wordsPreprocessor,
            ReaderSettings reader, DrawerSettings drawer, Point startPoint)
        {
            Font = font;
            Saver = saver;
            WordsPreprocessor = wordsPreprocessor;
            Reader = reader;
            Drawer = drawer;
            StartPoint = startPoint;
        }
    }
}