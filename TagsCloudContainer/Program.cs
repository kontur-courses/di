using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using CommandLine;
using CommandLine.Text;
using TagsCloudContainer.Layouter;

namespace TagsCloudContainer
{
    internal class Program
    {
        static void DisplayHelp<T>(ParserResult<T> result, IEnumerable<Error> errs)
        {
            var helpText = HelpText.AutoBuild(result, help =>
            {
                help.AdditionalNewLineAfterOption = true;
                help.AddPreOptionsLine("Usage: TagsCloudContainer -f/--file words.txt -s/--size width height -o/--output dest.png");
                help.AddPreOptionsLine("Default language: russian");
                return help;
            }, e => e);
            Console.WriteLine(helpText);
        }

        static WindsorContainer SetUpContainer(WindsorContainer container, string output, string path, Size size)
        {
            container.Register(Component.For<TagsCloudContainer>()
                        .DependsOn(
                            Dependency.OnValue("output", output)
                        ));
                container.Register(Component.For<ITextReader>()
                    .ImplementedBy<SimpleTextReader>()
                    .DependsOn(
                        Dependency.OnValue("path", path)
                    ));
                var reader = container.Resolve<ITextReader>();
                container.Register(Component.For<IWordsFilter>()
                    .ImplementedBy<SimpleWordsFilter>()
                    .DependsOn(
                        Dependency.OnValue("arr", reader.Read().ToArray())
                    ));
                var filter = container.Resolve<IWordsFilter>();
                container.Register(Component.For<IWordsCounter>()
                    .ImplementedBy<SimpleWordsCounter>()
                    .DependsOn(
                        Dependency.OnValue("arr", filter.FilterWords().ToArray())
                    ));
                var counter = container.Resolve<IWordsCounter>();
                container.Register(Component.For<IWordsToSizesConverter>()
                    .ImplementedBy<SimpleWordsToSizesConverter>()
                    .DependsOn(
                        Dependency.OnValue("size", size),
                        Dependency.OnValue("dictionary",
                            counter.CountWords().ToDictionary(kvp => kvp.Key, kvp => kvp.Value)),
                        Dependency.OnValue("sizeOfLayout", size)));
                var converter = container.Resolve<IWordsToSizesConverter>();
                container.Register(Component.For<ICircularCloudLayouter>()
                    .ImplementedBy<CircularCloudLayouter>()
                    .DependsOn(Dependency.OnValue("center", new Point(size.Width / 2, size.Height / 2))
                    ));
                container.Register(Component.For<IVisualiser>()
                    .ImplementedBy<SimpleRectanglesVisualiser>()
                    .DependsOn(
                        Dependency.OnValue("size", size))
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
                var size = new Size(int.Parse(O.Size.ToList()[0]), int.Parse(O.Size.ToList()[1]));
                var output = O.OutputFile;
                
                var container = new WindsorContainer();
                container = SetUpContainer(container, output, path, size);
                
                var tagsContainer = container.Resolve<TagsCloudContainer>();
                tagsContainer.Perform();
                
                Console.WriteLine($"Your file was succesfuly created and saved into {output}");
            });
        }
    }
}