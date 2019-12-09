using System;
using TagsCloudGenerator.Attributes;
using TagsCloudGenerator.Extensions;
using TagsCloudGenerator.Interfaces;
using TagsCloudGenerator.Painters;
using TagsCloudGenerator.PointsSearchers;
using TagsCloudGenerator.Savers;
using TagsCloudGenerator.WordsConverters;
using TagsCloudGenerator.WordsFilters;
using TagsCloudGenerator.WordsLayouters;
using TagsCloudGenerator.WordsParsers;

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
            PainterId = GetFactorialIdForType(typeof(PainterWithUserColors));
            SaverId = GetFactorialIdForType(typeof(PngSaver));
            PointsSearcherId = GetFactorialIdForType(typeof(PointsSearcherOnSpiral));
            WordsParserId = GetFactorialIdForType(typeof(UTF8LinesParser));
            WordsLayouterId = GetFactorialIdForType(typeof(WordsFrequencyLayouter));
            WordsFiltersIds = new[] { GetFactorialIdForType(typeof(BoringWordsFilter)) };
            WordsConvertersIds = new[] { GetFactorialIdForType(typeof(WordsToLowerConverter)) };
        }

        private string GetFactorialIdForType(Type type) =>
            type
            .GetFirstAttributeObj<FactorialAttribute>()
            .FactorialId;
    }
}