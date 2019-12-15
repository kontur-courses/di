using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using CommandLine;
using CommandLine.Text;
using TagsCloudContainer.Interfaces;
using TagsCloudContainer.Layouter;
using Component = Castle.MicroKernel.Registration.Component;

namespace TagsCloudContainer
{
    internal class Program
    {
        static void DisplayHelp<T>(ParserResult<T> result, IEnumerable<Error> errs)
        {
            if (errs.ToArray().Length != 0)
            {
                var isHelp = false;
                foreach (var err in errs)
                {
                    if (err is HelpRequestedError)
                    {
                        isHelp = true;
                        break;
                    }

                    Console.WriteLine(err.ToString());
                }

                if (!isHelp)
                {
                    Console.WriteLine("Use --help to see usage");
                    return;
                }
            }

            var helpText = HelpText.AutoBuild(result, help =>
            {
                help.AdditionalNewLineAfterOption = true;
                help.AddPreOptionsLine(
                    "Usage: TagsCloudContainer -i words.txt -w 2000 -h 2000 -o test -f Impact -c Red -m false -r Jpeg " +
                    "-e PR SPRO");
                help.AddPreOptionsLine("Default language: russian");
                return help;
            }, e => e);
            Console.WriteLine(helpText);
        }

        static WindsorContainer SetUpContainer(WindsorContainer container, string output, string input, Size size,
            String color, String font, bool compression, string format, IEnumerable<string> excluded)
        {
            container.Register(Component.For<TagsCloudContainer>()
                .DependsOn(
                    Dependency.OnValue("output", output + $".{format}"),
                    Dependency.OnValue("input", input)
                ));
            container.Register(Component.For<ITextReader>()
                .ImplementedBy<DefaultTextReader>());
            container.Register(Component.For<IWordsFilter>()
                .ImplementedBy<DefaultWordsFilter>()
                .DependsOn(Dependency.OnValue("excluded", excluded))
            );
            container.Register(Component.For<IWordsCounter>()
                .ImplementedBy<DefaultWordsCounter>());
            container.Register(Component.For<IWordsToSizesConverter>()
                .ImplementedBy<DefaultWordsToSizesConverter>()
                .DependsOn(Dependency.OnValue("size", size))
            );
            container.Register(Component.For<ICloudLayouter>()
                .ImplementedBy<CircularCloudLayouter>()
                .DependsOn(Dependency.OnValue("center", new Point(size.Width / 2, size.Height / 2)),
                    Dependency.OnValue("compression", compression)));
            container.Register(Component.For<IPointsGenerator>()
                .ImplementedBy<SpiralPointsGenerator>());
            container.Register(Component.For<IVisualiser>()
                .ImplementedBy<DefaultVisualiser>()
                .DependsOn(
                    Dependency.OnValue("size", size),
                    Dependency.OnValue("color", color),
                    Dependency.OnValue("font", font)
                )
            );
            container.Register(Component.For<IFileSaver>()
                .ImplementedBy<ImageSaver>()
                .DependsOn(Dependency.OnValue("format", format))
            );

            return container;
        }

        public static void Main(string[] args)
        {
            var parser = new Parser(with => with.HelpWriter = null);
            var parserResult = parser.ParseArguments<CMDOptions>(args);
            parserResult.WithNotParsed(errs => DisplayHelp(parserResult, errs));
            parserResult.WithParsed<CMDOptions>(O =>
            {
                var path = O.InputFile;
                var size = new Size(O.Width, O.Height);
                var output = O.OutputFile;

                var container = new WindsorContainer();
                container = SetUpContainer(container, output, path, size, O.Color, O.Font, O.Compression,
                    O.Format, O.Excluded);

                var tagsContainer = container.Resolve<TagsCloudContainer>();
                tagsContainer.Perform();

                Console.WriteLine($"Your file was succesfuly created and saved into {output}.{O.Format}");
            });
        }
    }
}