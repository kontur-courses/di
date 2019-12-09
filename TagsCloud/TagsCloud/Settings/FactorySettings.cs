using TagsCloudGenerator.Interfaces;

namespace TagsCloudGenerator.Settings
{
    public class FactorySettings : IFactorySettings
    {
        public FactorySettings() => Reset();

        public string PainterId { get; set; }
        public string SaverId { get; set; }
        public string PointsSearcherId { get; set; }
        public string WordsParserId { get; set; }
        public string WordsLayouterId { get; set; }
        public string[] WordsFiltersIds { get; set; }
        public string[] WordsConvertersIds { get; set; }

        public virtual void Reset()
        {
            PainterId = "PainterWithUserColors";
            SaverId = "PngSaver";
            PointsSearcherId = "PointsSearcherOnSpiral";
            WordsParserId = "UTF8LinesParser";
            WordsLayouterId = "WordsFrequencyLayouter";
            WordsFiltersIds = new[] { "BoringWordsFilter" };
            WordsConvertersIds = new[] { "WordsToLowerConverter" };
        }
    }
}