namespace TagsCloudContainer.Settings
{
    public interface ITagsCloudSettings
    {
        IFileLoadSettings FileLoading { get; set; }
        IWordsFiltersSettings WordsFiltering { get; set; }
        ITagsCloudLayouterSettings Layouting { get; set; }
        IWordsColorSettings WordsColor { get; set; }
        IFontSettings Font { get; set; }
        IRenderingSettings Rendering { get; set; }
        ISaveSettings Saving { get; set; }
        IWordsScaleSettings WordsScaling { get; set; }
    }

    public class DefaultTagsCloudSettings : ITagsCloudSettings
    {
        public IFileLoadSettings FileLoading { get; set; }
        public IWordsFiltersSettings WordsFiltering { get; set; }
        public ITagsCloudLayouterSettings Layouting { get; set; }
        public IWordsColorSettings WordsColor { get; set; }
        public IFontSettings Font { get; set; }
        public IRenderingSettings Rendering { get; set; }
        public ISaveSettings Saving { get; set; }
        public IWordsScaleSettings WordsScaling { get; set; }

        public DefaultTagsCloudSettings(
            DefaultWordsFiltersSettings wordsFiltersSettings,
            DefaultWordsColorSettings wordsColorSettings,
            DefaultTagsCloudLayouterSettings layouterSettings,
            DefaultFontSettings fontSettings,
            DefaultFileLoadSettings fileLoadSettings,
            DefaultRenderingSettings renderingSettings,
            DefaultSaveSettings saveSettings,
            DefaultWordsScaleSettings wordsScaleSettings)
        {
            WordsFiltering = wordsFiltersSettings;
            WordsColor = wordsColorSettings;
            Layouting = layouterSettings;
            Font = fontSettings;
            FileLoading = fileLoadSettings;
            Rendering = renderingSettings;
            Saving = saveSettings;
            WordsScaling = wordsScaleSettings;
        }
    }
}