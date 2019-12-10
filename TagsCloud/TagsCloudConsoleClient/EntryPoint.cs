using Autofac;
using System;
using System.Drawing;
using TagsCloudGenerator.Interfaces;
using CommandLine;
using System.Linq;
using TagsCloudGenerator.Settings;
using TagsCloudGeneratorExtensions;

namespace TagsCloudConsoleClient
{
    internal class EntryPoint
    {
        private static void Main(string[] args)
        {
            var container = BuildContainer();
            Console.WriteLine(new ParserOptionsHelp(container).GenerateHelp());
            var parseResult = Parser.Default.ParseArguments<ParserOptions>(args);
            SetSettingsFromArguments(container.Resolve<Settings>(), parseResult);
            var (readFrom, saveTo) = GetPathsToReadAndToSave(parseResult);
            var generator = container.Resolve<IGenerator>();
            Console.WriteLine(generator.TryGenerate(readFrom, saveTo));
        }

        private static (string readFrom, string saveTo) GetPathsToReadAndToSave(
            ParserResult<ParserOptions> parsedArguments)
        {
            string readFrom = null;
            string saveTo = null;
            parsedArguments.WithParsed(o =>
            {
                readFrom = o.PathToRead;
                saveTo = o.PathToSave;
            });
            return (readFrom, saveTo);
        }

        private static void SetSettingsFromArguments(
            Settings settings,
            ParserResult<ParserOptions> parsedArguments)
        {
            parsedArguments.WithParsed(o =>
            {
                settings.FactorySettings.PainterId = 
                    o.PainterFactorialId ?? settings.FactorySettings.PainterId;
                settings.FactorySettings.PointsSearcherId = 
                    o.PointsSearcherFactorialId ?? settings.FactorySettings.PointsSearcherId;
                settings.FactorySettings.SaverId = 
                    o.SaverFactorialId ?? settings.FactorySettings.SaverId;
                settings.FactorySettings.WordsParserId = 
                    o.ParserFactorialId ?? settings.FactorySettings.WordsParserId;
                settings.FactorySettings.WordsLayouterId = 
                    o.WordsLayouterFactorialId ?? settings.FactorySettings.WordsLayouterId;
                settings.FactorySettings.WordsConvertersIds = 
                    o.ConvertersFactorialIds.FirstOrDefault() != default ?
                    o.ConvertersFactorialIds.ToArray() :
                    settings.FactorySettings.WordsConvertersIds;
                settings.FactorySettings.WordsFiltersIds =
                    o.FiltersFactorialIds.FirstOrDefault() != default ?
                    o.FiltersFactorialIds.ToArray() :
                    settings.FactorySettings.WordsFiltersIds;

                settings.Font = o.FontName ?? settings.Font;
                settings.ImageSize =
                    o.WidthAndHeight.Count() == 2 ?
                    new Size(o.WidthAndHeight.ElementAt(0), o.WidthAndHeight.ElementAt(1)) :
                    settings.ImageSize;

                settings.PainterSettings.BackgroundColor =
                    o.BackgroundColor != null ?
                    Color.FromName(o.BackgroundColor) :
                    settings.PainterSettings.BackgroundColor;
                settings.PainterSettings.Colors =
                    o.Colors.FirstOrDefault() != default ?
                    o.Colors.Select(s => Color.FromName(s)).ToArray() :
                    settings.PainterSettings.Colors;

                settings.TakenPartsOfSpeech = o.TakenPartsOfSpeech.ToArray();
            });
        }

        private static IContainer BuildContainer()
        {
            var cb = new ContainerBuilder();
            cb.RegisterAssemblyTypes(typeof(ISettings).Assembly, typeof(Settings).Assembly)
                .Where(t => t != typeof(GeneratorSettings) && t != typeof(Settings))
                .AsImplementedInterfaces()
                .SingleInstance();
            cb.RegisterType<Settings>().As<ISettings>().AsSelf().SingleInstance();
            return cb.Build();
        }
    }
}