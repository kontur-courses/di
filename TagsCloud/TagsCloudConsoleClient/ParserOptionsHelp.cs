using CommandLine;
using Autofac;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TagsCloudGenerator.Attributes;
using TagsCloudGenerator.Interfaces;
using System.Linq;

namespace TagsCloudConsoleClient
{
    internal class ParserOptionsHelp
    {
        private readonly IContainer container;

        public ParserOptionsHelp(IContainer container) => this.container = container;

        public string GenerateHelp()
        {
            var sb = new StringBuilder();
            sb.AppendLine(new string('-', 30));
            sb.AppendLine("Help:");
            sb.AppendLine(CreateHelpForProrerty<IWordsParser>(nameof(ParserOptions.ParserFactorialId), true));
            sb.AppendLine(CreateHelpForProrerty<ISaver>(nameof(ParserOptions.SaverFactorialId), true));
            sb.AppendLine(CreateHelpForProrerty<IPainter>(nameof(ParserOptions.PainterFactorialId), true));
            sb.AppendLine(CreateHelpForProrerty<IPointsSearcher>(nameof(ParserOptions.PointsSearcherFactorialId), true));
            sb.AppendLine(CreateHelpForProrerty<IWordsLayouter>(nameof(ParserOptions.WordsLayouterFactorialId), true));
            sb.AppendLine(CreateHelpForProrerty<IWordsConverter>(nameof(ParserOptions.ConvertersFactorialIds), false));
            sb.AppendLine(CreateHelpForProrerty<IWordsFilter>(nameof(ParserOptions.FiltersFactorialIds), false));
            sb.AppendLine(new string('-', 30));

            return sb.ToString();
        }

        private string CreateHelpForProrerty<TFinding>(string propertyName, bool isSingle)
        {
            var option = typeof(ParserOptions)
                .GetProperty(propertyName)
                .GetCustomAttribute<OptionAttribute>();
            var singleOrSeveral = isSingle ? "single" : "several";
            var end = isSingle ? "" : "s";
            return $"For option " +
                $"\'-{option.ShortName}\'" +
                $" or " +
                $"\'--{option.LongName}\'" +
                $" are supported " +
                $"{singleOrSeveral}" +
                $" value{end}" +
                $" from: " +
                $"{string.Join(", ", GetAllFactorialIds<TFinding>().Select(s => $"\'{s}\'"))}";
        }

        private string[] GetAllFactorialIds<TWithFactorialId>()
        {
            var result = new List<string>();
            foreach (var t in container.Resolve<IEnumerable<TWithFactorialId>>())
            {
                var attr = t.GetType().GetCustomAttribute<FactorialAttribute>();
                if (attr != null)
                    result.Add(attr.FactorialId);
            }
            return result.ToArray();
        }
    }
}