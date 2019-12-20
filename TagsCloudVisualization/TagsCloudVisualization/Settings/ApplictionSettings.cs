namespace TagsCloudVisualization.Settings
{
    internal class ApplicationSettings
    {
        public DrawerSettings DrawerSettings;
        public ImageExt ImageExt;
        public LayouterSettings LayouterSettings;
        public ReaderSettings ReaderSettings;
        public string SavePath;

        public ApplicationSettings(ImageExt imageExt, ReaderSettings readerSettings,
            LayouterSettings layouterSettings,
            DrawerSettings drawerSettings, string savePath)
        {
            ImageExt = imageExt;
            ReaderSettings = readerSettings;
            LayouterSettings = layouterSettings;
            DrawerSettings = drawerSettings;
            SavePath = savePath;
        }
    }
}