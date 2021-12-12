using System;
using System.Collections.Generic;
using Autofac;
using CommandLine;
using TagsCloudVisualization.Common.TagCloudPainters;
using TagsCloudVisualization.Common.Tags;

namespace TagsCloudVisualization
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<CommandLineOptions>(args)
                .WithParsed(RunOptions)
                .WithNotParsed(HandleParseError);
        }
        
        private static void RunOptions(CommandLineOptions opts)
        {
            var container = ContainerConfig.ConfigureContainer(opts);
            
            var tagBuilder = container.Resolve<ITagBuilder>();
            var tags = tagBuilder.GetTags(opts.InputFile);

            var styledTagBuilder = container.Resolve<IStyledTagBuilder>();
            var styledTags = styledTagBuilder.GetStyledTags(tags);

            var painter = container.Resolve<ITagCloudPainter>();
            painter.Paint(styledTags, opts.OutputFile);
        }

        private static void HandleParseError(IEnumerable<Error> errors)
        {
            foreach (var error in errors)
                Console.WriteLine(error.ToString());
        }
    }
}